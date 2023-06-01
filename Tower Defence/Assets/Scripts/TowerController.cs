using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Tower Stats")]
    public int bulletDamage;
    public float fireSpeed;
    private float fireTimer;
    public float towerRange;
    public GameObject bulletPrefab;

    [Header("Tower Attacks")]
    public GameObject currentTarget;
    [SerializeField] private List<GameObject> targets;
    private List<GameObject> enemies;
    private EnemySpawner spawnerScript;


    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }

    private void Awake()
    {
        // Declare Components
        spawnerScript = GameObject.Find("GameManager").GetComponent<EnemySpawner>();
    }

    private void Update()
    {
        // Set enemy list
        enemies = spawnerScript.currentEnemies;

        // Decrease shoot timer
        if (fireTimer >= 0) { fireTimer -= Time.deltaTime; }
        

        // Set lowest distance to infinity
        float lowDis = Mathf.Infinity;

        // Loop through each enemy and check the distance
        foreach(GameObject enemy in enemies)
        {
            float newDis = Vector2.Distance(gameObject.transform.position, enemy.transform.position);

            // Is this distance lower than the last one?
            if (newDis <= towerRange && newDis < lowDis)
            {
                // Set the distance and target
                lowDis = newDis;
                currentTarget = enemy;
            }
        }

        // No target in range?
        if (lowDis == Mathf.Infinity) { currentTarget = null; }

        // If theres a target
        if (currentTarget == true)
        {
            // Turn and face the enemy
            transform.right = Vector3.Lerp(transform.right, currentTarget.transform.position - transform.position, Time.deltaTime * 10);

            // Can the tower shoot?
            if (fireTimer <= 0)
            {
                // Spawn a bullet object
                GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                // Make bullet face the same way as the tower
                spawnedBullet.transform.right = -transform.up;

                // Destroy the bullet after 5 seconds if it somehow manages to stay 'alive' that long
                Destroy(spawnedBullet, 5);

                // Reset timer
                fireTimer = fireSpeed;
            }
        }
    }
}
