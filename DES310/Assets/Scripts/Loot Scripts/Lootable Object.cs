using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SearchService;

public class LootableObject : MonoBehaviour
{
    private InputManager inputManager;
    public LootTable lootTable;
    public Item pickupPrefab;
    public float distanceToSpawn = 1.28f;
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

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        // Pick up item
        if (inputManager.GetButtonDown("Interact") && trigger && !looted)
        {
            SoundManager.instance.PlaySound(SoundManager.SFX.CrateOpen, transform, 0.5f);

            List<InventoryItem> itemsToDrop = lootTable.getLoot();
            int posIndex = 0;
            foreach (InventoryItem item in itemsToDrop)
            {
                Vector3 offset = Vector3.zero;
                switch (posIndex % 4)
                {
                    case 0:
                        offset = new Vector3(distanceToSpawn, 0, 0);
                        break;
                    case 1:
                        offset = new Vector3(-distanceToSpawn, 0, 0);
                        break;
                    case 2:
                        offset = new Vector3(0, distanceToSpawn, 0);
                        break;
                    case 3:
                        offset = new Vector3(0, -distanceToSpawn, 0);
                        break;
                }
                Item pickup = Instantiate(pickupPrefab, gameObject.transform.position, Quaternion.identity);
                pickup.hasDestination = true;
                pickup.destination = pickup.transform.position + offset;
                pickup.Quantity = item.quantity;
                pickup.InventoryItem = item.item;
                posIndex++;
            }
            looted = true;
        }
    }
}
