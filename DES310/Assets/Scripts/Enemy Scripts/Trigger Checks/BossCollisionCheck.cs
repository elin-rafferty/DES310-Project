using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionCheck : MonoBehaviour
{
    private EnemyBase enemyBase;

    private void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        enemyBase.colliderTag = collision.collider.gameObject.tag;
    }
}
