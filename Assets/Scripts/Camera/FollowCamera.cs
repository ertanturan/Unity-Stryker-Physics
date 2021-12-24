using UnityEngine;

namespace Camera
{
    using System;
    using Input = UnityEngine.Input;

    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        private Vector3 _offset;

        [SerializeField] private float _rotationSpeed = 10f;


        private void Start()
        {
            _offset = transform.position - _targetTransform.position;
        }

        private void LateUpdate()
        {
            HandleCamera();
        }

        private void HandleCamera()
        {
            float xAxis = Input.GetAxis("Mouse X") * _rotationSpeed;
            float yAxis = Input.GetAxis("Mouse Y") * _rotationSpeed;

            Quaternion xTurnAngle =
                Quaternion.AngleAxis(xAxis, Vector3.up);

            Quaternion yTurnAngle = Quaternion.AngleAxis(yAxis, Vector3.left);

            _offset = xTurnAngle * _offset;
            _offset = yTurnAngle * _offset;

            Vector3 newPosition = _offset + _targetTransform.position;

            if (newPosition.y > 2f)
            {
                transform.position = newPosition;
            }

            transform.LookAt(_targetTransform);
        }
    }
}