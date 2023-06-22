using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject selectedBase;
    public GameObject gameOverScreen;
    public int startMoney;
    public static int currency;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI destroyText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI loseText;
    public GameObject loseScreen;
    private HighScore scoreScript;
    private EnemySpawner spawnerScript;
    private bool gameOver;
    public int lives = 3;
    private void Awake()
    {
        currency = startMoney;
        scoreScript = GetComponent<HighScore>();
        spawnerScript = GetComponent<EnemySpawner>();
    }

    public void BuyTower(int type)
    {
        if (selectedBase)
            selectedBase.GetComponent<TowerBase>().PlaceTower(type);
    }
    public void Update()
    {
        currencyText.text = $"Currency: {currency}";
        livesText.text = $"Lives: {lives}";
        // Check for game over
        if (lives <= 0 && !gameOver)
        {
            Time.timeScale = 0;
            loseScreen.SetActive(true);
            scoreScript.SaveScore(spawnerScript.currentWave);
            loseText.text = $"Score: {spawnerScript.currentWave}\nHighscore: {scoreScript.highScore}";
            gameOver = true;
        }
    }
}
