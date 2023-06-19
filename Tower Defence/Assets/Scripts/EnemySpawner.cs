using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TextMeshProUGUI waveText;

    public float graceDelay; // Set this in inspector to wait # long
    private float graceTimer; // Dont config this in inspector
    private bool awaitingWave;
    private bool gracePeriod;

    private void Awake()
    {
        // Remove this if we dont want enemies to instantly spawn, helpful for testing tho
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        // Increase wave
        currentWave++;

        // Get Wave Value
        waveValue = currentWave * 5;

        if (gracePeriod)
        {

        }
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

        // Set Wave Text
        waveText.text = $"Current Wave: {currentWave}";

        // Decrease graceTimer when its above zero
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

        // TO:DO
        // when awaitingWave is true, set the grace timer to the grace wait time, do not update it every frame
        // or else it will always be stuck at the time
    }
}
