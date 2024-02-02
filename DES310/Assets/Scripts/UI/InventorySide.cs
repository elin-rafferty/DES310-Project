using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySide : MonoBehaviour
{
    public List<InventoryItemUI> inventory = new List<InventoryItemUI>();
    private Dictionary<ItemSO, InventoryItemUI> itemDictionary = new Dictionary<ItemSO, InventoryItemUI>();

    private void OnEnable()
    {
        
    }

    private void OnDisabled()
    {
       
    }

    public void Add(ItemSO ItemData)
    {
        if (itemDictionary.TryGetValue(ItemData, out InventoryItemUI item))
        {
            item.AddToStack();
            Debug.Log($"{item.item.Name} total stack is now {item.quantity}");
        }
        else
        {
            InventoryItemUI newItem = new InventoryItemUI(ItemData);
            inventory.Add(newItem);
            itemDictionary.Add(ItemData, newItem);
            Debug.Log("You added this item to your inventory for the first time!");
        }
    }

    public void Remove(ItemSO ItemData)
    {
        if (itemDictionary.TryGetValue(ItemData, out InventoryItemUI item))
        {
            item.RemoveFromStack();
            if (item.quantity == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(ItemData);
            }
        }
    }
}
