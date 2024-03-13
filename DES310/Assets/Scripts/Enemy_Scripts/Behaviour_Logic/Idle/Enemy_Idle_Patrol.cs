using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using static Pathfinding.SimpleSmoothModifier;
using System;

[CreateAssetMenu(fileName = "Idle - Enemy Patrol", menuName = "Enemy Logic/Idle Logic/Enemy Patrol")]
public class Enemy_Idle_Patrol : Enemy_Idle_SO_Base
{
    private float RandomMovementRange = 2f;
    private float smoothTime = 0.25f;
    private float rotateSpeed;
    private float newPathTimer = 0;

    private Vector3 patrolCenter;
    private Vector3 targetPos;
    private Vector3 direction;

    private Path path;
    private Seeker seeker;

    private float nextWaypointDistance = 1.0f;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    public override void DoAnimationTriggerEventLogic(Enemy_Base.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        // Find new patrol point
        targetPos = GetRandomPointInCircle(patrolCenter);

        seeker = enemyBase.GetComponent<Seeker>();
        UpdatePath(enemyBase.rb.position, targetPos);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // Calculate distance to patrol target
        float distance = (targetPos - enemyBase.transform.position).sqrMagnitude;

        // Update path once target reached
        if (distance < 0.25f || newPathTimer > 5) 
        {
            newPathTimer = 0;
            // Find new patrol target
            targetPos = GetRandomPointInCircle(patrolCenter);
            // Update path to new target
            UpdatePath(enemyBase.rb.position, targetPos);
        }

        newPathTimer += Time.deltaTime;
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        // Check path for null
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

        if (newPathTimer > 2)
        {
            // Move Enemy in direction of path
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemyBase.rb.position).normalized;
            enemyBase.MoveEnemy(direction * enemyBase.speed * 0.25f);

            // Look in direction of movement
            float angle;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, smoothTime);

            enemyBase.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            // If reached current waypoint increment waypoint 
            float distance = Vector2.Distance(enemyBase.rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
    }

    public override void Initialise(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialise(gameObject, enemyBase);

        patrolCenter = enemyBase.transform.position;
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private Vector3 GetRandomPointInCircle(Vector3 center)
    {
        Vector3 target = center + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
        float distance = (target - enemyBase.transform.position).sqrMagnitude;

        if (distance < 3)
        {
            return GetRandomPointInCircle(center);
        }

        return target;
    }

    private void OnPathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            path = newPath;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath(Vector3 start, Vector3 end)
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(start, end, OnPathComplete);
        }
    }
}
