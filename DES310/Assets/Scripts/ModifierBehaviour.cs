using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierBehaviour")]
public class ModifierBehaviour : ScriptableObject
{
    [SerializeField] public List<EnemyType> enemyTypes = new List<EnemyType>();
    [SerializeField] public List<int> enemyWeightings = new List<int>();
    [SerializeField] public int spawnPercentChance = 50;
    [SerializeField] public float enemySpeedMultiplier = 1;
    [SerializeField] public float enemyDamageMultiplier = 1;
    [SerializeField] public float enemyHealthMultiplier = 1;
    [SerializeField] public float enemyAggroRangeMultiplier = 1;
    [SerializeField] public float enemyDeaggroRangeMultiplier = 1;
}
