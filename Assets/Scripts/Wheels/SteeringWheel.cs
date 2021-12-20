namespace Wheels
{
    using Input;
    using UnityEngine;

    public class SteeringWheel : Wheel
    {
        protected float SteeringAngle=15;
        private float _smoothSteerAngle = 2f;
        
        public override void HandleWheel(BaseVehicleInput input)
        {
            base.HandleWheel(input);
            _smoothSteerAngle = Mathf.Lerp(_smoothSteerAngle,
                input.Yaw * SteeringAngle, Time.deltaTime * input.SteerSmoothSpeed);
            WheelCollider.steerAngle = _smoothSteerAngle;
        }
    }
}
