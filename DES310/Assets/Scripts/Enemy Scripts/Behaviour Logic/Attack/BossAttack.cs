using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack - Boss", menuName = "Enemy Logic/Attack Logic/Boss")]
public class BossAttack : EnemyAttackSOBase
{
    private enum AttackState
    {
        NONE,

        // Charge
        CHARGE_ATTACK,
        WINDUP,
        CHARGE,
        STUNNED,

        // Leap
        LEAP_ATTACK,
        READY_JUMP,
        JUMP,
        SLAM,

        // Shoot
        SHOOT_ATTACK,
        SHOOT,
        SHOOT_COOLDOWN,

        //Tenatcle
        TENTACLE_ATTACK
    }

    private AttackState currentAttackState;
    private AttackState currentAttack;

    // General Vars
    Vector2 targetDirection = new();
    Vector2 targetPosition = new();

    private float angleSmoothTime = 0.25f;
    private float rotateSpeed;

    #region Charge Variables

    private float chargeTimer = 2;

    #endregion

    #region Leap Variables

    private float jumpTimer = 1;

    Vector2 bossScale = new();

    #endregion

    #region Tentacle Variables

    #endregion

    #region Shoot Variables

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private float sprayAngle;
    [SerializeField] private int bullets;
    private Transform weaponTransform;
    private float shootTimer;
    private int shotsFired;

    #endregion

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        

        currentAttackState = AttackState.NONE;
        enemyBase.colliderTag = string.Empty;

        // Choose Random Attack
        int rand = Random.Range(0, 4);

        if (rand == 0)
        {
            // Leap Attack
            currentAttack = AttackState.LEAP_ATTACK;
            currentAttackState = AttackState.READY_JUMP;
        }
        else if (rand >= 1 && rand <= 2)
        {
            // Charge Attack
            chargeTimer = 2;
            currentAttack = AttackState.CHARGE_ATTACK;
            currentAttackState = AttackState.WINDUP;
        }
        else if (rand == 3)
        {
            // Shoot Attack
            weaponTransform = enemyBase.gameObject.GetComponent<Boss>().weaponTransform;
            shootTimer = 0;
            shotsFired = 0;
            currentAttack = AttackState.SHOOT_ATTACK;
            currentAttackState = AttackState.SHOOT;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();

        currentAttack = AttackState.NONE;
        currentAttackState = AttackState.NONE;
    }

