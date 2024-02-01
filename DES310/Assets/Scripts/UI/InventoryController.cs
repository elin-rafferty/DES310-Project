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
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        inventoryData.Initialize();
        inventoryUI.Show();
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
