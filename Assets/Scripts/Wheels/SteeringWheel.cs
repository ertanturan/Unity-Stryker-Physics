namespace Wheels
{
    using Input;
    using UnityEngine;

    public class SteeringWheel : Wheel
    {
        private float _steerSmoothSpeed=0.5f;

        protected float SteeringAngle=15;
        private float _smoothSteerAngle = 2f;
        
        public override void HandleWheel(BaseVehicleInput input)
        {
            base.HandleWheel(input);
            _smoothSteerAngle = Mathf.Lerp(_smoothSteerAngle,
                input.Yaw * SteeringAngle, Time.deltaTime * _steerSmoothSpeed);
            WheelCollider.steerAngle = _smoothSteerAngle;
        }
    }
}
