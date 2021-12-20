using Input;
using UnityEngine;
using VehicleController;

namespace PhysicsController
{
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
        }


        protected override void HandlePhysics()
        {
            Rigidbody.AddRelativeForce(transform.forward * _BaseVehicleInput.Forward * _throttleCoef);

            for (int i = 0; i < _StrykerController.Wheels.Count; i++)
            {
                _StrykerController.Wheels[i].WheelCollider.motorTorque = Mathf.Lerp(
                    _StrykerController.Wheels[i].WheelCollider.motorTorque, _BaseVehicleInput.Forward * _throttleCoef,
                    Time.fixedDeltaTime);

            }
        }
    }
}