using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int playerSpeed;

    private float x, y;

    private Rigidbody2D rb;
    private BoxCollider2D collider;

    private void Awake()
    {
        // Declare Components
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Get User Input
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Move Player
        rb.velocity = new Vector2(x * playerSpeed, y * playerSpeed);
    }
}
