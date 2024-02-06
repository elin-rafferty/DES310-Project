using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase - Follow", menuName = "Enemy Logic/Chase Logic/Follow")]
public class EnemyChaseFollowPlayer : EnemyChaseSOBase
{
    private float speed = 2;

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

        if (!enemyBase.IsAggro)
        {
            enemyBase.StateMachine.ChangeState(enemyBase.IDLEState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        Chase(Player);
    }

    public override void Initialise(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialise(gameObject, enemyBase);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }


    private void Chase(GameObject target)
    {
        // Follow Target
        Vector2 direction = target.transform.position - enemyBase.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyBase.MoveEnemy(direction * speed);
        enemyBase.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
