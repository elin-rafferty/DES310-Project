using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop_Trigger : MonoBehaviour
{
    [SerializeField] Canvas canvas, shopCanvas;
    [SerializeField] Text text;
    [SerializeField] Settings_SO settings;
    [SerializeField] Inventory_Animation inventoryAnimation;
    [SerializeField] string shopName = "shop";
    private bool trigger = false;
    private Input_Manager inputManager;

    private void Start()
    {
        inputManager = GetComponent<Input_Manager>();
        text.text = "Press " + (settings.Controls == 0 ? "E" : "X") + " to open " + shopName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger true on collision
            trigger = true;
            text.text = "Press " + (settings.Controls == 0 ? "E" : "X") + " to open " + shopName;
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
        canvas.gameObject.SetActive(trigger && !shopCanvas.gameObject.activeSelf);
        // Load shop menu
        if (inputManager.GetButtonDown("Interact") && trigger == true && !shopCanvas.gameObject.activeSelf)
        {
            shopCanvas.gameObject.SetActive(true);
            if (shopCanvas.gameObject.GetComponentInParent<Upgrade_Shop>() != null)
            {
                shopCanvas.gameObject.GetComponentInParent<Upgrade_Shop>().ResetUnpurchasedUpgrades();
            }
            inventoryAnimation.OpenInventory();
        }
        if (!inventoryAnimation.InventoryOpen && shopCanvas.gameObject.activeSelf)
        {
            shopCanvas.gameObject.SetActive(false);
        }
    }
}
