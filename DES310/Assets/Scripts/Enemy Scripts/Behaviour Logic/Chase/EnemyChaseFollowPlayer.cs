using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase - Follow", menuName = "Enemy Logic/Chase Logic/Follow")]
public class EnemyChaseFollowPlayer : EnemyChaseSOBase
{
    private Transform target;
    private float speed = 3;
    private float nextWaypointDistance = 0.5f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    float pathUpdateTime = 0.5f;
    float timer;

    Seeker seeker;
    Rigidbody2D rb;
    Transform playerTransform;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        seeker = enemyBase.GetComponent<Seeker>();
        rb = enemyBase.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        target = playerTransform;
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

        // Update path every interval
        if(timer >= pathUpdateTime)
        {
            timer = 0f;
            UpdatePath();
        } else
        {
            timer += Time.deltaTime;
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        if (path == null)
        {
            return;
        }

        // Check for end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        
        // Move Enemy in direction of path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyBase.MoveEnemy(direction * speed);
        enemyBase.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        // If reached current waypoint increment waypoint 
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    public override void Initialise(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialise(gameObject, enemyBase);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private void OnPathComplete(Path newPath)
    {
        if(!newPath.error)
        {
            path = newPath;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        Debug.Log("update path");
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
}
