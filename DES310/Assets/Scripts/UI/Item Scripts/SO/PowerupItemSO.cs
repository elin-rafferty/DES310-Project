using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Items/Powerup Item SO")]
    public class PowerupItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Use";
        public Buff buffToApply;
        public ActiveBuffs activeBuffs;

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            if (activeBuffs != null)
            {
                activeBuffs.ApplyBuff(buffToApply.type, buffToApply.time);
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