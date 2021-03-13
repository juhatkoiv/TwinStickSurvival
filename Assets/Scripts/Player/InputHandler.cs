using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(Player))]
public class InputHandler : MonoBehaviour
{
    private Player player = null;
    private MovementComponent movementComponent = null;
    private readonly Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    void Start()
    {
        player = GetComponent<Player>();
        movementComponent = GetComponent<MovementComponent>();
    }

    void Update()
    {
        HandleKeyboard();
        HandleMouse();
    }

    private void HandleTraverse()
    {
        Vector3 movement = new Vector3();
        Transform cameraTransform = GameCamera.Get().GetTransform();

        if (Input.GetKey(KeyCode.W))
        {
            movement = Vector3.ProjectOnPlane(cameraTransform.forward, groundPlane.normal);
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement = -Vector3.ProjectOnPlane(cameraTransform.forward, groundPlane.normal);
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement = -Vector3.ProjectOnPlane(cameraTransform.right, groundPlane.normal);
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement = Vector3.ProjectOnPlane(cameraTransform.right, groundPlane.normal);
        }

        movementComponent.SetTraverse(movement);
    }

    private void HandleFire()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            player.YieldWeapon();
        }
    }
    private void HandleRotate()
    {
        // Arbitrary plane instead of actual field GameObject because intersections 
        // have to be valid -inf < x,z < inf. 
        Ray screenToPointRay = GameCamera.Get().GetScreenToPointRay(Input.mousePosition);
        if (!groundPlane.Raycast(screenToPointRay, out float intersectAt))
            return;

        Vector3 positionOnPlane = screenToPointRay.GetPoint(intersectAt);
        movementComponent.RotateTowards(positionOnPlane);
    }

    private void HandleKeyboard()
    {
        HandleTraverse();
        HandleFire();
    }

    private void HandleMouse() 
    {

        HandleRotate();
    }
}
