using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase - None", menuName = "Enemy Logic/Chase Logic/None")]
public class No_Chase : Enemy_Chase_SO_Base
{
    public override void DoAnimationTriggerEventLogic(Enemy_Base.AnimationTriggerType triggerType)
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

        if (enemyBase.IsWithinAttackRange && enemyBase.IsLineOfSight)
        {
            enemyBase.StateMachine.ChangeState(enemyBase.ATTACKState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
