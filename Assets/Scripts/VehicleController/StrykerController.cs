namespace VehicleController
{
    using System;
    using Input;
    using UnityEditor;
    using UnityEngine;

    [RequireComponent(typeof(StrykerInput))]
    public class StrykerController : BaseVehicleController
    {
        public const int SPEED_COEF = 10;

        public float MaxSpeedInKmh
        {
            get { return _maxSpeedInKmh; }

            private set { _maxSpeedInKmh = value; }
        }

        [SerializeField] private float _maxSpeedInKmh = 125f;

        public float SpeedInKmh
        {
            get { return _speedInKmh; }

            private set { _speedInKmh = value; }
        }

        private float _speedInKmh;

        public float SpeedBoostPercentage
        {
            get { return _speedBoostPercentage; }

            private set { _speedBoostPercentage = value; }
        }

        [SerializeField] [Range(0, 1)] private float _speedBoostPercentage = 0.25f;

        public float ForwardForceCoef
        {
            get
            {
                if (_strykerInput.SpeedBoost)
                {
                    return _forwardForceCoef * (1 + _speedBoostPercentage);
                }

                return _forwardForceCoef;
            }

            private set { _forwardForceCoef = value; }
        }

        [SerializeField] private float _forwardForceCoef = 100f;


        [SerializeField] private AnimationCurve _curve;
        private StrykerInput _strykerInput;


        public AnimationCurve AccelerationCurve
        {
            get { return _curve; }
            private set { _curve = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            _strykerInput = BaseVehicleInput as StrykerInput;
        }

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