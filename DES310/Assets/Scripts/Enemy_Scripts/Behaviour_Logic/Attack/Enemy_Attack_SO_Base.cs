using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_SO_Base : ScriptableObject
{
    protected Enemy_Base enemyBase;
    protected Transform transform;
    protected GameObject gameObject;

    protected GameObject Player;

    public virtual void Initialise(GameObject gameObject, Enemy_Base enemyBase)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemyBase = enemyBase;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void DoEnterLogic() 
    { 
        //Debug.Log("ATTACK"); 
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() 
    { 
        if (enemyBase.IsLineOfSight)
        {
            if (!enemyBase.IsWithinAttackRange)
            {
                enemyBase.StateMachine.ChangeState(enemyBase.CHASEState);
            }
        }
        else
        {
            enemyBase.StateMachine.ChangeState(enemyBase.SEARCHState);
        }
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy_Base.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
