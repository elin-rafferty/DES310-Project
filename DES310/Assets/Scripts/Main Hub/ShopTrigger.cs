using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Text text;
    [SerializeField] SettingsSO settings;
    private bool trigger = false;
    private InputManager inputManager;
    private BuyItem buyItemComponent;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        buyItemComponent = GetComponent<BuyItem>();
        text.text = "Press " + (settings.Controls == 0 ? "E" : "X") + " to purchase " + buyItemComponent.quantity + " " + buyItemComponent.itemToBuy.Name + " for " + buyItemComponent.cost + " " + buyItemComponent.currencyItem;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger true on collision
            trigger = true;
            canvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger false on collision end
            trigger = false;
            canvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Load shop menu
        if (inputManager.GetButtonDown("Interact") && trigger == true)
        {
            // Put code to do that here
            Debug.Log("The shop will open here eventually");
            buyItemComponent.PurchaseItem();
        }
    }
}
