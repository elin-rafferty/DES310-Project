using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State
{
    protected Enemy_Base enemyBase;
    protected Enemy_State_Machine enemyStateMachine;

    public Enemy_State(Enemy_Base enemyBase, Enemy_State_Machine enemyStateMachine)
    {
        this.enemyBase = enemyBase;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Enemy_Base.AnimationTriggerType triggerType) { }
}
