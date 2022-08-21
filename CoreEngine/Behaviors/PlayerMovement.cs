using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class PlayerMovement : Movement, IAccelerationMovement
    {
        private const float Braking = 0.001f;

        protected override float Acceleration { get; set; }

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
            }
        }
        
        public PlayerMovement(Vector2 startPosition, float direction, float speed, Vector2 screenSize) : base(startPosition, direction, speed, screenSize)
        {
        }

        public override void Move(float deltaTime)
        {
            base.Move(deltaTime);

            var controller = this as IAccelerationMovement;
            controller.Acceleration -= Braking;
        }
    }
}