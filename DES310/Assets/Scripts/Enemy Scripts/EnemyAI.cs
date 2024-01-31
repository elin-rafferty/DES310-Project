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
    [SerializeField] private GameObject leftNode;
    [SerializeField] private GameObject rightNode;
    private State currentState;

    private List<Vector3> breadCrumbs = new List<Vector3>();
    private float breadCrumbInterval = 0.1f;
    private float timer = 0;
    private float delayTime = 0;
    private float followTime = 0;
    private bool isRecording = false;
    private float searchTimer = 0;

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

        //Debug.Log(currentState);

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
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (lineOfSight && distance < aggroDistance)
        {
            currentState = State.CHASE;
        }
    }

    private void Chase(GameObject target)
    {
        // Follow Target
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void ChasePlayer()
    {
        Chase(player);

        if (!lineOfSight || distance > deaggroDistance)
        {
            searchTimer = 0;
            currentState = State.SEARCH;
        }
    }

    private void SearchForPlayer()
    {
        // Copy list from player
        List<GameObject> breadCrumbList = player.GetComponent<BreadCrumbList>().breadCrumbs;
        int firstCrumbIndex = player.GetComponent<BreadCrumbList>().oldestCrumbIndex;

        GameObject Target = breadCrumbList[firstCrumbIndex];

        if (searchTimer < 3)
        {
            // Search for player
        }
        else
        {
            // Return to idle
            currentState = State.IDLE;
        }

        searchTimer += Time.deltaTime;
    }

    private bool LOScheck()
    {
        bool lLos = lineOfSightCheck.isLineOfSight(player, leftNode);
        bool rLos = lineOfSightCheck.isLineOfSight(player, rightNode);

        if (lLos && rLos) 
        { 
            return true; 
        }
        return false;
    }

    public void SetType(EnemyType type)
    {
        // Set type and details of enemy
        this.type = type;

        speed = type.speed;
        aggroDistance = type.aggroDist;
        deaggroDistance = type.deaggroDist;
        health = type.health;
        damage = type.damage;
        gameObject.GetComponent<SpriteRenderer>().sprite = type.sprite;
    }
}
