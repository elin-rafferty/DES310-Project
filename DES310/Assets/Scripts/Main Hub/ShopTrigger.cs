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
    private bool trigger = false;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        text.text = "Press " + (settings.Controls == 0 ? "E" : "X") + " to open shop";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger true on collision
            trigger = true;
            text.text = "Press " + (settings.Controls == 0 ? "E" : "X") + " to open shop";
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
            inventoryAnimation.OpenInventory();
        }
        if (!inventoryAnimation.InventoryOpen && shopCanvas.gameObject.activeSelf)
        {
            shopCanvas.gameObject.SetActive(false);
        }
    }
}
