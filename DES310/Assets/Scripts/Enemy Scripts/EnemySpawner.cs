using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Enemy Game Objects
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemyType enemyType;
    private List<Enemy> spawnedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAnEnemy();
    }

    private void OnDestroy()
    {
        DestroySpawnedEnemies();
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
        // Spawn Enemy at spawn point location
        Enemy newEnemy = Instantiate(enemyPrefab, new Vector2(transform.position.x, transform.position.y), enemyPrefab.transform.rotation);
        newEnemy.SetType(enemyType);
        spawnedEnemies.Add(newEnemy);
    }

    void DestroySpawnedEnemies()
    {
        foreach (Enemy e in spawnedEnemies)
        {
            Destroy(e);
        }
        spawnedEnemies.Clear();
    }
}
