using Input;
using UnityEngine;
using VehicleController;

namespace PhysicsController
{
    using System;
    using System.Linq;
    using System.Text;

    [RequireComponent(typeof(StrykerInput), typeof(StrykerController))]
    public class StrykerRigidbodyController : BaseRigidbodyController
    {
        protected StrykerInput _BaseVehicleInput;
        protected StrykerController _StrykerController;

        protected override void Awake()
        {
            base.Awake();
            _BaseVehicleInput = GetComponent<StrykerInput>();
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
            Transform thisTransform = transform;
            var forward = thisTransform.forward;
            Vector3 forceDirection = forward;

            float degree = Vector3.Angle(thisTransform.up, Vector3.forward) - 90;
            float inversedDegree = Mathf.InverseLerp(0, 45, degree);
            float forwardForceCoef = _StrykerController.ForwardForceCoef;
            float extraPowerForSlope = 1800f;


            // forwardForceCoef += forwardForceCoef ; // steep throttle support
            if (Rigidbody.velocity.magnitude < 1.75f && degree > 6)
            {
                // forwardForceCoef *= 1.7f;
                // forwardForceCoef += Rigidbody.mass / 1.5f;
                forwardForceCoef += extraPowerForSlope * inversedDegree;
                Debug.Log("slope assist involved...");
            }

            Debug.Log(forwardForceCoef);

            float normalizedKmh = Mathf.InverseLerp(0, _StrykerController.MaxSpeedInKmh,
                _StrykerController.SpeedInKmh);

            float forwardPower = _StrykerController.AccelerationCurve.Evaluate(normalizedKmh) *
                                 forwardForceCoef;

            Rigidbody.AddForce(forceDirection * (forwardPower * Time.fixedDeltaTime));
        }

        private void HandleWheels()
        {
            for (int i = 0; i < _StrykerController.Wheels.Count; i++)
            {
                _StrykerController.Wheels[i].WheelCollider.motorTorque = Mathf.Lerp(
                    _StrykerController.Wheels[i].WheelCollider.motorTorque,
                    _BaseVehicleInput.Forward * _StrykerController.ForwardForceCoef,
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

            float antiRollForce = (travelL - travelR) * 125;

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
                var velocity = Rigidbody.velocity;
                Rigidbody.velocity = Vector3.Lerp(velocity,
                    velocity.normalized * _StrykerController.MaxSpeedInKmh /
                    StrykerController.SPEED_COEF, Time.fixedDeltaTime * 5);
            }
        }
    }
}