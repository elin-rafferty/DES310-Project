using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    [SerializeReference] private SpriteRenderer SpriteRenderer;
    private EnemyType type;

    private enum State
    {
        IDLE,
        CHASE,
        SEARCH,
        ATTACK,
        RETREAT
    }

    private LineOfSightCheck lineOfSightCheck;
    private GameObject player;
    private State currentState;

    private List<Vector3> breadCrumbs = new List<Vector3>();
    private float breadCrumbInterval = 0.1f;
    private float timer = 0;
    private float delayTime = 0;
    private float followTime = 0;
    private bool isRecording = false;

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
        Debug.Log(currentState);

        // Record player position at set interval 
        timer += Time.deltaTime;
        delayTime += Time.deltaTime;
        if (timer > breadCrumbInterval && isRecording)
        {
            timer = 0;
            // Add latest position
            breadCrumbs.Add(player.transform.position);

            if (delayTime > 1)
            {
                // Remove oldest position
                breadCrumbs.RemoveAt(0);
            }
        }

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
            // Search State
            case State.SEARCH:
                SearchForPlayer();
                break;
            // Attack State
            case State.ATTACK:

                break;
            // Retreat State (May not be used)
            case State.RETREAT:

                break;
        }
    }

    private void FindTarget()
    {
        if (lineOfSight && distance < aggroDistance)
        {
            isRecording = true;
            delayTime = 0;
            currentState = State.CHASE;
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
            isRecording = false;
            currentState = State.SEARCH;
        }
        else if (distance > deaggroDistance)
        {
            isRecording = false;
            currentState = State.SEARCH;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void SearchForPlayer()
    {
        if (breadCrumbs.Count != 0)
        {
            // Follow player bread crumb trail
            Vector2 direction = breadCrumbs[0] - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            float distToBreadCrumb = Vector2.Distance(transform.position, breadCrumbs[0]);

            if (distToBreadCrumb < 0.5)
            {
                breadCrumbs.RemoveAt(0);
                if (lineOfSight && distance < deaggroDistance)
                {
                    currentState = State.CHASE;
                }
            }
            if (followTime > 0.3)
            {
                breadCrumbs.RemoveAt(0);
                followTime = 0;
                if (lineOfSight && distance < deaggroDistance)
                {
                    currentState = State.CHASE;
                }
            }
            else
            {
                followTime += Time.deltaTime;
            }

        }
        else if (lineOfSight && distance < deaggroDistance)
        {
            currentState = State.CHASE;
        }
        else
        {
            currentState = State.IDLE;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
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
}
