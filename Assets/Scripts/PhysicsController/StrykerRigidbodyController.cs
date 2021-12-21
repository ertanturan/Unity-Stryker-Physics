using Input;
using UnityEngine;
using VehicleController;

namespace PhysicsController
{
    using System.Linq;
    using System.Text;

    [RequireComponent(typeof(BaseVehicleInput), typeof(StrykerController))]
    public class StrykerRigidbodyController : BaseRigidbodyController
    {
        [SerializeField] private float _throttleCoef = 100f;
        protected BaseVehicleInput _BaseVehicleInput;
        protected StrykerController _StrykerController;
        public bool isIncreasing = false;

        protected override void Awake()
        {
            base.Awake();
            _BaseVehicleInput = GetComponent<BaseVehicleInput>();
            _StrykerController = GetComponent<StrykerController>();
            Rigidbody.centerOfMass += new Vector3(0, 0, 1.0f);
        }

        protected override void HandlePhysics()
        {
            if (!IsStrykerRollderOver())
            {
                HandleForwardForce();
                HandleWheels();
                HandleAntiTurn();
                HandleVelocity();
            }
            else
            {
                ResetAppliedForces();
            }
        }

        private void HandleForwardForce()
        {
            Vector3 forceToAdd = transform.forward * (_BaseVehicleInput.Forward * _throttleCoef * Time.fixedDeltaTime);
            Rigidbody.AddForce(forceToAdd, ForceMode.Force);
            isIncreasing = true;
        }

        private void HandleWheels()
        {
            for (int i = 0; i < _StrykerController.Wheels.Count; i++)
            {
                _StrykerController.Wheels[i].WheelCollider.motorTorque = Mathf.Lerp(
                    _StrykerController.Wheels[i].WheelCollider.motorTorque, _BaseVehicleInput.Forward * _throttleCoef,
                    Time.fixedDeltaTime);
            }
        }

        protected virtual void HandleAntiTurn()
        {
            WheelHit hit;
            float travelL = 1.0f;
            float travelR = 1.0f;

            WheelCollider wheelLeft = _StrykerController.Wheels[0].WheelCollider;
            WheelCollider wheelRight = _StrykerController.Wheels[4].WheelCollider;

            var groundedLeft = wheelLeft.GetGroundHit(out hit);

            if (groundedLeft)
            {
                travelL = (-wheelLeft.transform.InverseTransformPoint(hit.point).y - wheelLeft.radius) /
                          wheelLeft.suspensionDistance;
            }

            var groundedRight = wheelRight.GetGroundHit(out hit);
            if (groundedRight)
            {
                travelR = (-wheelRight.transform.InverseTransformPoint(hit.point).y - wheelRight.radius) /
                          wheelRight.suspensionDistance;
            }

            float antiRollForce = (travelL - travelR)*125;

            if (groundedLeft)
            {
                var transform1 = wheelLeft.transform;
                Rigidbody.AddForceAtPosition(transform1.up * -antiRollForce,
                    transform1.position, ForceMode.Impulse);
            }

            if (groundedRight)
            {
                var transform1 = wheelRight.transform;
                Rigidbody.AddForceAtPosition(transform1.up * antiRollForce,
                    transform1.position, ForceMode.Impulse);
            }
        }

        private bool IsStrykerRollderOver()
        {
            return _StrykerController.Wheels.Count(x => !x.WheelCollider.isGrounded) >=
                   _StrykerController.Wheels.Count / 2;
        }

        private void ResetAppliedForces()
        {
            Rigidbody.velocity = Vector3.zero;
            for (int i = 0; i < _StrykerController.Wheels.Count; i++)
            {
                _StrykerController.Wheels[i].ResetWheel();
            }
        }

        private void HandleVelocity()
        {
            if (_StrykerController.SpeedInKmh > _StrykerController.MaxSpeedInKmh)
            {
                Rigidbody.velocity = Rigidbody.velocity.normalized * _StrykerController.MaxSpeedInKmh /
                                     StrykerController.SPEED_COEF;
            }
        }
    }
}