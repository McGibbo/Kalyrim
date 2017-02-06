using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Rigidbody player;
	void Start () {
        player = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D))
            player.velocity = new Vector3(10, player.velocity.y, 0);
        if (Input.GetKey(KeyCode.A))
            player.velocity = new Vector3(-10, player.velocity.y, 0);

        if (Input.GetKey(KeyCode.Space))
            player.AddForce(new Vector3(0, 10));
	}
}
