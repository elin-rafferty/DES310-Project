using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Range_Check : MonoBehaviour
{
    public GameObject Player { get; set; }
    private Enemy_Base enemyBase;

    private float attackRange;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyBase = GetComponentInParent<Enemy_Base>();
    }
    private void Start()
    {
        if (enemyBase.attackRange > 0)
        {
            attackRange = Mathf.Pow(enemyBase.attackRange, 2);
        }
    }

    private void FixedUpdate()
    {
        float distance = new Vector2(Player.transform.position.x - enemyBase.transform.position.x, Player.transform.position.y - enemyBase.transform.position.y).sqrMagnitude;

        if (distance < attackRange)
        {
            enemyBase.SetWithinAttackRange(true);
        }
        else
        {
            enemyBase.SetWithinAttackRange(false);
        }
    }
}