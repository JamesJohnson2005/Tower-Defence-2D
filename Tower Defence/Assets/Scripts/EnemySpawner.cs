using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyTypes;
    public int[] enemyCosts;
    public List<GameObject> currentEnemies;
    public List<GameObject> toSpawn;
    public int remainingEnemies;
    public int currentWave = 1;
    private int waveValue;
    private float spawnDelay;
    private float timer;

    public float graceDelay;
    private float graceTimer;
    private bool awaitingWave;

    private void Awake()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        // Increase wave
        currentWave++;

        // Get Wave Value
        waveValue = currentWave * 5;

        while (waveValue > 0)
        {
            int chosenEnemy = Random.Range(0, enemyTypes.Length);
            toSpawn.Add(enemyTypes[chosenEnemy]);
            waveValue -= enemyCosts[chosenEnemy];
        }

        spawnDelay =  3 - (toSpawn.Count * 0.2f);

        if (spawnDelay < 0.2f) { spawnDelay = 0.2f; } // Hard limit
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        remainingEnemies = toSpawn.Count;

        // Decrease graceTimer 
        if (graceTimer >= 0)
        {
            graceTimer -= Time.deltaTime;
        }

        if (timer <= 0 && toSpawn.Count > 0)
        {
            GameObject spawnedEnemy = Instantiate(toSpawn[0].gameObject, transform.position, Quaternion.identity);
            toSpawn.RemoveAt(0);
            currentEnemies.Add(spawnedEnemy);
            timer = spawnDelay;
        }

        if (remainingEnemies == 0)
        {
            // Use this variable to tell when to do grace period
            awaitingWave = true;
        }
    }
}
