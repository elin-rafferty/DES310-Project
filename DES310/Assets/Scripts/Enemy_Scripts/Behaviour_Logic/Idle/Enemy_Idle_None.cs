using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle - None", menuName = "Enemy Logic/Idle Logic/None")]
public class Enemy_Idle_None : Enemy_Idle_SO_Base
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

        if (enemyBase.rb.bodyType == RigidbodyType2D.Dynamic)
        {
            enemyBase.MoveEnemy(Vector2.zero);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialise(GameObject gameObject, Enemy_Base enemyBase)
    {
        base.Initialise(gameObject, enemyBase);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
