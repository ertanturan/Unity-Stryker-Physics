using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Input;
using UnityEngine;
using Wheels;

[RequireComponent(typeof(BaseVehicleInput))]
public class StrykerPhysicsController : BaseRigidbodyController
{
    protected BaseVehicleInput _baseVehicleInput;
    [SerializeField] private float _throttleCOEF=100f;

    private List<Wheel> _wheels = new List<Wheel>(6);
    protected override void Awake()
    {
        base.Awake();
        _baseVehicleInput = GetComponent<BaseVehicleInput>();
        _wheels = FindObjectsOfType<Wheel>().ToList();
    }


    protected override void HandlePhysics()
    {
        // Rigidbody.AddForce(transform.forward*_baseVehicleInput.Throttle*_throttleCOEF);
        Rigidbody.AddRelativeForce(transform.forward*_baseVehicleInput.Forward*_throttleCOEF);

        for (int i = 0; i < _wheels.Count; i++)
        {
            _wheels[i].WheelCollider.motorTorque = _baseVehicleInput.Forward * _throttleCOEF;
        }
    }
}