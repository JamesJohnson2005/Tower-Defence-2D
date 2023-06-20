using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int enemyHealth;
    public float enemySpeed;
    private EnemyPath pathScript;
    private int currentTarget;
    private EnemySpawner spawnerScript;
    [SerializeField] private int deathValue;
    private GameManager gameManager;
    [SerializeField] public bool explosionImmunity;

    private void Awake()
    {
        pathScript = GameObject.Find("EnemyPath").GetComponent<EnemyPath>();
        currentTarget = 1;
        transform.position = pathScript.points[0].transform.position;
        spawnerScript = GameObject.Find("GameManager").GetComponent<EnemySpawner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (currentTarget <= pathScript.points.Length - 1)
        {
             gameObject.transform.position = Vector3.MoveTowards(transform.position, pathScript.points[currentTarget].transform.position, Time.deltaTime * enemySpeed);


            if (Vector2.Distance(gameObject.transform.position, pathScript.points[currentTarget].transform.position) < 0.1f)
            {
                currentTarget++;
            }
            
        }else
        {
            gameManager.lives--;
            Destroy(gameObject);
            
        }

        // Check enemy health
        if (enemyHealth <= 0)
        {
            GameManager.currency += deathValue;
            Destroy(gameObject);
        }

    }

    public void ChangeHealth(int healthChange)
    {
        enemyHealth += healthChange;
    }

    private void OnDestroy()
    {
        spawnerScript.currentEnemies.Remove(gameObject);
    }


}
