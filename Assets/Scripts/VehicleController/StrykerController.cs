namespace VehicleController
{
    using System;
    using UnityEngine;

    public class StrykerController : BaseVehicleController
    {
        public const int SPEED_COEF = 10;

        public float MaxSpeedInKmh
        {
            get { return _maxSpeedInKmh; }

            private set { _maxSpeedInKmh = value; }
        }

        [SerializeField] private float _maxSpeedInKmh = 125f;

        
        // [SerializeField] private float _speedInKmh = 0;


        public float SpeedInKmh
        {
            get { return _speedInKmh; }

            private set { _speedInKmh = value; }
        }

        private float _speedInKmh;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            _speedInKmh = CalculateSpeedInKmh();
        }

        private float CalculateSpeedInKmh()
        {
            return BaseRigidbodyController.Rigidbody.velocity.magnitude * SPEED_COEF;
        }
    }
}