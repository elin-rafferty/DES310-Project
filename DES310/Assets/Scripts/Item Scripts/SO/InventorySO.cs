using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItemUI> inventoryItems;

    [field: SerializeField]
    public int Size { get; set; } = 10;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItemUI>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

}

[Serializable]
public struct InventoryItem
{
    public int quantity;
    public ItemSO item;


    public bool IsEmpty => item == null;

    public InventoryItemUI ChangeQuantity(int newQuantity)
    {
        return new InventoryItemUI
        {
            itemData = this.item,
            stackSize = newQuantity,
        };
    }
    public static InventoryItemUI GetEmptyItem() 
        => new InventoryItemUI
        {
            itemData = null,
            stackSize = 0,
        };
}

