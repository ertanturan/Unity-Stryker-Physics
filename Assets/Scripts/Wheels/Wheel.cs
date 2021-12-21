using UnityEngine;

namespace Wheels
{
    using Input;

    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour
    {
        public WheelCollider WheelCollider { get; private set; }
        [SerializeField] private Transform _targetTransform;

        private void Awake()
        {
            WheelCollider = GetComponent<WheelCollider>();
            ResetMotorTorque();
        }

        public virtual void HandleWheel(BaseVehicleInput input)
        {
            if (_targetTransform && WheelCollider.isGrounded)
            {
                WheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

                _targetTransform.rotation = rotation;
                _targetTransform.position = position;
            }
        }

        public virtual void ResetWheel()
        {
            ResetMotorTorque();
            ResetBrakeTorque();
        }

        private void ResetMotorTorque()
        {
            WheelCollider.motorTorque = .00000000000001f;
        }

        private void ResetBrakeTorque()
        {
            WheelCollider.brakeTorque = 0f;
        }
    }
}