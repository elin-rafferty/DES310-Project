using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private inventoryMainPage inventoryUI;

    [SerializeField] private EventHandler eventHandler;

    [SerializeField]
    private InventorySO inventoryData;

    

    public void Start()
    {
        PrepareUI();
        //inventoryData.Initialize();
        inventoryUI.Show();
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);

        this.inventoryUI.OnDescriptionRequest += HandleDescriptionRequest;
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequired;
    }

    private void HandleItemActionRequired(int itemIndex)
    {
        
    }

    private void HandleDragging(int itemIndex)
    {
        
    }

    private void HandleSwapItems(int itemIndex1, int itemIndex2)
    {
        
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
        inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
                eventHandler.InventoryChangeState.Invoke(true);
            }
            else
            {

                inventoryUI.Hide();
                eventHandler.InventoryChangeState.Invoke(false);
            }
        }
    }
}
