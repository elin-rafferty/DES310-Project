using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Enemy Game Objects
    [SerializeField] private Enemy EnemyPrefab;
    [SerializeField] private EnemyType EnemyType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            // Spawn Enemy at spawn point location
            Enemy newEnemy = Instantiate(EnemyPrefab, transform.position, EnemyPrefab.transform.rotation);
            newEnemy.SetType(EnemyType);
        }
    }
}
