using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public enum TowerType
    {
        Basic,
        Gold,
        Red
    }

    private bool canBuy;
    public bool menuUp;
    public GameObject buyMenu;
    public TowerType currentType;
    public bool hasTower;
    public GameObject baseTower, goldTower, redTower;
    private GameManager gameManager;
    private GameObject player;
    public bool purchasable;
    [SerializeField]private int baseCost, goldCost, redCost;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buyMenu.SetActive(false);
    }
    private void Awake()
    {
        buyMenu = GameObject.Find("BuyMenu");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!player) { player = collision.gameObject; }

            if (!hasTower)
            {
                collision.gameObject.GetComponent<PlayerMovement>().purchaseText.SetActive(true);
                canBuy = true;
                gameManager.selectedBase = gameObject;
            } else
            {
                canBuy = true;
                collision.gameObject.GetComponent<PlayerMovement>().destroyText.SetActive(true);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetSelection();
        }
    }

    public void ResetSelection()
    {
        player.gameObject.GetComponent<PlayerMovement>().purchaseText.SetActive(false);
        player.gameObject.GetComponent<PlayerMovement>().destroyText.SetActive(false);
        canBuy = false;
        menuUp = false;
        gameManager.selectedBase = null;
        buyMenu.SetActive(false);
    }



    public void Update()
    {
        if (canBuy) // Is player close enough to tower?
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!hasTower)
                {
                    menuUp = true;
                    buyMenu.SetActive(true);
                } else
                {
                    SetTower(player.gameObject.GetComponent<PlayerMovement>().purchaseText);
                    hasTower = false;
                    ResetSelection();
                }
                player.gameObject.GetComponent<PlayerMovement>().purchaseText.SetActive(false);
                player.gameObject.GetComponent<PlayerMovement>().destroyText.SetActive(false);
            }
        }
    }

    public void PlaceTower(int type)
    {
        // TO:DO
        // Does player have enough to buy the tower done!
        // Optional (can leave for James): gray out buttons for towers you cant buy
        
        menuUp = false;
        buyMenu.SetActive(false);

        
        switch (type)
        {
            case 1:
                if(GameManager.currency < baseCost) { return; }
                SetTower(baseTower);
                GameManager.currency -= baseCost;
                break;
            case 2:
                if (GameManager.currency < goldCost) { return; }
                SetTower(goldTower);
                GameManager.currency -= goldCost;
                break;
            case 3:
                if (GameManager.currency < redCost) { return; }
                SetTower(redTower);
                GameManager.currency -= redCost;
                break;
        }
        gameManager.selectedBase = null;
        
        hasTower = true;
        
    }

    private void SetTower(GameObject newType)
    {
        // Disable all
        baseTower.SetActive(false);
        goldTower.SetActive(false);
        redTower.SetActive(false);

        // Set the new one as active
        newType.SetActive(true);
    }
}
