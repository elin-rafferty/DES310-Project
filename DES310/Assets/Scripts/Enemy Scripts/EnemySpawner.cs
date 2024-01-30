using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Enemy Game Objects
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemyType enemyType;

    // Start is called before the first frame update
    void Start()
    {

        Enemy newEnemy = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        newEnemy.SetType(enemyType);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            // Spawn Enemy at spawn point location
            Enemy newEnemy = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
            newEnemy.SetType(enemyType);
        }
    }
}
