using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject selectedBase;
    public static int currency = 200;
    public TextMeshProUGUI currencyText;
    public int lives;
    private void Awake()
    {
       
    }

    public void BuyTower(int type)
    {
        if (selectedBase)
            selectedBase.GetComponent<TowerBase>().PlaceTower(type);
    }
    public void Update()
    {
        currencyText.text = $"Currency: {currency}";
    }
}
