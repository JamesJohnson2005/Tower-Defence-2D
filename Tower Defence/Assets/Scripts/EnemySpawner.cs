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

        awaitingWave = false;


        /*
         * ENEMY SPAWNING SYSTEM
         * 
         * The game gets a value that it can use to 'buy' random enemies to spawn, the value is based off the wave and this
         * is how the game keeps things more 'random' and scales as the game progresses. We make sure to have an enemy that costs 1 incase it has left over points 
         * so it can fill it any spots it may have
         */

        // Get Wave Value
        waveValue = currentWave * 5;
        while (waveValue > 0)
        {
            // Choose enemies to spawn, and reduce the money the game has to choose enemies
            int chosenEnemy = Random.Range(0, enemyTypes.Length);
            if(enemyCosts[chosenEnemy] <= waveValue) // Does the game have enough to buy it?
            {
            toSpawn.Add(enemyTypes[chosenEnemy]);
            waveValue -= enemyCosts[chosenEnemy];
            }
        }

        // Dont spawn all enemies at once, but make it scale naturally
        spawnDelay =  3 - (toSpawn.Count * 0.2f);

        if (spawnDelay < 0.2f) { spawnDelay = 0.2f; } // Hard limit
    }

    private void Update()
    {
        // Reduce timer
        timer -= Time.deltaTime;


        remainingEnemies = toSpawn.Count;

        // Set Wave Text
        waveText.text = $"Current Wave: {currentWave}";

        // Decrease graceTimer when its above zero
        if (graceTimer >= 0)
        {
            graceTimer -= Time.deltaTime;
        } else if (graceTimer <= 0 && awaitingWave)
        {
            SpawnEnemies();
        }

        // Check to see if it times to start a new wave
        if (timer <= 0 && toSpawn.Count > 0)
        {
            GameObject spawnedEnemy = Instantiate(toSpawn[0].gameObject, transform.position, Quaternion.identity);
            toSpawn.RemoveAt(0);
            currentEnemies.Add(spawnedEnemy);
            timer = spawnDelay;
        }

        // Check if its time for the grace period
        if (currentEnemies.Count == 0 && remainingEnemies == 0 && awaitingWave == false)
        {
            StartCooldown();
        }
    }

    public void StartCooldown()
    {
        // Set timer to the correct amount
        GameManager.currency += currentWave * 5;
        graceTimer = graceDelay;
        awaitingWave = true;
    }
}
