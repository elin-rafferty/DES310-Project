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

    List<GameObject> breadCrumbList = new();
    private int targetIndex;
    private float searchTimer = 0;
    private bool ranSearch = false;

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
        lineOfSight = LOScheck(player);

        // Distance to player
        distance = Vector2.Distance(transform.position, player.transform.position);

        Debug.Log(currentState);

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
        if (!ranSearch)
        {
            // Copy list from player
            breadCrumbList = player.GetComponent<BreadCrumbList>().breadCrumbs;
            targetIndex = player.GetComponent<BreadCrumbList>().oldestCrumbIndex;

            float distance = Vector2.Distance(transform.position, breadCrumbList[targetIndex].transform.position);

            // Find nearest breadCrumb
            for (int i = 0; i < breadCrumbList.Count; i++)
            {
                float newDistance = Vector2.Distance(transform.position, breadCrumbList[i].transform.position);
                if (distance > newDistance)
                {
                    distance = newDistance;

                    targetIndex = i;
                }
            }
            ranSearch = true;
        }

        if (searchTimer < 10)
        {
            // Search for player using bread crumbs
            if (ranSearch)
            {
                distance = Vector2.Distance(transform.position, breadCrumbList[targetIndex].transform.position);
                if (distance < 0.1f)
                {
                    if (targetIndex == breadCrumbList.Count - 1)
                    {
                        targetIndex = 0;
                    }
                    else
                    {
                        targetIndex++;
                    }
                }
                for (int i = 0; i < breadCrumbList.Count; i++) breadCrumbList[i].gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                breadCrumbList[targetIndex].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                Chase(breadCrumbList[targetIndex]);
            }
        }
        else
        {
            // Return to idle
            ranSearch = false;
            currentState = State.IDLE;
        }

        searchTimer += Time.deltaTime;
    }

    private bool LOScheck(GameObject target)
    {
        bool lLos = lineOfSightCheck.isLineOfSight(target, leftNode);
        bool rLos = lineOfSightCheck.isLineOfSight(target, rightNode);

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
