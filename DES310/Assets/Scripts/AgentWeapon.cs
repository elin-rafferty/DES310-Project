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

    public void Start()
    {
        overheatSlider.value = 0;
        if (GetComponent<AgentWeapon>().weapon != null)
        {
            overheatSlider.maxValue = GetComponent<AgentWeapon>().weapon.weaponProperties.overheatCapacity;
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
        overheatSlider.maxValue = weapon.weaponProperties.overheatCapacity;
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