using UnityEngine;

namespace Wheels
{
    using System;
    using Input;

    public class BreakingWheel : Wheel
    {
        [SerializeField] private float _breakingPower = 5f;
        public bool IsBreaking { get; private set; }
        
        public override void HandleWheel(BaseVehicleInput input)
        {
            base.HandleWheel(input);
            if (Math.Abs(input.Brake - 1) < 0.01)
            {
                WheelCollider.brakeTorque = input.Brake * _breakingPower;
            }
            else
            {
                WheelCollider.brakeTorque = 0f;
                WheelCollider.motorTorque = .00000000000001f;
            }
        }
    }
}