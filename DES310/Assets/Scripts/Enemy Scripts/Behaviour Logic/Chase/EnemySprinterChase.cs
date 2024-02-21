using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

[CreateAssetMenu(fileName = "Chase - Sprinter Chase", menuName = "Enemy Logic/Chase Logic/Sprinter Chase")]
public class EnemySprinterChase : EnemyChaseSOBase
{
    private Transform target;

    Vector3 offset = new Vector3(0, 1, -5);
    private float speed = 9;
    private float runDelayTimer;
    private float smoothTime = 0.25f;
    private float rotateSpeed;

    private float pathUpdateTime = 0.25f;
    private float timer;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        target = Player.transform;
        runDelayTimer = 0;

        // Instantiate Alert Icon
        Destroy(enemyBase.alertObject = Instantiate(enemyBase.alertIconPrefab, enemyBase.transform.position + offset, Quaternion.identity), enemyBase.attackDelay);

        // Initial Path
        UpdatePath(rb.position, target.position);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();

        enemyBase.IsLineOfSight = false;
    }

    public override void DoFrameUpdateLogic()
    {
        // Update path every interval
        if (timer >= pathUpdateTime)
        {
            timer = 0f;
            UpdatePath(rb.position, target.position);
        }
        else
        {
            timer += Time.deltaTime;
        }
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


        // Move Enemy in direction of path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        float angle;

        if (enemyBase.IsLineOfSight)
        {
            // Look at Player
            Vector2 playerDirection = ((Vector2)Player.transform.position - rb.position).normalized;
            float targetAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, smoothTime);
        }
        else
        {
            // Look in direction of movement
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, smoothTime);
        }

        // Start sprinting after time delay
        Debug.Log(runDelayTimer);
        if (runDelayTimer > 1)
        {
            enemyBase.MoveEnemy(direction * speed);
        }
        else
        {
            if (enemyBase.alertObject) enemyBase.alertObject.transform.position = enemyBase.transform.position + offset;
            enemyBase.rb.velocity = Vector2.zero;
            runDelayTimer += Time.deltaTime;
        }

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
}
