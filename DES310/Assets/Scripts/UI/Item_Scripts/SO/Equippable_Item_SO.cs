using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu (menuName = "Items/Equippable Item SO")]
    public class Equippable_Item_SO : Item_SO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        public Weapon_Properties weaponProperties;


        //public AudioClip actionSFX { get; private set; }


        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            Agent_Weapon weaponSystem = character.GetComponent<Agent_Weapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ?
                    DefaultParametersList : itemState);
                inventory_Main_Page inventoryMainPage = character.GetComponentInChildren<inventory_Main_Page>();
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