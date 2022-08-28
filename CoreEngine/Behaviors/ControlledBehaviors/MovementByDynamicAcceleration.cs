using System;
using System.Numerics;

namespace CoreEngine.Behaviors.ControlledBehaviors
{
    public class MovementByDynamicAcceleration : MovementByStaticAcceleration, IAccelerationMovement
    {
        private readonly float _speed;
        private readonly float _breaking;

        protected override float Acceleration { get; set; }
        public event Action<float>? SpeedChanged;

        float IAccelerationMovement.Acceleration
        {
            get => Acceleration;
            set
            {
                Acceleration = value switch
                {
                    > 1 => 1,
                    < 0 => 0,
                    _ => value
                };
                SpeedChanged?.Invoke(Acceleration * _speed);
            }
        }
        
        public MovementByDynamicAcceleration(Vector2 startPosition, float direction, float speed, Vector2 screenSize, float breaking)
            : base(startPosition, direction, speed, 0, screenSize)
        {
            _speed = speed;
            _breaking = breaking;
        }

        public override void Move(float deltaTime)
        {
            base.Move(deltaTime);
            
            var controller = this as IAccelerationMovement;
            controller.Acceleration -= _breaking;
        }
    }
}