using System;
using CoreEngine.Entities.Objects.ControlledObjects;

namespace CoreEngine.Behaviors.ControlledBehaviors
{
    internal class MovingBehavior : IDisposable
    {
        private readonly IMotion _controller;
        private readonly IAccelerationMovement _movement;
        private readonly IAccelerationRotate _rotate;
        private readonly float _rate;

        public event Action<float> SpeedChanged
        {
            add => _movement.SpeedChanged += value;
            remove => _movement.SpeedChanged -= value;
        }

        public MovingBehavior(IMotion controller, IAccelerationMovement movement, IAccelerationRotate rotate, float rate)
        {
            _controller = controller;
            _movement = movement;
            _rotate = rotate;
            _rate = rate;

            _controller.Move += OnMove;
            _controller.Rotate += OnRotate;
        }

        private void OnRotate(float acceleration)
        {
            _rotate.Acceleration = acceleration;
        }

        private void OnMove()
        {
            _movement.Acceleration += _rate;
            _movement.CalculateDirection(_rotate.Angle);
        }

        public void Dispose()
        {
            _controller.Rotate -= OnRotate;
            _controller.Move -= OnMove;
            _movement.Dispose();
            _rotate.Dispose();
        }
    }
}