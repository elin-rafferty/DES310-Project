using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRangeCheck : MonoBehaviour
{
    public GameObject Player { get; set; }
    private EnemyBase enemy;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Trigger Check");
        enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            enemy.SetWithinAttackRange(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            enemy.SetWithinAttackRange(false);
        }
    }
}