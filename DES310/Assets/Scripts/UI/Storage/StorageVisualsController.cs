using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageVisualsController : MonoBehaviour
{
    [SerializeField] List<Inventory.UI.InventoryItemUI> slots = new();
    [SerializeField] Inventory.Model.InventorySO storageSO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        ResetAllItems();
        for (int i = 0; i < storageSO.Size; i++)
        {
            slots[i].SetData(storageSO.GetItemAt(i).item.ItemImage, storageSO.GetItemAt(i).quantity);
        }
    }

    void ResetAllItems()
    {
        foreach (var slot in slots)
        {
            if (slot != null)
            {
                slot.ResetData();
                slot.Deselect();
            }
        }
    }
}
