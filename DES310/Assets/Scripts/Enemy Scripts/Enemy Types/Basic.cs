using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : EnemyBase
{
    public override void SetModifiers(ModifierBehaviour modifier)
    {
        base.SetModifiers(modifier);

        MaxHealth *= modifier.walkerHealthMultiplier;
        meleeDamage *= modifier.walkerDamageMultiplier;
        speed *= modifier.walkerSpeedMultiplier;
        attackDelay /= modifier.walkerAttackSpeedMultiplier;
        attackRange *= modifier.walkerAttackRangeMultiplier;
        aggroRange *= modifier.walkerAggroRangeMultiplier;
    }
}
