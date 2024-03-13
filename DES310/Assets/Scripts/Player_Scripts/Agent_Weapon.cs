using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agent_Weapon : MonoBehaviour
{
    [SerializeField]
    public Equippable_Item_SO weapon;

    [SerializeField]
    private Inventory_SO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    [SerializeField]
    private Slider overheatSlider;

    [SerializeField]
    private Persistent_Variables persistentVariables;

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

    public void SetWeapon(Equippable_Item_SO weaponItemSO, List<ItemParameter> itemState, bool isTheSameAsCurrentlyEquipped = false)
    {
        if (weapon != null && !isTheSameAsCurrentlyEquipped)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }
        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
        overheatSlider.maxValue = weapon.weaponProperties.overheatCapacity * weapon.weaponProperties.weaponUpgrades.overheatCapacityModifier;
        overheatSlider.value = overheatSlider.maxValue;
        persistentVariables.equippedItem = weapon;
    }

    private void ModifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemparameter = parameter.itemparameter,
                    value = newValue
                };
            }
        }
    }
}