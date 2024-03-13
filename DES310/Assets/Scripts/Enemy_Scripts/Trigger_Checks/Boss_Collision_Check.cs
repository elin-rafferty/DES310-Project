using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionCheck : MonoBehaviour
{
    private Enemy_Base enemyBase;

    private void Awake()
    {
        enemyBase = GetComponent<Enemy_Base>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        enemyBase.colliderTag = collision.collider.gameObject.tag;
    }
}
