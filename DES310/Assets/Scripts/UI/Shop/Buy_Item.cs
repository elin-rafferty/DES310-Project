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
        if (playerInventory.RemoveItem(currencyItem, cost)) 
        {
            SoundManager.instance.PlaySound(SoundManager.SFX.PurchaseSuccessful, transform, 1f);
            InventoryItem purchasedItem = new();
            purchasedItem.quantity = quantity;
            purchasedItem.item = itemToBuy;
            playerInventory.AddItem(purchasedItem);
        } else
        {
            SoundManager.instance.PlaySound(SoundManager.SFX.PurchaseUnsuccessful, transform, 1f);
        }
    }
}
