using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public Image[] speedButtons;
    public int lives = 3;
    private void Awake()
    {
        // Set the speed button color
        speedButtons[0].color = Color.green;
        // Set starting money
        currency = startMoney;

        // Assign References
        scoreScript = GetComponent<HighScore>();
        spawnerScript = GetComponent<EnemySpawner>();
    }

    public void BuyTower(int type)
    {
        // Purchase a tower
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
            // Run game over stuff
            Time.timeScale = 0;
            loseScreen.SetActive(true);

            // Save highscore if theres a new score
            scoreScript.SaveScore(spawnerScript.currentWave);
            loseText.text = $"Score: {spawnerScript.currentWave}\nHighscore: {scoreScript.highScore}";
            gameOver = true;
        }
    }

    public void ChangeSpeed(int _speed)
    {
        // Adjust game speed
        Time.timeScale = _speed;
        
        // Set all buttons to normal
        foreach(Image image in speedButtons)
        {
            image.color = Color.white;
        }

        // Set the button you pressed to green
        switch (_speed)
        {
            case 1:
                speedButtons[0].color = Color.green;
                break;
            case 3:
                speedButtons[1].color = Color.green;
                break;
            case 10:
                speedButtons[2].color = Color.green;
                break;
        }
    }
}
