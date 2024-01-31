using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierBehaviour")]
public class ModifierBehaviour : ScriptableObject
{
    [SerializeField] public List<EnemyType> enemyTypes = new List<EnemyType>();
    [SerializeField] public List<int> enemyWeightings = new List<int>();
    [SerializeField] public int spawnPercentChance;
}
