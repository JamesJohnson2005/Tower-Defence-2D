using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyTypes;
    public List<GameObject> currentEnemies;
    public int remainingEnemies;
    public int currentWave = 1;
    private int waveValue;

    public void SpawnEnemies()
    {
        // Get Wave Value
        waveValue = currentWave * 5;

        // 
    }
}
