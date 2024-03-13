using Cinemachine;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy_Base
{
    [SerializeField] Slider bossHealthSlider;
    [SerializeField] Horizontal_Door door;

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);

        bossHealthSlider.value = CurrentHealth;
        if (bossHealthSlider.value <= 0 ) 
        {
            bossHealthSlider.gameObject.SetActive(false);
            GetComponent<Loot_Drops>().DropItems();
            door.Unlock();
        }
    }
}
