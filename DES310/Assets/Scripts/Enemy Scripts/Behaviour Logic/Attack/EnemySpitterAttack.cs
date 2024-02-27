using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack - Spitter", menuName = "Enemy Logic/Attack Logic/Spitter")]
public class EnemySpitterAttack : EnemyAttackSOBase
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ProjectileType projectileType;
    private Transform weaponTransform;
    private Transform target;

    private float attackTimer;


    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        attackTimer = enemyBase.attackDelay;
        target = Player.transform;
        weaponTransform = enemyBase.gameObject.GetComponent<Spitter>().weaponTransfom;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // Do Enemy Attack
        if (attackTimer >= enemyBase.attackDelay)
        {
            attackTimer = 0;
            Fire();
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    void Fire()
    {
        // Calculate direction to player
        Vector2 direction = (target.position - weaponTransform.position).normalized;

        // Fire
        Projectile newProjectile = Instantiate(projectilePrefab, weaponTransform.position, Quaternion.identity);
        newProjectile.SetType(projectileType);
        newProjectile.SetDirection(direction);
        newProjectile.SetOwner(enemyBase.gameObject);
        newProjectile.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        // Set projectile to despawn after a certain time has elapsed
        Destroy(newProjectile.gameObject, 1f * (1/enemyBase.attackDelay));

        // Play shoot sound
        SoundManager.instance.PlaySound(SoundManager.SFX.PlayerShoot, transform, 0.3f);
    }
}