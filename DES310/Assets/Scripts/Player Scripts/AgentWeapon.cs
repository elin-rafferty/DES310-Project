using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    public EquippableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private Slider overheatSlider;

    [SerializeField]
    private PersistentVariables persistentVariables;

    public void Start()
    {
        if (persistentVariables.equippedItem != null)
        {
            weapon = persistentVariables.equippedItem;
        }
        if (weapon != null)
        {
            overheatSlider.maxValue = weapon.weaponProperties.overheatCapacity * weapon.weaponProperties.weaponUpgrades.overheatCapacityModifier;
        }
        else
        {
            overheatSlider.maxValue = 10;
        }
        overheatSlider.value = overheatSlider.maxValue;
    }

    public void SetWeapon(EquippableItemSO weaponItemSO, bool isTheSameAsCurrentlyEquipped = false)
    {
        if (weapon != null && !isTheSameAsCurrentlyEquipped)
        {
            inventoryData.AddItem(weapon, 1);
        }
        this.weapon = weaponItemSO;
        overheatSlider.maxValue = weapon.weaponProperties.overheatCapacity * weapon.weaponProperties.weaponUpgrades.overheatCapacityModifier;
        overheatSlider.value = overheatSlider.maxValue;
        persistentVariables.equippedItem = weapon;
    }
}