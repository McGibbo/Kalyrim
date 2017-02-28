using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Vector3 playerPosition;

    private bool standingOnGround = false;
    private bool hasChangedGravity = false;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float jumpBufferTime = 1f;
    private float jumpBuffer;
    private bool jumpBufferBool;
    private bool flipRotation;
    private Rigidbody rb;
    private bool checkpoint;
    private Vector3 checkpointPos;
    private float value;
    [SerializeField]
    private bool resetYVelocity;
    [SerializeField]
    private float gravityChangeBufferTime;
    private float gravityChangeBuffer;
    [SerializeField]
    private float jumpAfterPlatformBufferTime;
    private float jumpAfterPlatformBuffer;
    private bool startGame = false;
    PlayerAnimations playerAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GameObject.FindWithTag("PlayerAnimation").GetComponent<PlayerAnimations>();
        checkpointPos = transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Debug.Log(standingOnGround);
    }

    void Update()
    {
        if (startGame)
        {
            PlayerChangeGravity();
            PlayerJump();
            PlayerConstantRunning();
        }


        if (transform.position.y > 20 || transform.position.y < -8)
        {
            RespawnPlayer();
        }

    
        if (Input.GetKeyDown(KeyCode.G))
        {
            startGame = true;
        }
    }

    void RespawnPlayer()
    {
        SendMessageUpwards("SpawnCrystals");
        rb.position = checkpointPos;
        Physics.gravity = new Vector3(Physics.gravity.x, value);
        ResetYVelocityFunction();
        playerAnim.SetAnimationBool("Rotate");
        SendMessageUpwards("ChangeDeathCounter");
    }

    void PlayerJump()
    {
        if (jumpBuffer > 0)
        {
            jumpBuffer -= Time.deltaTime;
        }
        if (jumpAfterPlatformBuffer > 0)
        {
            jumpAfterPlatformBuffer -= Time.deltaTime;
        }
        else
        {
            jumpAfterPlatformBuffer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferBool = true;
            jumpBuffer = jumpBufferTime;
        }
        if (jumpBuffer <= 0)
            jumpBufferBool = false;

        if (jumpBufferBool && standingOnGround)
        {
            rb.AddForce(new Vector3(0f, jumpForce * -Physics.gravity.y, 0f));
            jumpBufferBool = false;
            jumpAfterPlatformBuffer = 0;
        }
        else if (jumpBufferBool && jumpAfterPlatformBuffer > 0 && rb.velocity.y/Physics.gravity.y >= 0)
        {
            rb.AddForce(new Vector3(0f, jumpForce * -Physics.gravity.y, 0f));
            jumpBufferBool = false;
            jumpAfterPlatformBuffer = 0;
        }
    }

    void PlayerConstantRunning()
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }



    void PlayerChangeGravity()
    {
        if (gravityChangeBuffer > 0)
        {
            gravityChangeBuffer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.X) && hasChangedGravity == false && gravityChangeBuffer <= 0)
        {
            Physics.gravity *= -1;
            if (resetYVelocity)
            {
                ResetYVelocityFunction();
            }
            gravityChangeBuffer = gravityChangeBufferTime;
            hasChangedGravity = true;
            playerAnim.AnimatePlayerFlip("Rotate");
        }
    }

    void ResetYVelocityFunction()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Platform")
        {
            standingOnGround = true;
            if (gravityChangeBuffer <= 0)
            {
                hasChangedGravity = false;
            }
            SendMessageUpwards("ScreenShake");

        }
        if (coll.gameObject.tag == "Death")
        {
                RespawnPlayer();
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.tag == "Platform")
        {
            jumpAfterPlatformBuffer = jumpAfterPlatformBufferTime;
            standingOnGround = false;
        }
        
    }

    

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Crystal")
        {
            SendMessageUpwards("CollectCrystal", coll.transform.position);
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.tag == "Checkpoint")
        {
            value = Physics.gravity.y;
            checkpointPos = rb.transform.position;
            checkpoint = true;
            playerAnim.GetAnimationBool("Rotate");
            SendMessageUpwards("GetCurrentSpawnPos", transform.position);
            coll.GetComponentInChildren<ParticleSystem>().Play();
        }
        if (coll.gameObject.tag == "EndPlatform")
        {
            SendMessageUpwards("FinishLevel");
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
