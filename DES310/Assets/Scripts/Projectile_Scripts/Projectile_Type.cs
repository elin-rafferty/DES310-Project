using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileType", menuName = "Types/ProjectileType")]
public class Projectile_Type : ScriptableObject
{
    public float speed;
    public float damage;
    public float despawnTimer;
    public Sprite sprite;
}
