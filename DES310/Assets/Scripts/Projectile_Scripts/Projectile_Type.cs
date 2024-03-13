using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileType", menuName = "Types/ProjectileType")]
public class ProjectileType : ScriptableObject
{
    public float speed;
    public float damage;
    public float despawnTimer;
    public Sprite sprite;
}
