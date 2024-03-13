using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Modifier_Behaviour")]
public class Modifier_Behaviour : ScriptableObject
{
    [Header("Spawn Variables")]
    [SerializeField] public List<Enemy_Base> enemyTypes = new List<Enemy_Base>();
    [SerializeField] public List<int> enemyWeightings = new List<int>();
    [SerializeField] public int spawnPercentChance = 50;

    [Header("Health Multipliers")]
    [SerializeField] public float enemyHealthMultiplier = 1;
    [SerializeField] public float sprinterHealthMultiplier = 1;
    [SerializeField] public float walkerHealthMultiplier = 1;
    [SerializeField] public float spitterHealthMultiplier = 1;

    [Header("Damage Multipliers")]
    [SerializeField] public float enemyDamageMultiplier = 1;
    [SerializeField] public float sprinterDamageMultiplier = 1;
    [SerializeField] public float walkerDamageMultiplier = 1;
    [SerializeField] public float spitterDamageMultiplier = 1;

    [Header("Speed Multipliers")]
    [SerializeField] public float enemySpeedMultiplier = 1;
    [SerializeField] public float sprinterSpeedMultiplier = 1;
    [SerializeField] public float walkerSpeedMultiplier = 1;

    [Header("Attack Speed Multipliers")]
    [SerializeField] public float enemyAttackSpeedMultiplier = 1;
    [SerializeField] public float sprinterAttackSpeedMultiplier = 1;
    [SerializeField] public float walkerAttackSpeedMultiplier = 1;
    [SerializeField] public float spitterAttackSpeedMultiplier = 1;

    [Header("Attack Range Multipliers")]
    [SerializeField] public float enemyAttackRangeMultiplier = 1;
    [SerializeField] public float sprinterAttackRangeMultiplier = 1;
    [SerializeField] public float walkerAttackRangeMultiplier = 1;
    [SerializeField] public float spitterAttackRangeMultiplier = 1;

    [Header("Aggro Multipliers")]
    [SerializeField] public float enemyAggroRangeMultiplier = 1;
    [SerializeField] public float sprinterAggroRangeMultiplier = 1;
    [SerializeField] public float walkerAggroRangeMultiplier = 1;
    [SerializeField] public float spitterAggroRangeMultiplier = 1;
}
