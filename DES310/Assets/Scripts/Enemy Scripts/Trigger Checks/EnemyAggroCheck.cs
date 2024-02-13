using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    public GameObject Player { get; set; }
    private EnemyBase enemyBase;

    float aggroRange;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyBase = GetComponentInParent<EnemyBase>();
    }

    private void Start()
    {
        aggroRange = Mathf.Pow(enemyBase.aggroRange, 2);
    }

    private void FixedUpdate()
    {
        float distance = new Vector2(Player.transform.position.x - enemyBase.transform.position.x, Player.transform.position.y - enemyBase.transform.position.y).sqrMagnitude;

        if (distance < aggroRange)
        {
            enemyBase.SetAggroStatus(true);
        }
        else
        {
            enemyBase.SetAggroStatus(false);
        }
    }
}
