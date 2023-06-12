using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    public int playerSpeed;
    public GameObject purchaseText;

    private float x, y;
    private float xPos, yPos;

    private Rigidbody2D rb;
    private BoxCollider2D collider;
    Vector2 screenBounds;

    private void Awake()
    {
        // Declare Components
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void Update()
    {
        // Get User Input
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        xPos = Mathf.Clamp(transform.position.x, -screenBounds.x + transform.localScale.x, screenBounds.x - transform.localScale.y);
        yPos = Mathf.Clamp(transform.position.y, -screenBounds.y + transform.localScale.x, screenBounds.y - transform.localScale.y);

        
    }

    private void FixedUpdate()
    {
        // Move Player
        rb.velocity = new Vector2(x * playerSpeed, y * playerSpeed);
    }
}
