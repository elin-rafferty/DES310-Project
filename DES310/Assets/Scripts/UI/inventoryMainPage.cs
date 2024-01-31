using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

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

    public event Action<int> OnDescriptionRequest, OnItemActionRequested, OnStartDragging;

    public event Action<int, int> OnSwapItems;



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

            listOfUiItems.Add(UIItem);
            UIItem.OnItemClicked += HandleItemSelection;
            UIItem.OnItemBeginDrag += HandleBeginDrag;
            UIItem.OnItemDropped += HandleSwap;
            UIItem.OnItemEndDrag += HandleEndDrad;
            UIItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listOfUiItems.Count > itemIndex)
        {
            listOfUiItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleShowItemActions(InventoryItemUI inventoryItem)
    {

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

    private void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (InventoryItemUI item in listOfUiItems)
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
