namespace Input
{
    using UnityEngine;

    public sealed class StrykerInput : BaseVehicleInput
    {
        public bool SpeedBoost
        {
            get { return _speedBoost; }

            private set { _speedBoost = value; }
        }

        private bool _speedBoost;

        protected override void HandleInput()
        {
            base.HandleInput();
            _speedBoost = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }
    }
}