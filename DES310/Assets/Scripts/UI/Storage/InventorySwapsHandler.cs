using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySwapsHandler : MonoBehaviour
{
    [SerializeField] Inventory.Model.InventorySO playerInventory, storageSO;
    [SerializeField] Inventory.UI.inventoryMainPage inventoryMainPage;
    [SerializeField] StorageVisualsController storageVisuals;
    [SerializeField] GameObject depositButtons, withdrawButtons;

    bool inventorySelected = false;
    bool storageSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        inventoryMainPage.DeselectAllItems();
        storageVisuals.DeselectAllItems();
        CheckSelected();
        HandleButtons();
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
            int amount = playerInventory.GetItemAt(slot).quantity;
            Deposit(slot, amount);
        }
    }

    public void DepositQuantity(int amount)
    {
        int slot = inventoryMainPage.selectedSlot;
        if (slot != -1)
        {
            Deposit(slot, amount);
        }
    }

    public void WithdrawAll()
    {
        int slot = storageVisuals.selectedSlot;
        if (slot != -1)
        {
            int amount = storageSO.GetItemAt(slot).quantity;
            Withdraw(slot, amount);
        }
    }

    public void WithdrawQuantity(int amount)
    {
        int slot = storageVisuals.selectedSlot;
        if (slot != -1)
        {
            Withdraw(slot, amount);
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
        depositButtons.SetActive(inventorySelected);
        withdrawButtons.SetActive(storageSelected);
    }

    /*void Deposit(int slot, int amount)
    {
        Inventory.Model.InventoryItem newStack = new();
        Inventory.Model.InventoryItem oldStack = playerInventory.GetItemAt(slot);
        newStack.item = oldStack.item;
        newStack.quantity = amount <= oldStack.quantity ? amount : oldStack.quantity;
        storageSO.AddItem(newStack);
        playerInventory.RemoveItem(slot, newStack.quantity);
        if (playerInventory.GetItemAt(slot).quantity == 0)
        {
            inventoryMainPage.ResetSelection();
        }
    }*/
    void Deposit(int slot, int amount)
    {
        Inventory.Model.InventoryItem newStack = new();
        Inventory.Model.InventoryItem oldStack = playerInventory.GetItemAt(slot);
        int quantity = amount <= oldStack.quantity ? amount : oldStack.quantity;
        newStack.item = oldStack.item;
        newStack.quantity = quantity;
        bool foundExistingSlot = false;
        for (int i = 0; i < storageSO.inventoryItems.Count; i++)
        {
            if (storageSO.inventoryItems[i].item == oldStack.item)
            {
                foundExistingSlot = true;
                storageSO.inventoryItems[i] = storageSO.inventoryItems[i].ChangeQuantity(storageSO.inventoryItems[i].quantity + quantity);
            }
        }
        if (!foundExistingSlot)
        {
            storageSO.AddItem(newStack);
        }
        playerInventory.RemoveItem(slot, newStack.quantity);
        if (playerInventory.GetItemAt(slot).quantity == 0)
        {
            inventoryMainPage.ResetSelection();
        }
    }

    void Withdraw(int slot, int amount)
    {
        Inventory.Model.InventoryItem newStack = new();
        Inventory.Model.InventoryItem oldStack = storageSO.GetItemAt(slot);
        int maxStackSize = oldStack.item.MaxStackSize;
        int newStackSize = maxStackSize < amount ? maxStackSize : amount;
        newStackSize = newStackSize < oldStack.quantity ? newStackSize : oldStack.quantity;
        newStack.item = oldStack.item;
        newStack.quantity = newStackSize;
        storageSO.RemoveItem(slot, newStackSize);
        playerInventory.AddItem(newStack);
        if (storageSO.GetItemAt(slot).quantity == 0)
        {
            storageVisuals.DeselectAllItems();
        }
    }
}
