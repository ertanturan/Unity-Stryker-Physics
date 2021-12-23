using UnityEngine;

namespace Camera
{
    using System;

    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private float _distance = 15f;


        private void Update()
        {
            HandleCamera();
        }

        private void HandleCamera()
        {
            var transformPosition = transform.position;
            var targetTransformPosition = _targetTransform.position;

            Vector3 dir = transformPosition - targetTransformPosition;

            transformPosition = targetTransformPosition + Vector3.one*5;
            transform.position = transformPosition;

            transform.LookAt(_targetTransform);
        }
    }
}