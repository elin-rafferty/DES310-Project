using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Enemy Game Objects
    [SerializeField] private ModifierBehaviour modifierBehaviour;
    [SerializeField] private EventHandler eventHandler;
    private List<EnemyBase> spawnedEnemies = new List<EnemyBase>();

    [SerializeField] private float respawnTime;
    private float respawnTimer;
    private bool spawnedEnemy = false;

    enum WallDirection
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;

        // Has an enemy Spawned
        if (SpawnAnEnemy())
        {
            spawnedEnemy = true;
        }
        else 
        {
            spawnedEnemy = false;
        }

        respawnTimer = respawnTime;
    }

    private void OnDestroy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedEnemy)
        {
            // Clear dead enemies from list
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                if (spawnedEnemies[i] == null)
                {
                    spawnedEnemies.Remove(spawnedEnemies[i]);
                    i--;
                }
            }

            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            if (respawnTime != 0)
            {
                if (onScreen)
                {
                    // return if spawner on screen
                    respawnTimer = respawnTime;
                    return;
                }
                else if (spawnedEnemies.Count == 0)
                {
                    // Spawn Enemy after respawn timer and enemy dead
                    if (respawnTimer < 0)
                    {
                        SpawnAnEnemy();
                        respawnTimer = respawnTime;
                    }
                    else
                    {
                        respawnTimer -= Time.deltaTime;
                    }
                }
            }
        }
    }

    bool SpawnAnEnemy()
    {
        // RNG to determine whether to spawn or not
        if (UnityEngine.Random.Range(0, 100) < modifierBehaviour.spawnPercentChance)
        {
            // Check if a spitter can spawn
            EnemyBase chosenEnemy = ChooseEnemyType();
            WallDirection wallDirection = IsAdjacentToWall();
            while (chosenEnemy.gameObject.GetComponent<Spitter>() && wallDirection == WallDirection.NONE)
            {
                chosenEnemy = ChooseEnemyType();
            }

            // Calculate Rotation for Spitter
            float enemyRotation = 0;
            switch (wallDirection)
            {
                case WallDirection.UP:
                    enemyRotation = 90;
                    break;
                case WallDirection.DOWN:
                    enemyRotation = 270;
                    break;
                case WallDirection.LEFT:
                    enemyRotation = 180;
                    break;
                case WallDirection.RIGHT:
                    enemyRotation = 0;
                    break;
                case WallDirection.NONE:
                    enemyRotation = 0;
                    break;
            }


            // Spawn Enemy at spawn point location
            EnemyBase newEnemy = Instantiate(chosenEnemy, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            newEnemy.transform.Rotate(new Vector3(0, 0, 1), enemyRotation);
            newEnemy.SetModifiers(modifierBehaviour);
            spawnedEnemies.Add(newEnemy);
            eventHandler.ChangeEnemyCount.Invoke(1);

            return true;
        }
        return false;
    }

    // Lil bit of cleanup
    void DestroySpawnedEnemies()
    {
        foreach (EnemyBase enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();
    }

    EnemyBase ChooseEnemyType()
    {
        EnemyBase returnVal = null;
        // Check modifier has been set up correctly
        if (modifierBehaviour.enemyTypes.Count != modifierBehaviour.enemyWeightings.Count)
        {
            Debug.Log("Modifier behaviour does not have equal enemy types and weightings");
        }
        else
        {
            int totalWeights = 0;
            // Create and fill list of weightings for each enemy type with upper and lower bounds
            List<Tuple<int, int>> weightingRanges = new List<Tuple<int, int>>();
            foreach (int weighting in modifierBehaviour.enemyWeightings)
            {
                weightingRanges.Add(new Tuple<int, int>(totalWeights, totalWeights + weighting));
                totalWeights += weighting;
            }
            // Choose a random number within total weighting range
            int random = UnityEngine.Random.Range(0, totalWeights);
            // Find where that random number landed
            for (int i = 0; i < weightingRanges.Count; i++)
            {
                if (random >= weightingRanges[i].Item1 && random < weightingRanges[i].Item2)
                {
                    returnVal = modifierBehaviour.enemyTypes[i];
                }
            }
        }
        // This should never happen
        if (returnVal == null)
        {
            Debug.Log("Oopsie");
        }
        return returnVal;
    }

    private WallDirection IsAdjacentToWall()
    {
        WallDirection wallDirection = WallDirection.NONE;

        RaycastHit2D up = Physics2D.Raycast(transform.position, transform.up, 1.00f);
        RaycastHit2D down = Physics2D.Raycast(transform.position, -transform.up, 1.00f);
        RaycastHit2D left = Physics2D.Raycast(transform.position, -transform.right, 1.00f);
        RaycastHit2D right = Physics2D.Raycast(transform.position, transform.right, 1.00f);
        RaycastHit2D[] rays = { up, down, left, right };

        for (int i = 0; i < rays.Length; i++)
        {
            if (rays[i].collider && !rays[i].collider.gameObject.CompareTag("Enemy"))
            {
                wallDirection = (WallDirection)i;
                return wallDirection;
            }
        }
        return wallDirection;
    }
}