using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    public GameObject Player { get; set; }
    private EnemyBase enemyBase;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Trigger Check");
        enemyBase = GetComponentInParent<EnemyBase>();
    }

    void FixedUpdate()
    {
        float distance = new Vector2(Player.transform.position.x - enemyBase.transform.position.x, Player.transform.position.y - enemyBase.transform.position.y).sqrMagnitude;

        if (distance < 1225)
        {
            enemyBase.SetAggroStatus(true);
        }
        else
        {
            enemyBase.SetAggroStatus(false);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{;
    //    if (collision.gameObject == Player)
    //    {
    //        enemyBase.SetAggroStatus(true);
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject == Player) 
    //    {
    //        enemyBase.SetAggroStatus(false);
    //    }
    //}
}
