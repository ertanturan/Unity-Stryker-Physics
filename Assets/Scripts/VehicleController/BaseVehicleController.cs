using UnityEngine;

namespace VehicleController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Input;
    using PhysicsController;
    using Wheels;

    [RequireComponent(typeof(BaseVehicleInput), typeof(BaseRigidbodyController))]
    public class BaseVehicleController : MonoBehaviour
    {
        public List<Wheel> Wheels { get; private set; }
        public BaseVehicleInput BaseVehicleInput { get; private set; }
        public BaseRigidbodyController BaseRigidbodyController { get; private set; }


        private void Awake()
        {
            Wheels = GetComponentsInChildren<Wheel>().ToList();
            BaseVehicleInput = GetComponent<BaseVehicleInput>();
            BaseRigidbodyController = GetComponent<BaseRigidbodyController>();
        }

        protected virtual void FixedUpdate()
        {
            HandleWheels();
        }

        protected virtual void HandleWheels()
        {
            for (int i = 0; i < Wheels.Count; i++)
            {
                Wheels[i].HandleWheel(BaseVehicleInput);
            }
        }
    }
}