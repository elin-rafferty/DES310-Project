using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageVisualsController : MonoBehaviour
{
    [SerializeField] List<Inventory.UI.InventoryItemUI> slots = new();
    [SerializeField] Inventory.Model.InventorySO storageSO;
    [SerializeField] Inventory.UI.inventoryMainPage inventoryMainPage;
    // Start is called before the first frame update

    public int selectedSlot = -1;
    void Start()
    {
        foreach (var slot in slots)
        {
            slot.OnItemClicked += HandleItemSelection;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisuals();
    }

    private void OnEnable()
    {
        ResetAllItems();
        DeselectAllItems();
    }

    public void UpdateVisuals()
    {
        for (int i = 0; i < storageSO.Size; i++)
        {
            if (!storageSO.GetItemAt(i).isEmpty)
            {
                slots[i].SetData(storageSO.GetItemAt(i).item.ItemImage, storageSO.GetItemAt(i).quantity);
            } else
            {
                slots[i].ResetData();
            }
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

    void HandleItemSelection(Inventory.UI.InventoryItemUI inventoryItem)
    {
        int index = slots.IndexOf(inventoryItem);
        if (index != -1 && index < slots.Count)
        {
            inventoryMainPage.DeselectAllItems();
            DeselectAllItems();
            if (!storageSO.GetItemAt(index).isEmpty)
            {
                inventoryItem.Select();
                selectedSlot = index;
            }
        }
    }

    public void DeselectAllItems()
    {
        foreach (var slot in slots)
        {
            slot.Deselect();
            selectedSlot = -1;
        }
    }
}
