using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySwapsHandler : MonoBehaviour
{
    [SerializeField] Inventory.Model.InventorySO playerInventory, storageSO;
    [SerializeField] Inventory.UI.inventoryMainPage inventoryMainPage;
    [SerializeField] StorageVisualsController storageVisuals;
    [SerializeField] List<GameObject> buttons = new();

    bool inventorySelected = false;
    bool storageSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSelected();
        HandleButtons();
    }

    public void DepositAll()
    {
        int slot = inventoryMainPage.selectedSlot;
        if (slot != -1)
        {
            Inventory.Model.InventoryItem newStack = new();
            Inventory.Model.InventoryItem oldStack = playerInventory.GetItemAt(slot);
            newStack.item = oldStack.item;
            newStack.quantity = oldStack.quantity;
            storageSO.AddItem(newStack);
            playerInventory.RemoveItem(slot, playerInventory.GetItemAt(slot).quantity);
            inventoryMainPage.ResetSelection();
        }
    }

    void CheckSelected()
    {
        if (inventoryMainPage.selectedSlot != -1 && storageVisuals.selectedSlot != -1)
        {
            inventoryMainPage.DeselectAllItems();
            storageVisuals.DeselectAllItems();
        } else
        {
            if (inventoryMainPage.selectedSlot != -1)
            {
                inventorySelected = true;
            }
            else
            {
                inventorySelected = false;
            }
            if (storageVisuals.selectedSlot != -1)
            {
                storageSelected = true;
            }
            else
            {
                storageSelected = false;
            }
        }
    }

    void HandleButtons()
    {
        foreach (var button in buttons)
        {
            button.SetActive(inventorySelected || storageSelected);
        }
    }
}
