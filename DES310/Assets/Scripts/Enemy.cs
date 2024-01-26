using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeReference] private SpriteRenderer SpriteRenderer;
    private EnemyType type;

    private GameObject player;
    private bool lineOfSight;
    private bool aggro = false;

    private float aggroDistance;
    private float deaggroDistance;

    private float speed;
    private float health;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
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

        if (distance > deaggroDistance) aggro = false;
    }

    private void FixedUpdate()
    {
        // Cast ray from enemy to player
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position);
        foreach (RaycastHit2D collision in ray)
        {
            if (collision.collider.gameObject.tag == "Player")
            {
                // If first collision player enemy has LOS
                lineOfSight = true;
                break;
            }
            else if (collision.collider.gameObject.tag == "Enemy")
            {
                // Ignore enemy colliders
                continue;
            }
            else
            {
                // If any collision before player enemy hasn't got LOS
                lineOfSight = false;
                break;
            }
        }

        if (lineOfSight)
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        }
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
