using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack - Boss", menuName = "Enemy Logic/Attack Logic/Boss")]
public class BossAttack : EnemyAttackSOBase
{
    private enum AttackState
    {
        NONE,
        WINDUP,
        CHARGE,
        STUNNED
    }

    private AttackState currentAttackState;
    Vector2 playerDirection = new();

    private float angleSmoothTime = 0.25f;
    private float rotateSpeed;

    // Cooldown Timers
    private float chargeTimer = 2;

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

        ChargeAttack();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    void LeapAttack()
    {
        
    }

    void ChargeAttack()
    {
        Debug.Log(currentAttackState.ToString());

        float angle;

        switch (currentAttackState) 
        {
            // Wind up charge
            case AttackState.WINDUP:

                if (chargeTimer > 0)
                {
                    chargeTimer -= Time.deltaTime;

                    // Look at Player
                    playerDirection = ((Vector2)Player.transform.position - enemyBase.rb.position).normalized;
                    float targetAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
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
                    enemyBase.MoveEnemy(playerDirection * enemyBase.speed * 3);
                }

                else if (enemyBase.colliderTag == "Wall")
                {
                    enemyBase.MoveEnemy(Vector2.zero);
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
