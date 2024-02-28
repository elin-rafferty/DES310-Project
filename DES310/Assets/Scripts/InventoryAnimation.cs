using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{

    Animator anim;
    bool InventoryOpen = false;
    public EventHandler eventHandler;
    public InputManager inputManager;
    public inventoryMainPage inventoryMainPage;

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
                anim.Play("Open");
                InventoryOpen = true;
                eventHandler.InventoryChangeState.Invoke(true);
            }
        }

        else if (InventoryOpen)
        {
            if (inputManager.GetButtonDown("OpenInventory") || inputManager.GetButtonDown("Back"))
            {
                inventoryMainPage.HideItemAction();
                anim.Play("Close");
                InventoryOpen = false;
                eventHandler.InventoryChangeState.Invoke(false);
            }
        }

    }

}
