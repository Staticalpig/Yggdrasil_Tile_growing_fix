using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Flip the player based on movement direction
        if (movement.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movement.x), transform.localScale.y, 1f);
        }
        if (movement.y != 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Sign(movement.y), 1f);
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}