using UnityEngine;

namespace Wheels
{
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

        private void FixedUpdate()
        {
            HandleWheel();
        }

        protected virtual void HandleWheel()
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