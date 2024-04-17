using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class HotkeyListener : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Inventory.Model.InventorySO playerInventory;
    [SerializeField] inventoryMainPage inventoryMainPage;
    [SerializeField] InventoryAnimation inventoryAnimation;
    [SerializeField] PersistentVariables persistentVariables;
    [SerializeField] InventoryItemUI inventoryItemUI1, inventoryItemUI2;
    [SerializeField] Text hotkey1Text, hotkey2Text;
    [SerializeField] SettingsSO settings;

    [SerializeField] int hotkeySlot1, hotkeySlot2;

    bool hotkey1ControllerThisFrame = false, hotkey2ControllerThisFrame = false, hotkey1ControllerLastFrame = false, hotkey2ControllerLastFrame = false;
    // Start is called before the first frame update
    void Start()
    {
        hotkeySlot1 = persistentVariables.hotkeySlot1;
        hotkeySlot2 = persistentVariables.hotkeySlot2;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMissing();
        HandleInput();
        persistentVariables.hotkeySlot1 = hotkeySlot1;
        persistentVariables.hotkeySlot2 = hotkeySlot2;
        UpdateVisuals();
    }

    void HandleInput()
    {
        UpdateControllerControls();
        if (inputManager.GetButtonDown("Hotkey1") || (hotkey1ControllerThisFrame && !hotkey1ControllerLastFrame))
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
                            if (hotkeySlot2 == inventoryMainPage.selectedSlot)
                            {
                                hotkeySlot2 = hotkeySlot1;
                            }
                            hotkeySlot1 = inventoryMainPage.selectedSlot;
                        }
                    }
                }
            }
            else
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
        if (inputManager.GetButtonDown("Hotkey2") || (hotkey2ControllerThisFrame && !hotkey2ControllerLastFrame))
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
                            if (hotkeySlot1 == inventoryMainPage.selectedSlot)
                            {
                                hotkeySlot1 = hotkeySlot2;
                            }
                            hotkeySlot2 = inventoryMainPage.selectedSlot;
                        }
                    }
                }
            }
            else
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
    }

    void UpdateVisuals()
    {
        bool hasItemInSlot = false;
        if (hotkeySlot1 != -1)
        {
            InventoryItem item = playerInventory.GetItemAt(hotkeySlot1);
            if (!item.IsEmpty)
            {
                inventoryItemUI1.SetData(item.item.ItemImage, item.quantity);
                hasItemInSlot = true;
            }
        }
        if (!hasItemInSlot)
        {
            inventoryItemUI1.ResetData();
        }
        hasItemInSlot = false;
        if (hotkeySlot2 != -1)
        {
            InventoryItem item = playerInventory.GetItemAt(hotkeySlot2);
            if (!item.IsEmpty)
            {
                inventoryItemUI2.SetData(item.item.ItemImage, item.quantity);
                hasItemInSlot = true;
            }
        }
        if (!hasItemInSlot)
        {
            inventoryItemUI2.ResetData();
        }
        hotkey1Text.text = settings.Controls == 0 ? "1" : "DPAD LEFT";
        hotkey2Text.text = settings.Controls == 0 ? "2" : "DPAD RIGHT";

    }

    void UpdateControllerControls()
    {
        hotkey1ControllerLastFrame = hotkey1ControllerThisFrame;
        hotkey2ControllerLastFrame = hotkey2ControllerThisFrame;
        hotkey1ControllerThisFrame = Input.GetAxisRaw("DPadHorizontal") < -0.5;
        hotkey2ControllerThisFrame = Input.GetAxisRaw("DPadHorizontal") > 0.5;
    }

    void CheckForMissing()
    {
        if (hotkeySlot1 != -1)
        {
            if (playerInventory.GetItemAt(hotkeySlot1).IsEmpty)
            {
                hotkeySlot1 = -1;
            }
        }
        if (hotkeySlot2 != -1)
        {
            if (playerInventory.GetItemAt(hotkeySlot2).IsEmpty)
            {
                hotkeySlot2 = -1;
            }
        }
    }
}
