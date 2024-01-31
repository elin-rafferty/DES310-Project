using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Enemy Game Objects
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private ModifierBehaviour modifierBehaviour;
    private List<Enemy> spawnedEnemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnAnEnemy();
    }

    private void OnDestroy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SpawnAnEnemy();
        }
    }

    void SpawnAnEnemy()
    {
        if (UnityEngine.Random.Range(0, 100) < modifierBehaviour.spawnPercentChance)
        {
            // Spawn Enemy at spawn point location
            Enemy newEnemy = Instantiate(enemyPrefab, new Vector2(transform.position.x, transform.position.y), enemyPrefab.transform.rotation);
            newEnemy.SetType(ChooseEnemyType());
            newEnemy.SetModifiers(modifierBehaviour);
            spawnedEnemies.Add(newEnemy);
        }
    }

    void DestroySpawnedEnemies()
    {
        foreach (Enemy e in spawnedEnemies)
        {
            Destroy(e);
        }
        spawnedEnemies.Clear();
    }

    EnemyType ChooseEnemyType()
    {
        EnemyType returnVal = null;
        if (modifierBehaviour.enemyTypes.Count != modifierBehaviour.enemyWeightings.Count)
        {
            Debug.Log("Modifier behaviour does not have equal enemy types and weightings");
        } else
        {
            int totalWeights = 0;
            List<Tuple<int, int>> weightingRanges = new List<Tuple<int, int>>();
            foreach (int weighting in modifierBehaviour.enemyWeightings)
            {
                weightingRanges.Add(new Tuple<int, int>(totalWeights, totalWeights + weighting));
                totalWeights += weighting;
            }
            int random = UnityEngine.Random.Range(0, totalWeights);
            for (int i = 0; i < weightingRanges.Count; i++)
            {
                if (random >= weightingRanges[i].Item1 && random < weightingRanges[i].Item2)
                {
                    returnVal = modifierBehaviour.enemyTypes[i];
                }
            }
        }
        if (returnVal == null)
        {
            Debug.Log("Oopsie");
        }
        return returnVal;
    }
}
