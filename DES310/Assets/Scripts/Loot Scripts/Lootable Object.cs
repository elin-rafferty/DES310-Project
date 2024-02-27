using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SearchService;

public class LootableObject : MonoBehaviour
{
    public LootTable lootTable;
    public Item pickupPrefab;
    bool trigger = false;
    bool looted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger true on collision
            trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger false on collision end
            trigger = false;
        }
    }

    void Update()
    {
        // Pick up item
        if (Input.GetKeyDown(KeyCode.E) && trigger && !looted)
        {
            List<InventoryItem> itemsToDrop = lootTable.getLoot();
            foreach (InventoryItem item in itemsToDrop)
            {
                Item pickup = Instantiate(pickupPrefab, gameObject.transform.position, Quaternion.identity);
                pickup.Quantity = item.quantity;
                pickup.InventoryItem = item.item;
            }
            looted = true;
        }
    }
}
