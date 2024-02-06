using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack - None", menuName = "Enemy Logic/Attack Logic/None")]
public class NoAttack : EnemyAttackSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialise(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialise(gameObject, enemyBase);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
