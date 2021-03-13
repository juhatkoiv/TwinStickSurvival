using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  AI class for enemies 
 *  - Currently, handles enemy movement logic.
 *  
 *  TODO - make a base class or interface and give to Enemy as member
 */
[RequireComponent(typeof(MovementComponent), typeof(Collider))]
public class EnemyAI : MonoBehaviour
{
    private Player player;
    private MovementComponent movementComponent;
    private float colliderSizeY;

    [SerializeField]
    private float groundedMargin;

    void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
        colliderSizeY = GetComponent<Collider>().bounds.extents.y;

        // would not rather use FindObjectOfType, but it's  the most straigthforward
        player = FindObjectOfType<Player>();
        if (!player)
        {
            Debug.LogError("Player is not spawned.");
            return;
        }
    }

    void Update()
    {
        if (!IsGrounded())
            return;

        movementComponent.RotateTowards(player.transform.position);
        movementComponent.SetTraverse(transform.forward);
    }

    private bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, Vector3.down, colliderSizeY + groundedMargin);
    }
}
