using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCHASEState : EnemyState
{
    private GameObject Player;
    private float speed = 2;

    public EnemyCHASEState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!enemyBase.IsAggro)
        {
            enemyBase.StateMachine.ChangeState(enemyBase.IDLEState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Chase(Player);
    }

    #region State Specific Functions

    private void Chase(GameObject target)
    {
        // Follow Target
        Vector2 direction = target.transform.position - enemyBase.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyBase.MoveEnemy(direction * speed);
        enemyBase.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    #endregion
}
