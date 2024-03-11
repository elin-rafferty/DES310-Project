using Cinemachine;
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
        WINDUP,
        CHARGE,
        STUNNED,

        // Leap
        READY_JUMP,
        JUMP,
        SLAM,

        //Tenatcle
    }

    private AttackState currentAttackState;

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

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        

        currentAttackState = AttackState.WINDUP;
        chargeTimer = 2;
        enemyBase.colliderTag = string.Empty;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {

    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        Debug.Log(currentAttackState.ToString());

        ChargeAttack();

        //LeapAttack();

        //TentacleAttack();
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
                    currentAttackState = AttackState.WINDUP;
                    enemyBase.StateMachine.ChangeState(enemyBase.CHASEState);
                }

                break;
        }
    }

    void TentacleAttack()
    {
       
    }
}
