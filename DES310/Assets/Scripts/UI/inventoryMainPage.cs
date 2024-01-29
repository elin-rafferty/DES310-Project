using System;
using System.Collections;
using System.Collections.Generic;
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


    List<InventoryItem> listOfUiItems = new List<InventoryItem>();

    public Sprite image;
    public int quantity;
    public string title, description;

    private void Awake()
    {
        Hide();
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

    private void HandleShowItemActions(InventoryItem obj)
    {
        
    }

    private void HandleEndDrad(InventoryItem obj)
    {
        
    }

    private void HandleSwap(InventoryItem obj)
    {
        
    }

    private void HandleBeginDrag(InventoryItem obj)
    {
        
    }

    private void HandleItemSelection(InventoryItem obj)
    {
        itemDescription.SetDescription(image, title, description);
        listOfUiItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfUiItems[0].SetData(image, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
