using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprinter : EnemyBase
{
    public override void SetModifiers(ModifierBehaviour modifier)
    {
        base.SetModifiers(modifier);

        MaxHealth *= modifier.sprinterHealthMultiplier;
        meleeDamage *= modifier.sprinterDamageMultiplier;
        speed *= modifier.sprinterSpeedMultiplier;
        attackDelay /= modifier.sprinterAttackSpeedMultiplier;
        attackRange *= modifier.sprinterAttackRangeMultiplier;
        aggroRange *= modifier.sprinterAggroRangeMultiplier;
    }
}
