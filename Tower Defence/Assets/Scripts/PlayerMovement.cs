using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    public int playerSpeed;
    public int playerDamage;
    public float fireSpeed;

    private float x, y;
    private float xPos, yPos;

    private Rigidbody2D rb;
    private BoxCollider2D collider;
    public GameObject purchaseText;
    private float fireTimer;
    public GameObject bulletPrefab;
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
        fireTimer -= Time.deltaTime;

        // Get User Input
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        if (Input.GetButton("Fire1"))
        {
            PlayerShoot();
        }
        xPos = Mathf.Clamp(transform.position.x, -screenBounds.x + transform.localScale.x, screenBounds.x - transform.localScale.y);
        yPos = Mathf.Clamp(transform.position.y, -screenBounds.y + transform.localScale.x, screenBounds.y - transform.localScale.y);
    }   


        

    private void FixedUpdate()
    {
        // Move Player
        rb.velocity = new Vector2(x * playerSpeed, y * playerSpeed);
    }
    private void PlayerShoot()
    {
        // Can the tower shoot?
        if (fireTimer <= 0)
        {
            // Spawn a bullet object
            GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Make bullet face the same way as the tower
            spawnedBullet.transform.right = -transform.up;

            spawnedBullet.GetComponent<BulletManager>().SetValues(playerDamage, null);

            // Destroy the bullet after 5 seconds if it somehow manages to stay 'alive' that long
            Destroy(spawnedBullet, 5);

            // Reset timer
            fireTimer = fireSpeed;
        }
    }
}
