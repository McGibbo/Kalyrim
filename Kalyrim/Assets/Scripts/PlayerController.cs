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
    private bool jumpBufferBool;
    private float time;
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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Debug.Log(standingOnGround);
        PlayerConstantRunning();
    }

    void Update()
    {
        PlayerChangeGravity();
        PlayerJump();
        Timers();

        if (rb.position.y > 20 || rb.position.y < -8)
        {
            if (checkpoint)
            {
                SendMessageUpwards("SpawnCrystals");
                rb.position = checkpointPos;
                Physics.gravity = new Vector3(Physics.gravity.x, value);
            }
            else
                SendMessageUpwards("StartOverLevel");
            
        }
    }

    void PlayerConstantRunning()
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferBool = true;
            time = Time.time;
        }
        if (Time.time > time + jumpBufferTime)
            jumpBufferBool = false;

        if (jumpBufferBool && standingOnGround == true)
        {
            rb.AddForce(new Vector3(0f, jumpForce * -Physics.gravity.y, 0f));
            jumpBufferBool = false;
        }
    }

    void PlayerChangeGravity()
    {
        if (Input.GetKeyDown(KeyCode.X) && hasChangedGravity == false)
        {
            Physics.gravity *= -1;
            ResetYVelocityFunction();
            gravityChangeBuffer = gravityChangeBufferTime;
            hasChangedGravity = true;
            GetComponent<Animator>().SetBool("Rotate", !GetComponent<Animator>().GetBool("Rotate"));
        }
    }

    void ResetYVelocityFunction()
    {
        if (resetYVelocity)
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    void Timers()
    {
        gravityChangeBuffer -= Time.deltaTime;
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
            if (checkpoint)
            {
                SendMessageUpwards("SpawnCrystals");
                rb.position = checkpointPos;
                Physics.gravity = new Vector3(Physics.gravity.x, value);
            }
            else
                SendMessageUpwards("StartOverLevel");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        standingOnGround = false;
    }

    

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Crystal")
        {
            SendMessageUpwards("CollectCrystal", coll.gameObject.transform.position);
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.tag == "Checkpoint")
        {
            value = Physics.gravity.y;
            checkpointPos = rb.transform.position;
            checkpoint = true;
        }
    }
}
