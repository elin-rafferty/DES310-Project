using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase - Boss", menuName = "Enemy Logic/Chase Logic/Boss")]
public class Boss_Chase : Enemy_Chase_SO_Base
{
    private Transform target;

    private float angleSmoothTime = 0.25f;
    private float rotateSpeed;
    private float velocitySmoothTime = 0.005f;
    private float refSpeedX;
    private float refSpeedY;
    Vector2 currentDirection = new(0, 0);

    private float pathUpdateTime = 0.25f;
    private float timer;
    private float attackCooldownTimer;

    public override void DoAnimationTriggerEventLogic(Enemy_Base.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        target = Player.transform;
        UpdatePath(rb.position, target.position);
        attackCooldownTimer = Random.Range(3, 8);
        enemyBase.enemyTimer = 0.5f;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        //Debug.Log("Chase State");

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

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        else
        {
            enemyBase.StateMachine.ChangeState(enemyBase.ATTACKState);
        }

        if (enemyBase.enemyTimer > 0)
        {
            enemyBase.enemyTimer -= Time.deltaTime;
        }
        else
        {
            enemyBase.eventHandler.ShakeCamera.Invoke(0, 0);
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
            angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, angleSmoothTime);
        }
        else
        {
            // Look in direction of movement
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, angleSmoothTime);
        }
        // Direction Damping
        direction.x = Mathf.SmoothDamp(currentDirection.x, direction.x, ref refSpeedX, velocitySmoothTime);
        direction.y = Mathf.SmoothDamp(currentDirection.y, direction.y, ref refSpeedY, velocitySmoothTime);
        enemyBase.MoveEnemy(direction * enemyBase.speed);
        enemyBase.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        // If reached current waypoint increment waypoint 
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
            currentDirection = direction;
        }
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
