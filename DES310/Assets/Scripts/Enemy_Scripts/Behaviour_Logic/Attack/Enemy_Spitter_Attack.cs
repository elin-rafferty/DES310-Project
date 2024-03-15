using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack - Spitter", menuName = "Enemy Logic/Attack Logic/Spitter")]
public class Enemy_Spitter_Attack : Enemy_Attack_SO_Base
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ProjectileType projectileType;
    private Transform weaponTransform;
    private Transform target;

    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint;

    private float attackTimer;
    private float grabTimer;


    public override void DoAnimationTriggerEventLogic(Enemy_Base.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        grabTimer = 7;
        attackTimer = enemyBase.attackDelay;
        target = Player.transform;
        weaponTransform = enemyBase.gameObject.GetComponent<Spitter>().weaponTransfom;

        distanceJoint = enemyBase.gameObject.GetComponent<DistanceJoint2D>();
        distanceJoint.distance = enemyBase.attackRange - 0.5f;
        distanceJoint.connectedBody = Player.GetComponent<Rigidbody2D>();
        distanceJoint.enabled = false;
        lineRenderer = enemyBase.gameObject.GetComponentInChildren<LineRenderer>();
        lineRenderer.forceRenderingOff = false;
        lineRenderer.enabled = false;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();

        distanceJoint.connectedBody = null;
        distanceJoint.enabled = false;
        lineRenderer.forceRenderingOff = true;
        lineRenderer.enabled = false;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (grabTimer >= 2)
        {
            // Do Enemy Basic Attack
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
        else if (grabTimer > 0 && grabTimer < 2)
        {
            // Grab visual targeting
            lineRenderer.startColor = Color.Lerp(new Color(0.52f, 0.18f, 0.13f, 1), new Color(1, 1, 1, 1), grabTimer * 0.5f);
            lineRenderer.endColor = Color.Lerp(new Color(0.52f, 0.18f, 0.13f, 1), new Color(1, 1, 1, 1), grabTimer * 0.5f);

            Vector2 playerDirection = (Player.transform.position - enemyBase.gameObject.GetComponent<Spitter>().tetherTransform.position).normalized;
            lineRenderer.positionCount = 3;
            Vector3 source = enemyBase.gameObject.GetComponent<Spitter>().tetherTransform.position;

            // Right Cone
            lineRenderer.SetPosition(0, (Vector2)source + Vector2.Lerp(playerDirection, Quaternion.AngleAxis(30, new Vector3(0, 0, 1)) * playerDirection, grabTimer * 0.5f) * 3);
            // Tether Position
            lineRenderer.SetPosition(1, source);
            // Left Cone
            lineRenderer.SetPosition(2, (Vector2)source + Vector2.Lerp(playerDirection, Quaternion.AngleAxis(-30, new Vector3(0, 0, 1)) * playerDirection, grabTimer * 0.5f) * 3);

            lineRenderer.enabled = true;
        }
        else if (grabTimer <= 0) 
        {
            // Perform Grab
            lineRenderer.startColor = new Color(0.52f, 0.18f, 0.13f, 1);
            lineRenderer.endColor = new Color(0.52f, 0.18f, 0.13f, 1);
            distanceJoint.enabled = true;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, enemyBase.gameObject.GetComponent<Spitter>().tetherTransform.position);
            lineRenderer.SetPosition(1, Player.transform.position);
            lineRenderer.enabled = true;
        }

        grabTimer -= Time.deltaTime;
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
        Sound_Manager.instance.PlaySound(Sound_Manager.SFX.SpitterAttack, transform, 1f);
    }
}