using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class inventoryMainPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventoryDescription itemDescription;

    [SerializeField]
    private MouseFollow mouseFollow;


    List<InventoryItem> listOfUiItems = new List<InventoryItem>();


    private int currentlyDraggedItemIndex = 1;

    public event Action<int> OnDescriptionRequest, OnItemActionRequested, OnStartDragging;

    public event Action<int, int> OnSwapItems;



    private void Awake()
    {
        Hide();
        mouseFollow.Toggle(false);
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI (int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem uiItem = 
                Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUiItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDropped += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrad;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listOfUiItems.Count > itemIndex)
        {
            listOfUiItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleShowItemActions(InventoryItem inventoryItem)
    {
        
    }

    private void HandleEndDrad(InventoryItem inventoryItemUI)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(InventoryItem inventoryItemUI)
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

    private void HandleBeginDrag(InventoryItem inventoryItemUI)
    {
        int index = listOfUiItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;

        OnStartDragging?.Invoke(index);
    }

    public void CreatedDraggedItem (Sprite sprite, int quantity)
    {
        mouseFollow.Toggle(true);
        mouseFollow.SetData(sprite, quantity);  
    }

    private void HandleItemSelection(InventoryItem inventoryItemUI)
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

    private void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (InventoryItem item in listOfUiItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

}
