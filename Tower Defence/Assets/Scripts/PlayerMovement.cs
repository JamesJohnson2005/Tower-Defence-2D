using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    public int playerSpeed;
    public int playerDamage;
    public float fireSpeed;
    public GameObject purchaseText;

    private float x, y;
    private float xPos, yPos;

    private Rigidbody2D rb;
    private BoxCollider2D collider;
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
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 worldPosFlattened = new Vector3(worldPosition.x, worldPosition.y, 0);

        transform.right = Vector3.Lerp(transform.right, worldPosFlattened - transform.position, Time.deltaTime * 10);

        // Get User Input
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        if (Input.GetButton("Fire1"))
        {
            PlayerShoot();
        }
        xPos = Mathf.Clamp(transform.position.x, -screenBounds.x + transform.localScale.x, screenBounds.x - transform.localScale.y);
        yPos = Mathf.Clamp(transform.position.y, -screenBounds.y + transform.localScale.x, screenBounds.y - transform.localScale.y);

        // Keep player in bounds
        if (Mathf.Abs(xPos) < Mathf.Abs(transform.position.x))
        {
            transform.position = new Vector2(xPos, transform.position.y);
        }

        if (Mathf.Abs(yPos) < Mathf.Abs(transform.position.y))
        {
            transform.position = new Vector2(transform.position.x, yPos);
        }
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

            spawnedBullet.GetComponent<BulletManager>().SetValues(playerDamage, null, false);

            // Destroy the bullet after 5 seconds if it somehow manages to stay 'alive' that long
            Destroy(spawnedBullet, 5);

            // Reset timer
            fireTimer = fireSpeed;
        }
    }
}
