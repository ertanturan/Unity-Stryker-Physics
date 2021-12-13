using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseRigidbodyController : MonoBehaviour
{
    protected Rigidbody Rigidbody;
    [SerializeField] private Transform _centerOfMass;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    protected virtual     void Start()
    {
        Rigidbody.centerOfMass = _centerOfMass.position;
    }

    protected virtual void FixedUpdate()
    {
        HandlePhysics();
    }

    protected abstract void HandlePhysics();
    
}