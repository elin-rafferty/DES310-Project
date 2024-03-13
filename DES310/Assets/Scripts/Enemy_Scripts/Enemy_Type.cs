using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Types/EnemyType")]
public class Enemy_Type : ScriptableObject
{
    public Sprite sprite;

    public float speed = 5;
    public float health = 50;
    public float damage = 5;
    public float aggroDist = 5;
    public float deaggroDist = 10;
}
