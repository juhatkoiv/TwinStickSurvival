using System.Collections.Generic;
using UnityEngine;

/*
 * Object pool for instances of GameObjects containing component Enemy.
 */
public class EnemyPool : IObjectPool<Enemy>
{
    private float spawnFrequencySeconds = 0.0f;
    private uint maxEnemiesSpawned = 0;

    private uint enemiesSpawned = 0;
    private float lastEnemySpawnedTimeSeconds = 0.0f;

    private List<GameObject> enemyPool;

    public EnemyPool(GameObject enemyPrototype, float spawnFrequencySeconds, uint maxEnemiesSpawned)
    {
        if (!enemyPrototype)
        {
            Debug.LogError("Enemy prototype null.");
        }
        else
        {
            this.spawnFrequencySeconds = spawnFrequencySeconds;
            this.maxEnemiesSpawned = maxEnemiesSpawned;
            this.enemyPool = new List<GameObject>();

            for (int i = 0; i < (int)maxEnemiesSpawned + 1; i++)
            {
                var obj = GameObject.Instantiate(enemyPrototype);
                obj.SetActive(false);
                this.enemyPool.Add(obj);
            }

            lastEnemySpawnedTimeSeconds = spawnFrequencySeconds;

            Game.EnemyKilledEvent.AddListener(OnEnemyKilled);
        }
    }

    public Enemy Spawn(SpawnParams spawnParams)
    {
        if (enemyPool == null)
        {
            Debug.LogError("Enemy pool null. Construction probably failed.");
            return null;
        }

        if (!CanSpawn())
            return null;

        GameObject result = null;
        foreach (GameObject obj in enemyPool)
        {
            if (!obj.activeSelf)
            {
                result = obj;
                break;
            }
        }

        if (!result)
            return null;

        result.SetActive(true);
        result.transform.position = spawnParams.spawnPoint;
        result.transform.rotation = spawnParams.rotation;

        lastEnemySpawnedTimeSeconds = 0.0f;
        enemiesSpawned++;

        return result.GetComponent<Enemy>();
    }

    public void UpdatePool(float deltaTime)
    {
        if (enemiesSpawned < maxEnemiesSpawned)
        {
            lastEnemySpawnedTimeSeconds += deltaTime;
            return;
        }
    }

    private void OnEnemyKilled() 
    {
        enemiesSpawned--;
    }

    private bool CanSpawn()
    {
        return (lastEnemySpawnedTimeSeconds >= spawnFrequencySeconds) && (enemiesSpawned < maxEnemiesSpawned);
    }

}
