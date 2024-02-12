using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseSOBase : ScriptableObject
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
        //Debug.Log("CHASE"); 
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() 
    {
        if (!enemyBase.IsAggro || !enemyBase.IsLineOfSight)
        {
            enemyBase.StateMachine.ChangeState(enemyBase.SEARCHState);
        }
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
