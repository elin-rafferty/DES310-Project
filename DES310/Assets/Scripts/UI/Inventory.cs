using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        Item.OnItemCollected += Add;  
    }

    private void OnDisabled()
    {
        Item.OnItemCollected -= Add;
    }

    public void Add(ItemData ItemData)
    {
        if (itemDictionary.TryGetValue(ItemData, out InventoryItem item))
        {
            item.AddToStack();
            Debug.Log($"{item.itemData.displayName} total stack is now {item.stackSize}");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(ItemData);
            inventory.Add(newItem);
            itemDictionary.Add(ItemData, newItem);
            Debug.Log("You added this item to your inventory for the first time!");
        }
    }

    public void Remove(ItemData ItemData)
    {
        if (itemDictionary.TryGetValue(ItemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(ItemData);
            }
        }
    }
}
