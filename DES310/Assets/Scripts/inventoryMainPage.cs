using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryMainPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    List<InventoryItem> listOfUiItems = new List<InventoryItem>();

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
        Debug.Log(obj.name);
    }

    private void HandleItemSelection(InventoryItem obj)
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
