using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy_Item : MonoBehaviour
{
    [SerializeField] private Inventory_SO playerInventory;
    public Item_SO itemToBuy;
    public int quantity = 1;
    public Item_SO currencyItem;
    public int cost = 1;

    public void PurchaseItem()
    {
        if (playerInventory.RemoveItem(currencyItem, cost)) 
        {
            Sound_Manager.instance.PlaySound(Sound_Manager.SFX.PurchaseSuccessful, transform, 1f);
            InventoryItem purchasedItem = new();
            purchasedItem.quantity = quantity;
            purchasedItem.item = itemToBuy;
            playerInventory.AddItem(purchasedItem);
        } else
        {
            Sound_Manager.instance.PlaySound(Sound_Manager.SFX.PurchaseUnsuccessful, transform, 1f);
        }
    }
}
