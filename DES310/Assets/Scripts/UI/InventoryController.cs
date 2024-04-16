using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UI.inventoryMainPage inventoryUI;

        [SerializeField] private EventHandler eventHandler;

        [SerializeField]
        private Model.InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField] private Item itemPickupPrefab;

        [SerializeField] InventorySwapsHandler inventorySwapsHandler;


        public void Awake()
        {
            PrepareUI();
            PrepareInventoryData();
            inventoryUI.Show();
        }

        private void PrepareInventoryData()
        {
            bool isFresh = inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            if (isFresh)
            {
                foreach (InventoryItem item in initialItems)
                {
                    if (item.IsEmpty)
                        continue;
                    inventoryData.AddItem(item);
                }
            }
            UpdateInventoryUI(inventoryData.GetCurrentInventoryState());
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequest += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequired;
        }

        private void HandleItemActionRequired(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
          
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                if (itemAction == null)
                {
                    inventoryUI.ShowItemAction(itemIndex);
                }
                GameObject inventoryTutorialManager = GameObject.FindGameObjectWithTag("Inventory Tutorial");
                if (inventoryTutorialManager == null)
                {
                    if (inventorySwapsHandler == null)
                    {
                        inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
                    }
                    else
                    {
                        if (inventorySwapsHandler.gameObject.activeSelf)
                        {
                            inventoryUI.AddAction("Store", () => inventorySwapsHandler.DepositAll());
                        }
                        else
                        {
                            inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
                        }
                    }
                }
            }

        }

        private void DropItem(int itemIndex, int quantity)
        {
            Item newPickup = Instantiate(itemPickupPrefab, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, 1), Quaternion.identity);
            newPickup.InventoryItem = inventoryData.GetItemAt(itemIndex).item;
            newPickup.Quantity = inventoryData.GetItemAt(itemIndex).quantity;
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreatedDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, description);
        }

        public string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemparameter.ParameterName} " + $": {inventoryItem.itemState[i].value} / {inventoryItem.item.DefaultParametersList[i].value}");
            }
            return sb.ToString();
        }

        public void Update()
        {
        //    if (Input.GetKeyDown(KeyCode.I))
        //    {
        //        if (inventoryUI.isActiveAndEnabled == false)
        //        {
        //            inventoryUI.Show();
        //            foreach (var item in inventoryData.GetCurrentInventoryState())
        //            {
        //                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        //            }
        //            eventHandler.InventoryChangeState.Invoke(true);
        //        }
        //        else
        //        {
        //            inventoryUI.Hide();
        //            eventHandler.InventoryChangeState.Invoke(false);
        //        }
        //    }
        }
    }
}