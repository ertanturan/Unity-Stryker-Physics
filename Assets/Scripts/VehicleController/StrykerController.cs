namespace VehicleController
{
    using System;
    using UnityEngine;

    public class StrykerController : BaseVehicleController
    {
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
            if (_speedInKmh <= _maxSpeedInKmh)
            {
                _speedInKmh = CalculateSpeedInKmh();
            }
        }

        private float CalculateSpeedInKmh()
        {
            return BaseRigidbodyController.Rigidbody.velocity.magnitude * 10;
        }
    }
}