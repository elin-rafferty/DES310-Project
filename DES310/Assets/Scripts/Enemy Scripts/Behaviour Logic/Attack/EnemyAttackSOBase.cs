using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{
    protected EnemyBase enemyBase;
    protected Transform transform;
    protected GameObject gameObject;

    protected GameObject Player;

    public virtual void Initialise(GameObject gameObject, EnemyBase enemyBase)
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
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
