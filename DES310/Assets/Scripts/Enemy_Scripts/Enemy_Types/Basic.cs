using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : Enemy_Base
{
    public override void SetModifiers(Modifier_Behaviour modifier)
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
