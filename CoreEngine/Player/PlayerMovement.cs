using System.Numerics;

namespace CoreEngine.Player
{
    public class PlayerMovement : Movement
    {
        private float _acceleration = 1;
        public PlayerMovement(Vector2 startPosition, Vector2 direction, float speed) : base(startPosition, direction, speed)
        {
        }

        public override float Acceleration
        {
            get => _acceleration;
            set
            {
                if (value > 1)
                {
                    _acceleration = 1;
                }else if (value < 0)
                {
                    _acceleration = 0;
                }
                else
                {
                    _acceleration = value;
                }
            }
        }

        public override void Move()
        {
            base.Move();

            Acceleration -= 0.01f;
        }
    }
}