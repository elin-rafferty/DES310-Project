using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    [SerializeReference] private SpriteRenderer SpriteRenderer;
    private EnemyType type;
    private ModifierBehaviour modifierBehaviour;

    private enum State
    {
        IDLE,
        CHASE,
        ATTACK,
        RETREAT
    }

    private LineOfSightCheck lineOfSightCheck;
    private GameObject player;
    private State currentState;

    private bool lineOfSight;
    private float distance;
    private float aggroDistance;
    private float deaggroDistance;

    private float speed;
    private float health;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        lineOfSightCheck = GetComponent<LineOfSightCheck>();
        player = GameObject.FindGameObjectWithTag("Player");

        currentState = State.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        // Line of sight check
        lineOfSight = LOScheck();

        // Distance to player
        distance = Vector2.Distance(transform.position, player.transform.position);

        // Enemy State Machine
        switch (currentState)
        {
        // Idle State
        case State.IDLE:
            FindTarget();
            break;

        // Chase State
        case State.CHASE:
            ChasePlayer();
            break;

        // Attack State
        case State.ATTACK:

            break;

        // Retreat State (May not be used)
        case State.RETREAT:

            break;
        }
    }

    private void ChasePlayer()
    {
        // Follow player when aggro
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (!lineOfSight)
        {
            currentState = State.IDLE;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        if (distance > deaggroDistance)
        {
            currentState = State.IDLE;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void FindTarget()
    {
        if (lineOfSight && distance < aggroDistance) 
        {
            currentState = State.CHASE;
        }
    }

    private bool LOScheck()
    {
        bool los = lineOfSightCheck.isLineOfSight(player);

        if (los)
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        }

        return los;
    }

    public void SetType(EnemyType type)
    {
        // Set type and details of enemy
        this.type = type;

        speed = type.speed;
        aggroDistance = type.aggroDist;
        deaggroDistance = type.aggroDist;
        health = type.health;
        damage = type.damage;
        gameObject.GetComponent<SpriteRenderer>().sprite = type.sprite;
    }

    public void SetModifiers(ModifierBehaviour modifier)
    {
        modifierBehaviour = modifier;
        health *= modifierBehaviour.enemyHealthMultiplier;
        damage *= modifierBehaviour.enemyDamageMultiplier;
        speed *= modifierBehaviour.enemySpeedMultiplier;
        aggroDistance *= modifierBehaviour.enemyAggroRangeMultiplier;
        deaggroDistance *= modifierBehaviour.enemyDeaggroRangeMultiplier;
    }

    // Change the hell out of this later, here for now just while AI gets worked on
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Attacking player");
            collision.gameObject.GetComponent<PlayerMovement>().Damage(damage * modifierBehaviour.enemyDamageMultiplier);
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
