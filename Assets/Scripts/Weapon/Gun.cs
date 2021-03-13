using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField]
    private GameObject spawnPointOwner;

    [SerializeField]
    private GameObject bulletPrototoype;

    [SerializeField]
    private float bulletVelocity;

    [SerializeField]
    private float bulletLifeTime;

    [SerializeField]
    private float maxFireRatePerSecond;

    private float timeSinceLastFireSeconds;

    private IObjectPool<Bullet> bulletPool;

    void Start()
    {
        bulletPool = new BulletPool(bulletPrototoype, bulletLifeTime, maxFireRatePerSecond);
        timeSinceLastFireSeconds = 0.0f;
    }

    void Update()
    {
        timeSinceLastFireSeconds += Time.deltaTime;

        bulletPool.UpdatePool(Time.deltaTime);
    }

    public bool CanFire()
    {
        if (!spawnPointOwner)
            return false;

        if (bulletPool == null)
            return false;

        if (bulletVelocity <= 0.0f)
            return false;
         
        if (maxFireRatePerSecond * timeSinceLastFireSeconds < 1.0f)
            return false;

        return true;
    }

    public void Fire() 
    {
        if (!CanFire())
            return;

        SpawnParams spawnParams = new SpawnParams();
        spawnParams.rotation = Quaternion.identity;
        spawnParams.spawnPoint = spawnPointOwner.transform.position;

        Bullet bullet = bulletPool.Spawn(spawnParams);
        if (!bullet)
            return;
       
        bullet.Velocity = (spawnPointOwner.transform.position - transform.position).normalized * bulletVelocity;
        timeSinceLastFireSeconds = 0.0f;
    }
}
