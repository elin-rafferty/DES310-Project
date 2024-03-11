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
    private List<ItemParameter> parametersToModify, itemCurrentState;

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
        overheatSlider.value = 0;
        if (weapon != null)
        {
            overheatSlider.maxValue = weapon.weaponProperties.overheatCapacity * weapon.weaponProperties.weaponUpgrades.overheatCapacityModifier;
        }
        else
        {
            overheatSlider.maxValue = 10;
        }
    }

    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }
        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
        overheatSlider.maxValue = weapon.weaponProperties.overheatCapacity * weapon.weaponProperties.weaponUpgrades.overheatCapacityModifier;
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