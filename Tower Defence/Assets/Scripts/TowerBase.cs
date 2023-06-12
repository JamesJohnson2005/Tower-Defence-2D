using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    private bool canBuy;
    private bool menuUp;
    public GameObject buyMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().purchaseText.SetActive(true);
            canBuy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().purchaseText.SetActive(false);
            canBuy = false;
            menuUp = false;
        }
    }

    public void Update()
    {
        // Make the menu show
        buyMenu.SetActive(menuUp);

        if (canBuy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                menuUp = true;
            }
        }
    }
}
