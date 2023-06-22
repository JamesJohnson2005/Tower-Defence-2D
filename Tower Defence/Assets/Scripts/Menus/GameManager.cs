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
    public int lives = 3;
    private void Awake()
    {
        currency = startMoney;
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
        if (lives <= 0)
        {
            gameOverScreen.SetActive(true);
            //Game End Method
        }
    }
}
