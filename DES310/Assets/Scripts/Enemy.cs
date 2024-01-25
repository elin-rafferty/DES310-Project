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

        // Follow player when within aggro range
        if (distance < aggroDistance)
        {
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

    private void FixedUpdate()
    {
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position);
        foreach (RaycastHit2D collision in ray)
        {
            if (collision.collider.gameObject.tag == "Player")
            {
                lineOfSight = collision.collider.CompareTag("Player");

                if (lineOfSight)
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                }
            }
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
