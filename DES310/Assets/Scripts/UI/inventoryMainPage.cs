using Inventory.Model;
using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class inventoryMainPage : MonoBehaviour
    {
        [SerializeField]
        private InventoryItemUI itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private InventoryDescription itemDescription;

        [SerializeField]
        private MouseFollow mouseFollow;

        List<InventoryItemUI> listOfUiItems = new List<InventoryItemUI>();

        private int currentlyDraggedItemIndex = 1;
        private InventoryItemUI inventoryItemUI;

        public event Action<int> OnDescriptionRequest, OnItemActionRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        [SerializeField]
        public ItemAction itemAction;

        private void Awake()
        {
            Hide();
            mouseFollow.Toggle(false);
            itemDescription.ResetDescription();
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                InventoryItemUI UIItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                UIItem.transform.SetParent(contentPanel);
                listOfUiItems.Add(UIItem);

                UIItem.OnItemClicked += HandleItemSelection;
                UIItem.OnItemBeginDrag += HandleBeginDrag;
                UIItem.OnItemDropped += HandleSwap;
                UIItem.OnItemEndDrag += HandleEndDrad;
                UIItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfUiItems)
            {
                if (item != null)
                {
                    item.ResetData();
                    item.Deselect();
                }
               
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfUiItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUiItems.Count > itemIndex)
            {
                if (listOfUiItems[itemIndex] != null)
                {
                    listOfUiItems[itemIndex].SetData(itemImage, itemQuantity);
                }
                
            }
        }

        private void HandleShowItemActions(InventoryItemUI inventoryItemUI)
        {
            int index = listOfUiItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrad(InventoryItemUI inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(InventoryItemUI inventoryItemUI)
        {
            int index = listOfUiItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        }

        private void ResetDraggedItem()
        {
            mouseFollow.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(InventoryItemUI inventoryItemUI)
        {
            int index = listOfUiItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;

            OnStartDragging?.Invoke(index);

        }
        public void CreatedDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollow.Toggle(true);
            mouseFollow.SetData(sprite, quantity);
        }

        private void HandleItemSelection(InventoryItemUI inventoryItemUI)
        {
            int index = listOfUiItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequest?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction (string actionName, Action performAction)
        {
            itemAction.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            itemAction.Toggle(true);
            itemAction.transform.position = listOfUiItems[itemIndex].transform.position; 
        }

        private void DeselectAllItems()
        {
            foreach (InventoryItemUI item in listOfUiItems)
            {
                item.Deselect();
            }
            itemAction.Toggle(false);
        }

        public void Hide()
        {
            itemAction.Toggle(false);
            gameObject.SetActive(false);
            ResetDraggedItem();
        }
    }
}