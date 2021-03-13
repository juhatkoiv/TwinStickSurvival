using UnityEngine;

public struct SpawnParams 
{
    public Vector3 spawnPoint;
    public Quaternion rotation;
}

public interface IObjectPool<T> where T : MonoBehaviour
{
    void UpdatePool(float deltaTime);
    T Spawn(SpawnParams spawnParams);
}
