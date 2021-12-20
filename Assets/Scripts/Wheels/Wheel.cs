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
            WheelCollider.motorTorque = .00000000000001f;
        }

        public virtual void HandleWheel(BaseVehicleInput input)
        {
            if (_targetTransform)
            {
                WheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

                _targetTransform.rotation = rotation;
                _targetTransform.position = position;
            }
        }
    }
}