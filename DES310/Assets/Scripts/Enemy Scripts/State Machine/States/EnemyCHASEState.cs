using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCHASEState : EnemyState
{
    public EnemyCHASEState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {
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

        enemyBase.MoveEnemy(Vector2.zero);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    #region State Specific Functions

    #endregion
}
