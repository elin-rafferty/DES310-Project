using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Types/EnemyType")]
public class EnemyType : ScriptableObject
{
    public Sprite Sprite;

    public float speed;
    public float health;
    public float damage;
    public float aggroDist;
    public float deaggroDist;
}
