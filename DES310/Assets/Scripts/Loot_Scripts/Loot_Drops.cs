using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Loot_Drops : MonoBehaviour
{
    [SerializeField] Item pickupPrefab;
    [SerializeField] Loot_Table lootTable;
    public void DropItems()
    {
        List<InventoryItem> itemsToDrop = lootTable.getLoot();
        foreach (InventoryItem item in itemsToDrop)
        {
            Item pickup = Instantiate(pickupPrefab, gameObject.transform.position, Quaternion.identity);
            pickup.Quantity = item.quantity;
            pickup.InventoryItem = item.item;
        }
    }
}
