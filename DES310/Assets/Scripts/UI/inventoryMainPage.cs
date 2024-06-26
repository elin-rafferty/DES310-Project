using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public int selectedSlot = -1;

        private void Awake()
        {
            Hide();
            //mouseFollow.Toggle(false);
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
                UIItem.OnMouseHover += HandleItemSelection;
                //UIItem.OnMouseStopHover += ResetSelection;
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
            selectedSlot = -1;
        }

        private void HandleBeginDrag(InventoryItemUI inventoryItemUI)
        {
            int index = listOfUiItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                selectedSlot = index;
                return;
            }
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
            {
                selectedSlot = index;
                return;
            }
            OnDescriptionRequest?.Invoke(index);
            if (!inventoryItemUI.empty)
            {
                selectedSlot = index;
            } else
            {
                selectedSlot = -1;
            }
            StorageVisualsController storageVisuals = FindFirstObjectByType<StorageVisualsController>();
            if (storageVisuals != null)
            {
                storageVisuals.DeselectAllItems();
            }
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
            selectedSlot = -1;
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

        public void DeselectAllItems()
        {
            foreach (InventoryItemUI item in listOfUiItems)
            {
                item.Deselect();
            }
            itemAction.Toggle(false);
            selectedSlot = -1;
        }

        public void Hide()
        {
            itemAction.Toggle(false);
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        public void HideItemAction()
        {
            itemAction.Toggle(false);
        }
    }
}