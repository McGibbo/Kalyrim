using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    private Vector3 playerPosition;

    private float lockFloat = 0f;
    private bool standingOnGround = false;
    private bool hasChangedGravity = false;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        playerJump();
        playerConstantRunning();
        playerChangeGravity();
	}

    void playerConstantRunning()
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, lockFloat);
    }

    
    void OnCollisionStay(Collision collision)
    {
        standingOnGround = true;
    }


    void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Z) && standingOnGround == true)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, jumpForce, 0f));
        }
    }

    void playerChangeGravity()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Physics.gravity *= -1;
            hasChangedGravity = !hasChangedGravity;
            Debug.Log ("Swag2");
        }
    }
}
