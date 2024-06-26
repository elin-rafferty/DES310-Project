using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightCheck : MonoBehaviour
{
    public GameObject Player { get; set; }
    private EnemyBase enemyBase;

    [SerializeField] private GameObject leftNode;
    [SerializeField] private GameObject rightNode;

    private bool rLos = false;
    private bool lLos = false;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyBase = GetComponent<EnemyBase>();
    }

    private void Update()
    {
        if(enemyBase.IsAggro)
        {
            rLos = IsLineOfSight(rightNode.transform);
            lLos = IsLineOfSight(leftNode.transform);

            if (rLos || lLos)
            {
                enemyBase.SetLineOfSightStatus(true);
            }
            else
            {
                enemyBase.SetLineOfSightStatus(false);
            }
        }
    }

    private bool IsLineOfSight(Transform source)
    {
        bool los = false;

        // Cast ray from enemy to traget
        RaycastHit2D[] ray = Physics2D.RaycastAll(source.position, Player.transform.position - source.position);
        foreach (RaycastHit2D collision in ray)
        {
            if (Player.CompareTag(collision.collider.gameObject.tag))
            {
                // If first collision target, has LOS
                los = true;
                break;
            }
            else if (collision.collider.gameObject.CompareTag("Enemy"))
            {
                // Ignore objects with same tag colliders
                continue;
            }
            else
            {
                // If any collision before target enemy, hasn't got LOS
                los = false;
                break;
            }
        }

        if (los)
        {
            Debug.DrawRay(source.position, Player.transform.position - source.position, Color.green);
        }
        else
        {
            Debug.DrawRay(source.position, Player.transform.position - source.position, Color.red);
        }

        return los;
    }
}
