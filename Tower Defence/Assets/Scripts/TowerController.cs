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
            transform.LookAt(currentTarget.transform.position);

            //transform.localRotation = Quaternion.Euler(new Vector3(initialRotation.x, this.transform.localRotation.y, initialRotation.z));
        }
    }
}
