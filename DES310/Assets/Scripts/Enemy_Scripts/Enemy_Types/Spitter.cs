using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy_Base
{
    public Transform weaponTransfom;
    public Transform tetherTransform;

    public override void SetModifiers(Modifier_Behaviour modifier)
    {
        base.SetModifiers(modifier);

        MaxHealth *= modifier.spitterHealthMultiplier;
        rangedDamage *= modifier.spitterDamageMultiplier;
        attackDelay /= modifier.spitterAttackSpeedMultiplier;
        attackRange *= modifier.spitterAttackRangeMultiplier;
        aggroRange *= modifier.spitterAggroRangeMultiplier;
    }
}
