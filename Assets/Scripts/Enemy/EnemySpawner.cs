using System.Collections.Generic;
using UnityEngine;

/*
 * Enemy spawner:
 * - spawns enemies every @spawnFrequencySeconds seconds using enemy pool.
 * - maximum number of live enemies at the same time is dictated by @maxEnemiesSpawned
 * - TODO - if more types of enemies are added, make an EnemyFactory and make this class use that factory.
 */
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrototype;

    [SerializeField]
    private float spawnFrequencySeconds = 0.0f;

    [SerializeField]
    private uint maxEnemiesSpawned = 0;

    [SerializeField]
    private List<GameObject> spawnPoints;

    private List<Bounds> spawnPointBounds;
    private IObjectPool<Enemy> enemyPool;
    private SpawnParams nextSpawnParams;

    void Start()
    {
        if (!enemyPrototype)
        {
            Debug.LogError("Enemy prototype == null.");
            return;
        }

        enemyPool = new EnemyPool(enemyPrototype, spawnFrequencySeconds, maxEnemiesSpawned);
        spawnPointBounds = new List<Bounds>();

        Vector3 enemySize = enemyPrototype.transform.lossyScale;
        foreach (GameObject spawnPoint in spawnPoints) 
        {
            spawnPointBounds.Add(new Bounds(spawnPoint.transform.position, enemySize));
        }
        SetNextSpawnParams();
    }

    void Update()
    {
        if (TrySpawn()) 
        {
            SetNextSpawnParams();
        }
        
        if (enemyPool == null)
            return;

        enemyPool.UpdatePool(Time.deltaTime);
    }

    private bool TrySpawn()
    {
        if (enemyPool == null)
            return false;

        Enemy enemy = enemyPool.Spawn(nextSpawnParams);
        return enemy != null;
    }

    private void SetNextSpawnParams() 
    {
        nextSpawnParams = new SpawnParams();
        nextSpawnParams.spawnPoint = GetNextSpawnParamsPosition();
        nextSpawnParams.rotation = Quaternion.identity;
    }

    private Vector3 GetNextSpawnParamsPosition()
    {
        Plane[] planes = GameCamera.Get().GetFrustumPlanes();

        Vector3 result = new Vector3();
        for (int i = 0; i < spawnPointBounds.Count; i++) 
        {
            if (!GeometryUtility.TestPlanesAABB(planes, spawnPointBounds[i]))
            {
                result = spawnPointBounds[i].center;
                break;
            }
        }
        return result;
    }

}
