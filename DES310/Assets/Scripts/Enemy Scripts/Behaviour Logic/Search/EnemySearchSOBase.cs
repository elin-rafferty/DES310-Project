using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySearchSOBase : ScriptableObject
{
    protected EnemyBase enemyBase;
    protected Transform transform;
    protected GameObject gameObject;

    protected GameObject Player;
    protected Rigidbody2D rb;

    protected Path path;
    protected Seeker seeker;

    protected float nextWaypointDistance = 1.0f;
    protected int currentWaypoint = 0;
    protected bool reachedEndOfPath = false;

    public virtual void Initialise(GameObject gameObject, EnemyBase enemyBase)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemyBase = enemyBase;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void DoEnterLogic() 
    {
        //Debug.Log("SEARCH"); 
        seeker = enemyBase.GetComponent<Seeker>();
        rb = enemyBase.GetComponent<Rigidbody2D>();
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() 
    {
        if (enemyBase.IsAggro && enemyBase.IsLineOfSight)
        {
            enemyBase.StateMachine.ChangeState(enemyBase.CHASEState);
        }
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }

    public void OnPathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            path = newPath;
            currentWaypoint = 0;
        }
    }

    public void UpdatePath(Vector3 start, Vector3 end)
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(start, end, OnPathComplete);
        }
    }
}
