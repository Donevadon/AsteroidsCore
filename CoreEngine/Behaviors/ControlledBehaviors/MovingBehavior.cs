using System;
using CoreEngine.Core;
using CoreEngine.Entities.Objects.ControlledObjects;

namespace CoreEngine.Behaviors.ControlledBehaviors
{
    internal class MovingBehavior : IDisposable
    {
        private readonly IAccelerationMovement _movement;
        private readonly IAccelerationRotate _rotate;
        private readonly float _rate;

        public event Action<float> SpeedChanged
        {
            add => _movement.SpeedChanged += value;
            remove => _movement.SpeedChanged -= value;
        }

        public MovingBehavior(IAccelerationMovement movement, IAccelerationRotate rotate, float rate)
        {
            _movement = movement;
            _rotate = rotate;
            _rate = rate;
        }

        public void Rotate(float acceleration)
        {
            _rotate.Acceleration = acceleration;
        }

        public void Move()
        {
            _movement.Acceleration += _rate;
            _movement.CalculateDirection(_rotate.Angle);
        }

        public void Dispose()
        {
            _movement.Dispose();
            _rotate.Dispose();
        }
    }
}