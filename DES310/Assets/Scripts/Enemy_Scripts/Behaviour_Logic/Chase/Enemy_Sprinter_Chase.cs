using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

[CreateAssetMenu(fileName = "Chase - Sprinter Chase", menuName = "Enemy Logic/Chase Logic/Sprinter Chase")]
public class Enemy_Sprinter_Chase : Enemy_Chase_SO_Base
{
    private Transform target;

    Vector3 offset = new Vector3(0, 1, -5);
    private float runDelayTimer;
    private float angleSmoothTime = 0.25f;
    private float rotateSpeed;
    private float velocitySmoothTime = 0.05f;
    private float refSpeedX;
    private float refSpeedY;
    Vector2 currentDirection = new(0, 0);

    private float pathUpdateTime = 0.25f;
    private float timer;

    private float dodgeTimer = 0;
    private float dodgeCooldownTimer = 2;
    private bool canDodge = false;
    Vector2 dodgeDirection = new(0, 0);

    public override void DoAnimationTriggerEventLogic(Enemy_Base.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        target = Player.transform;
        runDelayTimer = 0;
        dodgeCooldownTimer = 0;

        // Play Aggro Sound
        Sound_Manager.instance.PlaySound(Sound_Manager.SFX.EnemyScreech, transform, 0.4f);

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
            angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, angleSmoothTime);
        }
        else
        {
            // Look in direction of movement
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, angleSmoothTime);
        }

        // Start sprinting after time delay
        if (runDelayTimer >= 1)
        {
            // Listen for dodge command after cooldown
            if (dodgeCooldownTimer < 0 && !canDodge)
            {
                enemyBase.eventHandler.PlayerBulletFired.AddListener(CanDodge);
            }

            // Move Normally
            if (dodgeTimer > 0.1f)
            {
                // Update Path after dodge
                if (canDodge)
                {
                    enemyBase.eventHandler.PlayerBulletFired.RemoveListener(Dodge);
                    canDodge = false;

                    UpdatePath(rb.position, target.position);
                    
                    dodgeCooldownTimer = 2;
                }

                // Direction Damping
                direction.x = Mathf.SmoothDamp(currentDirection.x, direction.x, ref refSpeedX, velocitySmoothTime);
                direction.y = Mathf.SmoothDamp(currentDirection.y, direction.y, ref refSpeedY, velocitySmoothTime);
                enemyBase.MoveEnemy(direction * enemyBase.speed);
            }
            
            // Dodge for 0.1s
            if (dodgeTimer <= 0.1f && enemyBase.IsLineOfSight && canDodge) 
            {
                Dodge();
                direction = dodgeDirection.normalized + direction.normalized;
                enemyBase.MoveEnemy(1.5f * enemyBase.speed * direction);
            }

            // Increment timers
            dodgeTimer += Time.deltaTime;
            dodgeCooldownTimer -= Time.deltaTime;
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
            currentDirection = direction;
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

    void Dodge()
    {
        Vector2 perpDirection = Vector2.Perpendicular(target.position - rb.transform.position).normalized;

        LayerMask mask = LayerMask.GetMask("Obstacle");

        RaycastHit2D rightRay = Physics2D.Raycast(perpDirection, Vector2.right, 7, mask);
        RaycastHit2D leftRay = Physics2D.Raycast(-perpDirection, Vector2.left, 7, mask);

        if (rightRay.collider && leftRay.collider)
        {
            // Don't Dodge
            dodgeDirection = Vector2.zero;
        }

        if (!rightRay.collider && !leftRay.collider) 
        {
            // Dodge Random Direction
            int rand = Random.Range(0, 100);

            if (rand < 50)
            {
                dodgeDirection = perpDirection;
            }

            if (rand >= 1)
            {
                dodgeDirection = -perpDirection;
            }
        }

        if (rightRay.collider && !leftRay.collider) 
        {
            // Dodge Left
            dodgeDirection = perpDirection;
        }

        if (!rightRay.collider && leftRay.collider)
        {
            // Dodge Right
            dodgeDirection = -perpDirection;
        }
    }

    void CanDodge()
    { 
        if (dodgeCooldownTimer < 0)
        {
            dodgeTimer = 0;
            canDodge = true;
        }
    }
}
