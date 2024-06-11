using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LootDrops : MonoBehaviour
{
    [SerializeField] Item pickupPrefab;
    [SerializeField] LootTable lootTable;
    public void DropItems()
    {
        List<InventoryItem> itemsToDrop = lootTable.getLoot();
        foreach (InventoryItem item in itemsToDrop)
        {
            Item pickup = Instantiate(pickupPrefab, gameObject.transform.position, Quaternion.identity);
            pickup.SetItem(item.item, item.quantity);
        }
    }
}
