using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HotkeyListener : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Inventory.Model.InventorySO playerInventory;
    [SerializeField] inventoryMainPage inventoryMainPage;
    [SerializeField] InventoryAnimation inventoryAnimation;
    [SerializeField] PersistentVariables persistentVariables;

    [SerializeField] int hotkeySlot1, hotkeySlot2;
    // Start is called before the first frame update
    void Start()
    {
        hotkeySlot1 = persistentVariables.hotkeySlot1;
        hotkeySlot2 = persistentVariables.hotkeySlot2;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.GetButtonDown("Hotkey1"))
        {
            if (inventoryAnimation.InventoryOpen)
            {
                if (inventoryMainPage.selectedSlot != -1)
                {
                    if (!playerInventory.GetItemAt(inventoryMainPage.selectedSlot).IsEmpty)
                    {
                        IItemAction itemAction = playerInventory.GetItemAt(inventoryMainPage.selectedSlot).item as IItemAction;
                        if (itemAction != null)
                        {
                            hotkeySlot1 = inventoryMainPage.selectedSlot;
                        }
                    }
                }
            } else
            {
                if (hotkeySlot1 != -1)
                {
                    if (!playerInventory.GetItemAt(hotkeySlot1).IsEmpty)
                    {
                        IItemAction itemAction = playerInventory.GetItemAt(hotkeySlot1).item as IItemAction;
                        if (itemAction != null)
                        {
                            InventoryItem item = playerInventory.GetItemAt(hotkeySlot1);
                            playerInventory.RemoveItem(hotkeySlot1, 1);
                            itemAction.PerformAction(GameObject.FindGameObjectWithTag("Player"), item.itemState);
                            if (playerInventory.GetItemAt(hotkeySlot1).IsEmpty)
                            {
                                hotkeySlot1 = -1;
                            }
                        }
                    }
                    else
                    {
                        hotkeySlot1 = -1;
                    }
                }
            }
        }
        if (inputManager.GetButtonDown("Hotkey2"))
        {
            if (inventoryAnimation.InventoryOpen)
            {
                if (inventoryMainPage.selectedSlot != -1)
                {
                    if (!playerInventory.GetItemAt(inventoryMainPage.selectedSlot).IsEmpty)
                    {
                        IItemAction itemAction = playerInventory.GetItemAt(inventoryMainPage.selectedSlot).item as IItemAction;
                        if (itemAction != null)
                        {
                            hotkeySlot2 = inventoryMainPage.selectedSlot;
                        }
                    }
                }
            } else
            {
                if (hotkeySlot2 != -1)
                {
                    if (!playerInventory.GetItemAt(hotkeySlot2).IsEmpty)
                    {
                        IItemAction itemAction = playerInventory.GetItemAt(hotkeySlot2).item as IItemAction;
                        if (itemAction != null)
                        {
                            InventoryItem item = playerInventory.GetItemAt(hotkeySlot2);
                            playerInventory.RemoveItem(hotkeySlot2, 1);
                            itemAction.PerformAction(GameObject.FindGameObjectWithTag("Player"), item.itemState);
                            if (playerInventory.GetItemAt(hotkeySlot2).IsEmpty)
                            {
                                hotkeySlot2 = -1;
                            }
                        }
                    }
                    else
                    {
                        hotkeySlot2 = -1;
                    }
                }
            }
        }
        persistentVariables.hotkeySlot1 = hotkeySlot1;
        persistentVariables.hotkeySlot2 = hotkeySlot2;
    }
}
