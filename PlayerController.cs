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

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerJump();
        playerConstantRunning();
        playerChangeGravity();
        
    }

    void Update()
    {

    }

    void playerConstantRunning()
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }


    void OnCollisionStay(Collision collision)
    {
        standingOnGround = true;
        
    }


    void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && standingOnGround == true)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f));
        }
    }

    void playerChangeGravity()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Physics.gravity *= -1;
            hasChangedGravity = !hasChangedGravity;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Platform")
        {
            SendMessageUpwards("ScreenShake", rb.velocity.y);
        }

        if (coll.gameObject.tag == "Death")
            SendMessageUpwards("StartOverLevel");

    }
}
