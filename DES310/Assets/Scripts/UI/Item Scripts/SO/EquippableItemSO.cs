using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu (menuName = "Items/Equippable Item SO")]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        public WeaponProperties weaponProperties;

        //public AudioClip actionSFX { get; private set; }
        public bool isOriginalSO = true;

        public bool PerformAction(GameObject character)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this);
                inventoryMainPage inventoryMainPage = character.GetComponentInChildren<inventoryMainPage>();
                if (inventoryMainPage != null)
                {
                    inventoryMainPage.HideItemAction();
                }
                return true;
            }
            return false;
        }
    }
}