using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform trans;

    public float speed;
    public float jumpForce;
    public float moveInput;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        rb.velocity = Vector2.up * jumpForce;       // jump
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);       // move horizontal
        transform.Rotate(0, 0, -1);         // rotate salmon
    }
}
