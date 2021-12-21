namespace VehicleController
{
    using System;
    using UnityEngine;

    public class StrykerController : BaseVehicleController
    {
        [SerializeField] private float _speedInKmh = 0;
        private Vector3 _currentPosition;

        private void Start()
        {
            _currentPosition = transform.position;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _speedInKmh = CalculateSpeedInKmh();
         
        }

        private float CalculateSpeedInKmh()
        {
            // Vector3 position = transform.position;
            // float result = Vector3.Distance(position,_currentPosition) / Time.fixedDeltaTime;
            // _currentPosition = position;
            // return result*10;
            return BaseRigidbodyController.Rigidbody.velocity.magnitude * 10;
        }
    }
}