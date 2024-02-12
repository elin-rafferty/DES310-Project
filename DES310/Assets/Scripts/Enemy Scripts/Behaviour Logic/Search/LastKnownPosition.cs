using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pathfinding.SimpleSmoothModifier;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "Search - Last Known Position", menuName = "Enemy Logic/Search Logic/Last Known Position")]
public class LastKnownPosition : EnemySearchSOBase
{
    private Transform playerLastKnownPosition;
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

        playerLastKnownPosition = playerTransform;

        UpdatePath();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
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
            enemyBase.StateMachine.ChangeState(enemyBase.IDLEState);
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Move Enemy in direction of path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        // Look in direction of movement
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, smoothTime);

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
        if (!newPath.error)
        {
            path = newPath;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, playerLastKnownPosition.position, OnPathComplete);
        }
    }
}
