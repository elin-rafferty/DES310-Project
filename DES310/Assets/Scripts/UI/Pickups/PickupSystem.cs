//using Inventory.Model;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PickupSystem : MonoBehaviour
//{
//    [field: SerializeField]
//    public InventorySO inventoryData;

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        Item item = collision.GetComponent<Item>();
//        if (item != null)
//        {
//            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
//            if (reminder == 0)
//                item.DestoryItem();
//            else
//                item.Quantity = reminder;
//        }
//    }
//}