    public override void DoFrameUpdateLogic()
    {

    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        //Debug.Log(currentAttackState.ToString());

        switch (currentAttack)
        {
            case AttackState.LEAP_ATTACK:
                LeapAttack();
                break; 

            case AttackState.CHARGE_ATTACK:
                ChargeAttack();
                break;

            case AttackState.SHOOT_ATTACK:
                ShootAttack();
                break;
        }
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    void LeapAttack()
    {
        switch (currentAttackState)
        {
            case AttackState.READY_JUMP:

                // Set jump direction
                targetDirection = ((Vector2)Player.transform.position - enemyBase.rb.position).normalized;
                targetPosition = Player.transform.position;

                // Save current scale
                bossScale = enemyBase.transform.localScale;

                jumpTimer = 0;
                gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
                currentAttackState = AttackState.JUMP;

                break;

            // Leap into the air
            case AttackState.JUMP:

                // Move to player's position
                if (new Vector2(targetPosition.x - enemyBase.transform.position.x, targetPosition.y - enemyBase.transform.position.y).sqrMagnitude <= 1 || enemyBase.colliderTag == "Wall")
                {
                    // Stop on target position
                    enemyBase.MoveEnemy(Vector2.zero);
                    jumpTimer = 0.4f;
                    bossScale = enemyBase.transform.localScale;
                    currentAttackState = AttackState.SLAM;
                }
                else
                {
                    jumpTimer += Time.deltaTime;
                    enemyBase.MoveEnemy(targetDirection * enemyBase.speed * 2);
                    enemyBase.transform.localScale = Vector2.Lerp(bossScale, Vector2.one * 2.5f, jumpTimer);
                }

                break;

            // Crash back down
            case AttackState.SLAM:

                if (jumpTimer <= 0)
                {
                    enemyBase.eventHandler.ShakeCamera.Invoke(2, 100);

                    enemyBase.transform.localScale = Vector2.one * 2;

                    gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
                    currentAttackState = AttackState.READY_JUMP;
                    enemyBase.StateMachine.ChangeState(enemyBase.CHASEState);
                }
                else
                {
                    // Scale down over time
                    enemyBase.transform.localScale = Vector2.Lerp(Vector2.one * 2, bossScale, jumpTimer * 2.5f);

                    jumpTimer -= Time.deltaTime;
                }

                break;
        }
    }

    void ChargeAttack()
    {
        float angle;

        switch (currentAttackState)
        {
            // Wind up charge
            case AttackState.WINDUP:

                if (chargeTimer > 0)
                {
                    chargeTimer -= Time.deltaTime;

                    // Look at Player
                    targetDirection = ((Vector2)Player.transform.position - enemyBase.rb.position).normalized;
                    float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                    angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, angleSmoothTime);
                    enemyBase.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

                    enemyBase.MoveEnemy(Vector2.zero);
                }
                else
                {
                    currentAttackState = AttackState.CHARGE;
                }
                break;

            // Begin Charge
            case AttackState.CHARGE:

                if (enemyBase.colliderTag != "Wall")
                {
                    // Charge Player's Current Position
                    enemyBase.MoveEnemy(targetDirection * enemyBase.speed * 3);
                }

                else if (enemyBase.colliderTag == "Wall")
                {
                    enemyBase.MoveEnemy(Vector2.zero);
                    enemyBase.eventHandler.ShakeCamera.Invoke(2, 100);
                    chargeTimer = 5;
                    enemyBase.damageReduction = 0;
                    currentAttackState = AttackState.STUNNED;
                }
                break;

            // Stun on collision with wall
            case AttackState.STUNNED:

                enemyBase.MoveEnemy(Vector2.zero);

                if (chargeTimer > 0)
                {
                    chargeTimer -= Time.deltaTime;

                    if (chargeTimer < 4)
                    {
                        enemyBase.eventHandler.ShakeCamera.Invoke(0, 0);
                    }
                }
                else
                {
                    enemyBase.damageReduction = 0.9f;
                    currentAttackState = AttackState.WINDUP;
                    enemyBase.StateMachine.ChangeState(enemyBase.CHASEState);
                }

                break;
        }
    }

    void TentacleAttack()
    {
       
    }

    void SpawnEnemyAttack()
    {

    }

    void ShootAttack()
    {
        float angle;

        // Look at Player
        targetDirection = ((Vector2)Player.transform.position - enemyBase.rb.position).normalized;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        angle = Mathf.SmoothDampAngle(enemyBase.transform.eulerAngles.z, targetAngle, ref rotateSpeed, angleSmoothTime);
        enemyBase.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        enemyBase.MoveEnemy(Vector2.zero);

        switch (currentAttackState) 
        {
            case AttackState.SHOOT:

                // Player Direction
                Vector2 direction = (Player.transform.position - weaponTransform.position).normalized;

                for (int i = 0; i < bullets; i++)
                {
                    float interval = sprayAngle / bullets;

                    float firingAngle = Mathf.Atan2(direction.y, direction.x) + (interval * i * Mathf.Deg2Rad) - ((sprayAngle / 2) * Mathf.Deg2Rad);
                    Vector2 firingDirection = new Vector2(Mathf.Cos(firingAngle), Mathf.Sin(firingAngle));

                    Fire(firingDirection);
                }

                shootTimer = enemyBase.attackDelay;
                shotsFired++;
                currentAttackState = AttackState.SHOOT_COOLDOWN;

                if (shotsFired >= 5)
                {
                    enemyBase.StateMachine.ChangeState(enemyBase.CHASEState);
                }

                break;

            case AttackState.SHOOT_COOLDOWN:

                if (shootTimer > 0)
                {
                    // Decremnet timer
                    shootTimer -= Time.deltaTime;
                }
                else
                {
                    // Shoot again
                    currentAttackState = AttackState.SHOOT;
                }

                break;
        }
    }

    void Fire(Vector2 direction)
    {
        // Fire
        Projectile newProjectile = Instantiate(projectilePrefab, weaponTransform.position, Quaternion.identity);
        newProjectile.SetType(projectileType);
        newProjectile.SetDirection(direction);
        newProjectile.SetOwner(enemyBase.gameObject);
        newProjectile.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        // Set projectile to despawn after a certain time has elapsed
        Destroy(newProjectile.gameObject, 10);

        // Play shoot sound
        SoundManager.instance.PlaySound(SoundManager.SFX.SpitterAttack, transform, 1f);
    }
}
