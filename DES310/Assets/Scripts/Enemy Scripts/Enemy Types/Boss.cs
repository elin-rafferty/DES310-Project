using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBase
{
    [SerializeField] Slider bossHealthSlider;

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);

        bossHealthSlider.value = CurrentHealth;
        if (bossHealthSlider.value <= 0 ) 
        {
            bossHealthSlider.gameObject.SetActive(false);
        }
    }
}
