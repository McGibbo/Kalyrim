using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D player;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            player.velocity = new Vector2(-5, player.velocity.y);
        if (Input.GetKeyDown(KeyCode.D))
            player.velocity = new Vector2(5, player.velocity.y);
        if (Input.GetKey(KeyCode.Space))
            player.velocity = new Vector2(player.velocity.x, 5);
    }
}