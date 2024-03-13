using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Inventory_Animation : MonoBehaviour
{

    Animator anim;
    public bool InventoryOpen = false;
    public EventHandler eventHandler;
    public Input_Manager inputManager;
    public inventory_Main_Page inventoryMainPage;
    public GameObject virtualMouse;
    public Settings_SO settings;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InventoryOpen)
        {
            if (inputManager.GetButtonDown("OpenInventory"))
            {
                OpenInventory();
            }
        }

        else if (InventoryOpen)
        {
            if (inputManager.GetButtonDown("OpenInventory") || inputManager.GetButtonDown("Back"))
            {
                CloseInventory();
            }
        }

    }

    public void OpenInventory()
    {
        anim.Play("Open");
        InventoryOpen = true;
        eventHandler.InventoryChangeState.Invoke(true);
        if (settings.Controls == 1)
        {
            virtualMouse.SetActive(true);
        }
    }

    public void CloseInventory()
    {
        inventoryMainPage.HideItemAction();
        anim.Play("Close");
        InventoryOpen = false;
        eventHandler.InventoryChangeState.Invoke(false);
        virtualMouse.SetActive(false);
    }

}
