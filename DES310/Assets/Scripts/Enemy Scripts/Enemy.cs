using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeReference] private SpriteRenderer SpriteRenderer;
    private EnemyType type;

    private LineOfSightCheck lineOfSightCheck;
    private GameObject player;

    private bool aggro = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        // Line of sight check
        bool lineOfSight = LOScheck();

        // Distance to player
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (lineOfSight)
        {
            // Aggro player within range
            if (distance < aggroDistance) aggro = true;

            if (aggro)
            {
                // Follow player when aggro
                Vector2 direction = player.transform.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        // Deaggro when player greater than deaggro distance
        if (distance > deaggroDistance) aggro = false;
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
        gameObject.GetComponent<SpriteRenderer>().sprite = type.sprite;
    }
}
