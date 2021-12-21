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

        protected override void Awake()
        {
            base.Awake();
            _BaseVehicleInput = GetComponent<BaseVehicleInput>();
            _StrykerController = GetComponent<StrykerController>();
            Rigidbody.centerOfMass += new Vector3(0, 0, 1.0f);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!IsStrykerRollderOver())
            {
                HandleWheels();
                HandleAntiTurn();
            }
            else
            {
                ResetAppliedForces();
            }
        }

        protected override void HandlePhysics()
        {
            Rigidbody.AddRelativeForce(transform.forward * _BaseVehicleInput.Forward * _throttleCoef);
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

            var groundedL = wheelLeft.GetGroundHit(out hit);

            if (groundedL)
            {
                travelL = (-wheelLeft.transform.InverseTransformPoint(hit.point).y - wheelLeft.radius) /
                          wheelLeft.suspensionDistance;
            }

            var groundedR = wheelRight.GetGroundHit(out hit);
            if (groundedR)
            {
                travelR = (-wheelRight.transform.InverseTransformPoint(hit.point).y - wheelRight.radius) /
                          wheelRight.suspensionDistance;
            }

            float antiRollForce = (travelL - travelR)/5;

            if (groundedL)
            {
                var transform1 = wheelLeft.transform;
                Rigidbody.AddForceAtPosition(transform1.up /5* -antiRollForce,
                    transform1.position);
            }

            if (groundedR)
            {
                var transform1 = wheelRight.transform;
                Rigidbody.AddForceAtPosition(transform1.up/5 * antiRollForce,
                    transform1.position);
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
    }
}