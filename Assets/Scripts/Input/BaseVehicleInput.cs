namespace Input
{
    using UnityEngine;

    public class BaseVehicleInput : MonoBehaviour
    {
        #region Fields

        protected float _Forward = 0f;
        protected float _Yaw=0f;
        protected float _Brake = 0f;
        protected float ForwardSpeed = 0.03f;

        [SerializeField] private KeyCode _brakeKey = KeyCode.Space;

        [SerializeField] private KeyCode _cameraKey = KeyCode.X;
        protected bool _CameraSwitch = false;

        #endregion

        #region Properties

        public float Forward
        {
            get { return _Forward; }
        }

        public float Brake
        {
            get { return _Brake; }
        }

        public float Yaw
        {
            get { return _Yaw; }
        }


        public bool CameraSwitch
        {
            get { return _CameraSwitch; }
        }

        #endregion

        #region BuiltIn Methods

        public virtual void Update()
        {
            HandleInput();
        }

        #endregion

        #region Custom Methods

        protected virtual void HandleInput()
        {
            //Main controls
            _Forward = Input.GetAxis("Vertical");

            //Brakes
            _Brake = Input.GetKey(_brakeKey) ? 1f : 0f;

            _Yaw = Input.GetAxis("Horizontal");
            _CameraSwitch = Input.GetKeyDown(_cameraKey);
        }

        #endregion
    }
}