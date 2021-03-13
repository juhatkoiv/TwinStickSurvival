using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    private float maxVelocity;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetTraverse(Vector3 traverse) 
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if ((rb.velocity + traverse).sqrMagnitude < maxVelocity)
            rb.velocity += traverse;
    }

    public void RotateTowards(Vector3 lookAtPosition)
    {
        // let's not look up or down
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);
    }
}

