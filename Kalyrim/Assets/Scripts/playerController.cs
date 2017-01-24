using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    private Vector3 playerPosition;
    public float speed = 5f;
    public float jumpForce = 100f;

    private bool standingOnGround = true;
    private bool hasChangedGravity = false;

    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        playerJump();
        playerConstantRunning();
        playerChangeGravity();
	}

    void playerConstantRunning()
    {
        //Vector3 position = transform.position;
        //position[0] += speed;
        //transform.position = position;
        GetComponent<Rigidbody2D>().velocity = new Vector2(10, GetComponent<Rigidbody2D>().velocity.y);
    }

    /*
    void OnCollisionStay(Collision)
    {

    }
    */

    void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (standingOnGround == true)
            {
                if (hasChangedGravity == false)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpForce));
                }
            }
        }
    }
    
    void playerChangeGravity()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Physics2D.gravity *= -1;
            hasChangedGravity = !hasChangedGravity;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Platform")
        {
            SendMessageUpwards("ScreenShake", GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
