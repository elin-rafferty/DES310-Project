using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField] Canvas canvas,storageCanvas;
    [SerializeField] Text text;
    [SerializeField] SettingsSO settings;
    [SerializeField] InventoryAnimation inventoryAnimation;
    [SerializeField] string storageName = "Storage";
    private bool trigger = false;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        text.text = "Press " + (settings.Controls == 0 ? "E" : "A") + " to open " + storageName;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger true on collision
            trigger = true;
            text.text = "Press " + (settings.Controls == 0 ? "E" : "A") + " to open " + storageName;
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
        // Load chest menu
        //if (inputManager.GetButtonDown("Interact") && trigger == true)
        //{
        //    // Put code to do that here
        //    Debug.Log("Chest will open here eventually");
        //}

        canvas.gameObject.SetActive(trigger && !storageCanvas.gameObject.activeSelf);
        // Load shop menu
        if (inputManager.GetButtonDown("Interact") && trigger == true && !storageCanvas.gameObject.activeSelf)
        {
            storageCanvas.gameObject.SetActive(true);
            if (storageCanvas.gameObject.GetComponentInParent<UpgradeShop>() != null)
            {
                storageCanvas.gameObject.GetComponentInParent<UpgradeShop>().ResetUnpurchasedUpgrades();
            }
            inventoryAnimation.OpenInventory();
        }
        if (!inventoryAnimation.InventoryOpen && storageCanvas.gameObject.activeSelf)
        {
            storageCanvas.gameObject.SetActive(false);
        }
    }
}
