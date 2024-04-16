using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] Canvas canvas, shopCanvas;
    [SerializeField] Text text;
    [SerializeField] SettingsSO settings;
    [SerializeField] InventoryAnimation inventoryAnimation;
    [SerializeField] string shopName = "shop";
    private bool trigger = false;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        text.text = "Press " + (settings.Controls == 0 ? "E" : "A") + " to open " + shopName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger true on collision
            trigger = true;
            text.text = "Press " + (settings.Controls == 0 ? "E" : "A") + " to open " + shopName;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger false on collision end
            trigger = false;
        }
    }

    void Update()
    {
        canvas.gameObject.SetActive(trigger && !shopCanvas.gameObject.activeSelf && !inventoryAnimation.InventoryOpen);
        // Load shop menu
        if (inputManager.GetButtonDown("Interact") && trigger == true && !shopCanvas.gameObject.activeSelf && !inventoryAnimation.InventoryOpen && Time.timeScale != 0)
        {
            shopCanvas.gameObject.SetActive(true);
            if (shopCanvas.gameObject.GetComponentInParent<UpgradeShop>() != null)
            {
                shopCanvas.gameObject.GetComponentInParent<UpgradeShop>().ResetUnpurchasedUpgrades();
            }
            inventoryAnimation.OpenInventory();
        }
        if (!inventoryAnimation.InventoryOpen && shopCanvas.gameObject.activeSelf)
        {
            shopCanvas.gameObject.SetActive(false);
        }
    }
}
