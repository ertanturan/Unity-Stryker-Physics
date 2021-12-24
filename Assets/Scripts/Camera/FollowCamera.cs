using UnityEngine;

namespace Camera
{
    using System;
    using Input = UnityEngine.Input;

    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        private Vector3 _offset;
        [SerializeField] private float _distance = 15f;
        [SerializeField] private float _translationSpeed = 10f;
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
            transform.LookAt(_targetTransform);

            Vector3 rotateVector = Vector3.zero;
            rotateVector = Vector3.right * (Time.deltaTime * Input.GetAxis("Mouse X") * _rotationSpeed);
            // rotateVector += Vector3.up * (Time.deltaTime * Input.GetAxis("Mouse Y") * _rotationSpeed);
            transform.Translate(rotateVector);

            //
            // Vector3 newPosition = _targetTransform.position + _offset;
            // transform.position = newPosition;
            // Vector3 axis = Vector3.zero;
            // axis.x =-1* Input.GetAxis("Mouse Y") * Time.deltaTime;
            // axis.y = Input.GetAxis("Mouse X") * Time.deltaTime;
            // transform.Rotate(axis,_rotationSpeed * Time.deltaTime);
            //
        }
    }
}