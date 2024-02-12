using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase - Follow", menuName = "Enemy Logic/Chase Logic/Follow")]
public class EnemyChaseFollowPlayer : EnemyChaseSOBase
{
    private Transform target;
    private float speed = 3;
    private float smoothTime = 0.25f;
    private float rotateSpeed;

    Path path;
    private float nextWaypointDistance = 1.0f;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private float pathUpdateTime = 0.5f;
    private float timer;

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

        UpdatePath();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();

        enemyBase.IsLineOfSight = false;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // Update path every interval
        if(timer >= pathUpdateTime)
        {
            timer = 0f;
            UpdatePath();
        } 
        else
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
        float angle;

        if (enemyBase.IsLineOfSight)
        {
            // Look at Player
            Vector2 playerDirection = ((Vector2)Player.transform.position - rb.position).normalized;
            float targetAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, smoothTime);
        } else
        {
            // Look in direction of movement
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, smoothTime);
        }
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
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
}
