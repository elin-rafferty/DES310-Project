using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    [SerializeField] private InventorySO playerInventory;
    public ItemSO itemToBuy;
    public int quantity = 1;
    public ItemSO currencyItem;
    public int cost = 1;

    public void PurchaseItem()
    {
        for (int i = 0; i < playerInventory.Size; i++)
        {
            InventoryItem item = playerInventory.GetItemAt(i);
            if (item.item == currencyItem && item.quantity >= cost)
            {
                playerInventory.RemoveItem(i, cost);
                InventoryItem purchasedItem = new();
                purchasedItem.quantity = quantity;
                purchasedItem.item = itemToBuy;
                playerInventory.AddItem(purchasedItem);
            }
        }
    }
}
