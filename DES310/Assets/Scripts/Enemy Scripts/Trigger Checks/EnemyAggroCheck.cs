using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    public GameObject Player { get; set; }
    private EnemyBase enemy;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {;
        if (collision.gameObject == Player)
        {
            enemy.SetAggroStatus(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player) 
        {
            enemy.SetAggroStatus(false);
        }
    }
}
