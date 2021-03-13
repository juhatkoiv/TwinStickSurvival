using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Wrapper class for some camera functions.
 * The caller doesn't have to be aware of possible camera changes.
 */
public class GameCamera
{
    private Camera camera;
    private Vector3 offset = new Vector3();

    private static GameCamera instance;
    public static GameCamera Get()
    {
        if (instance == null)
            instance = new GameCamera();

        return instance;
    }

    private GameCamera()
    {
        camera = Camera.main;
    }

    public void SetPlayerCameraOffset(Player player)
    {
        if (!player)
        {
            Debug.LogError("Player == null");
            return;
        }

        offset = camera.transform.position - player.transform.position;
    }

    public void SyncWithPlayer(Player player)
    {
        if (!player)
        {
            Debug.LogError("Player == null");
            return;
        }

        camera.transform.position = offset + player.transform.position;
        camera.transform.LookAt(player.transform.position);
    }

    public Plane[] GetFrustumPlanes()
    {
        return GeometryUtility.CalculateFrustumPlanes(camera);
    }
    
    public Transform GetTransform() 
    {
        return camera.transform;
    }

    public Ray GetScreenToPointRay(Vector3 screenPoint) 
    {
        return camera.ScreenPointToRay(screenPoint);
    }
}
