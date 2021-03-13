using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * An object pool for instances of GameObjects containing component Bullet.
 * - Each bullet lifetime is dictated by @BulletPoolItem.accumulatedSpawnTimeSecond and @bulletLifeTime
 * - maximum amount of objects in the pool is calculated by: bulletLifetime / maxSpawnRatePerTime.
 */
public class BulletPool : IObjectPool<Bullet>
{
    private class BulletPoolItem
    {
        public float accumulatedSpawnTimeSecond;
        public GameObject itemOwner;
    }
    
    private List<BulletPoolItem> bulletPool;
    private float bulletLifeTime;

    public BulletPool(GameObject bulletPrototype, float bulletLifeTime, float maxFireRatePerSecond)
    {
        if (!bulletPrototype)
        {
            Debug.LogError("BulletPool miscofigured - bulletPrototype == null.");
        }
        else 
        {
            if (maxFireRatePerSecond < Global.MIN_BULLET_SPAWN_RATE_PER_SECOND)
            {
                Debug.LogWarning("BulletPool miscofigured - maxFireRatePerSecond is less than MIN_BULLET_SPAWN_RATE_PER_SECOND. Setting maxFireRatePerSeconds to min.");
                maxFireRatePerSecond = Global.MIN_BULLET_SPAWN_RATE_PER_SECOND;
            }

            this.bulletLifeTime = bulletLifeTime;
            this.bulletPool = new List<BulletPoolItem>();

            float maxBulletsExisting = bulletLifeTime / maxFireRatePerSecond;
            int maxBulletsInt = (int)Mathf.Ceil(maxBulletsExisting) + 1; // make one extra

            for (int i = 0; i < maxBulletsInt; i++)
            {
                BulletPoolItem item = new BulletPoolItem
                {
                    itemOwner = GameObject.Instantiate(bulletPrototype),
                    accumulatedSpawnTimeSecond = 0.0f
                };
                item.itemOwner.SetActive(false);

                this.bulletPool.Add(item);
            }
        }
        
    }

    public Bullet Spawn(SpawnParams spawnParams)
    {
        if (bulletPool == null) 
        {
            Debug.LogError("BulletPool list null. Construction probably failed.");
            return null;
        }

        GameObject result = null;
        foreach (BulletPoolItem item in bulletPool)
        {
            if (!item.itemOwner.activeSelf)
            {
                result = item.itemOwner;
                break;
            } 
        }
        if (!result)
            return null;

        result.SetActive(true);
        result.transform.position = spawnParams.spawnPoint;
        result.transform.rotation = spawnParams.rotation;

        return result.GetComponent<Bullet>();
    }

    public void UpdatePool(float deltaTime)
    {
        if (bulletPool == null)
        {
            Debug.LogError("BulletPool list null. Construction probably failed.");
            return;
        }

        for (int i = 0; i < bulletPool.Count; i++)
        {
            BulletPoolItem item = bulletPool[i];
            if (item.itemOwner.activeSelf)
            {
                item.accumulatedSpawnTimeSecond += deltaTime;
                if (item.accumulatedSpawnTimeSecond < bulletLifeTime)
                    continue;

                item.itemOwner.SetActive(false);
                item.accumulatedSpawnTimeSecond = 0.0f;
            }
        }
    }


}
