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
    public class inventory_Main_Page : MonoBehaviour
    {
        [SerializeField]
        private Inventory_Item_UI itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private Inventory_Description itemDescription;

        [SerializeField]
        private Mouse_Follow mouseFollow;

        List<Inventory_Item_UI> listOfUiItems = new List<Inventory_Item_UI>();

        private int currentlyDraggedItemIndex = 1;
        private Inventory_Item_UI inventoryItemUI;

        public event Action<int> OnDescriptionRequest, OnItemActionRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        [SerializeField]
        public Item_Action itemAction;

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
                Inventory_Item_UI UIItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
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

        private void HandleShowItemActions(Inventory_Item_UI inventoryItemUI)
        {
            int index = listOfUiItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrad(Inventory_Item_UI inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(Inventory_Item_UI inventoryItemUI)
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

        private void HandleBeginDrag(Inventory_Item_UI inventoryItemUI)
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

        private void HandleItemSelection(Inventory_Item_UI inventoryItemUI)
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
            foreach (Inventory_Item_UI item in listOfUiItems)
            {
                item.Deselect();
            }
            itemAction.Toggle(false);
        }

        public void HideItemAction()
        {
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