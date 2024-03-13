using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle - Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]
public class Enemy_Idle_Random_Wander : Enemy_Idle_SO_Base
{
    private float RandomMovementRange = 2f;
    private Vector3 targetPos;
    private Vector3 direction;

    public override void DoAnimationTriggerEventLogic(Enemy_Base.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        targetPos = GetRandomPointInCircle();
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

        direction = (targetPos - enemyBase.transform.position).normalized;

        enemyBase.MoveEnemy(direction * RandomMovementRange);

        if ((enemyBase.transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            targetPos = GetRandomPointInCircle();
        }
    }

    public override void Initialise(GameObject gameObject, Enemy_Base enemyBase)
    {
        base.Initialise(gameObject, enemyBase);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }


    private Vector3 GetRandomPointInCircle()
    {
        return enemyBase.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
    }
}
