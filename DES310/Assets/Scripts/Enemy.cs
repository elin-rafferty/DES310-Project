using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeReference] private SpriteRenderer SpriteRenderer;
    private EnemyType type;

    private GameObject Player;
    private float aggroDistance;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        // Follow player when within aggro range
        if (distance < aggroDistance)
        {
            Vector2 direction = Player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        } else
        {

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    public void SetType(EnemyType type)
    {
        // Set type and details of enemy
        this.type = type;

        speed = type.speed;
        aggroDistance = type.aggroDist;
    }
}
