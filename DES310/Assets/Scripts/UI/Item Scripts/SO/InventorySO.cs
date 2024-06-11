using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;



namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        public List<InventoryItem> inventoryItems;
        [SerializeField]
        private Item itemPickupPrefab;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
        public EventHandler eventHandler;

        public bool resetOnDeath = true;

        public void OnEnable()
        {
            if (resetOnDeath)
            {
                eventHandler.PlayerDeath.AddListener(WipeInventory);
            }
            WipeInventory();
        }

        public void WipeInventory()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public bool Initialize()
        {
            bool returnVal = true;
            if (inventoryItems != null)
            {
                returnVal = false;
            }
            else
            {
                WipeInventory();
            }
            return returnVal;
        }

        public int AddItem(ItemSO item, int quantity)
        {
            int initialQuantity = quantity;
            if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }
                    //InformAboutChange();
                    //return quantity;
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && quantity != 0)
            {
                Item newPickup = Instantiate(itemPickupPrefab, player.transform.position + new Vector3(0, 0, 1), Quaternion.identity);
                newPickup.SetItem(item, quantity);
            }
            item.onItemAddedToInventory(initialQuantity);
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItem newItem = new InventoryItem(item, quantity);

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInventoryFull()
            => inventoryItems.Where(item => item.isEmpty).Any() == false;


        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                    continue;
                if (inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake =
                        inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].isEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex]
                        .ChangeQuantity(reminder);

                InformAboutChange();
            }
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;

        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];

        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            InventoryItem item1 = inventoryItems[itemIndex1];
            inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
            inventoryItems[itemIndex2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public bool HasItem(ItemSO item, int amountToFind = 1)
        {
            foreach (InventoryItem inventoryItem in inventoryItems)
            {
                if (inventoryItem.item == null)
                {
                    continue;
                }
                if (item.Name == inventoryItem.item.Name)
                {
                    amountToFind -= inventoryItem.quantity;
                    if (amountToFind <= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemoveItem(ItemSO item, int count = 1)
        {
            int amountToRemove = count;
            if (HasItem(item, count)) {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i].item == null)
                    {
                        continue;
                    }
                    if (item.Name == inventoryItems[i].item.Name)
                    {
                        int temp = inventoryItems[i].quantity > amountToRemove ? amountToRemove : inventoryItems[i].quantity;
                        amountToRemove -= temp;
                        RemoveItem(i, temp);
                        if (amountToRemove == 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            } else { return false; }
        }
    }
}
