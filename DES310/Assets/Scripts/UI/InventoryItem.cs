using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InventoryItem
{
    public int quantity;
    public ItemSO item;
    public bool isEmpty => item == null;
    public InventoryItem(ItemSO baseItem, int itemQuantity)
    {
        var equippableItemBase = baseItem as EquippableItemSO;
        if (equippableItemBase == null)
        {
            item = baseItem;
        } else
        {
            if (!equippableItemBase.isOriginalSO)
            {
                item = baseItem;
            } else
            {
                item = GameObject.Instantiate(baseItem);
                equippableItemBase = item as EquippableItemSO;
                equippableItemBase.isOriginalSO = false;
                equippableItemBase.weaponProperties = GameObject.Instantiate(equippableItemBase.weaponProperties);
                equippableItemBase.weaponProperties.weaponUpgrades = WeaponUpgrades.CreateInstance("WeaponUpgrades") as WeaponUpgrades;
            }
        }
        quantity = itemQuantity;
    }

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQuantity,
        };
    }
    public static InventoryItem GetEmptyItem() => new InventoryItem
    {
        item = null,
        quantity = 0,
    };
}
