using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : EnemyBase
{
    public Transform weaponTransfom;

    public override void SetModifiers(ModifierBehaviour modifier)
    {
        base.SetModifiers(modifier);

        MaxHealth *= modifier.spitterHealthMultiplier;
        rangedDamage *= modifier.spitterDamageMultiplier;
        attackDelay /= modifier.spitterAttackSpeedMultiplier;
        attackRange *= modifier.spitterAttackRangeMultiplier;
        aggroRange *= modifier.spitterAggroRangeMultiplier;
    }
}
