using System;
using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class PlayerMovement : Movement, IAccelerationMovement
    {
        private readonly float _speed;
        private const float Braking = 0.001f;

        protected override float Acceleration { get; set; }
        public event Action<float> SpeedChanged;

        float IAccelerationMovement.Acceleration
        {
            get => Acceleration;
            set
            {
                if (value > 1)
                {
                    Acceleration = 1;
                }else if (value < 0)
                {
                    Acceleration = 0;
                }
                else
                {
                    Acceleration = value;
                }
                SpeedChanged?.Invoke(Acceleration * _speed);
            }
        }
        
        public PlayerMovement(Vector2 startPosition, float direction, float speed, Vector2 screenSize) : base(startPosition, direction, speed, screenSize)
        {
            _speed = speed;
        }

        public override void Move(float deltaTime)
        {
            base.Move(deltaTime);
            
            var controller = this as IAccelerationMovement;
            controller.Acceleration -= Braking;
        }
    }
}