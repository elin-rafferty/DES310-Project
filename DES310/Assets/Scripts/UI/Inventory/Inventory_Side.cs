using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Side : MonoBehaviour
{
    public List<Inventory_Item_UI> inventory = new List<Inventory_Item_UI>();
    private Dictionary<Item_SO, Inventory_Item_UI> itemDictionary = new Dictionary<Item_SO, Inventory_Item_UI>();

    private void OnEnable()
    {
        
    }

    private void OnDisabled()
    {
       
    }

    public void Add(Item_SO ItemData)
    {
        if (itemDictionary.TryGetValue(ItemData, out Inventory_Item_UI item))
        {
            item.AddToStack();
            Debug.Log($"{item.item.Name} total stack is now {item.quantity}");
        }
        else
        {
            Inventory_Item_UI newItem = new Inventory_Item_UI(ItemData);
            inventory.Add(newItem);
            itemDictionary.Add(ItemData, newItem);
            Debug.Log("You added this item to your inventory for the first time!");
        }
    }

    public void Remove(Item_SO ItemData)
    {
        if (itemDictionary.TryGetValue(ItemData, out Inventory_Item_UI item))
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
