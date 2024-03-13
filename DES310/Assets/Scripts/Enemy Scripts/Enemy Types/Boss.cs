using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBase
{
    [SerializeField] Slider bossHealthSlider;
    [SerializeField] HorizontalDoor door;

    public override void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Die();
        }

        if (damageReduction == 0.9)
        {
            SoundManager.instance.PlaySound(SoundManager.SFX.LaserRebound, transform, 1f);
        }
        else
        {
            SoundManager.instance.PlaySound(SoundManager.SFX.EnemyHit, transform, 1f);
        }

        bossHealthSlider.value = CurrentHealth;
        if (bossHealthSlider.value <= 0 ) 
        {
            bossHealthSlider.gameObject.SetActive(false);
            GetComponent<LootDrops>().DropItems();
            door.Unlock();
        }
    }
}
