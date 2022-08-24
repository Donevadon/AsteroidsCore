using System.Numerics;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Behaviors
{
    public class PlayerRotation : Rotation, IAccelerationRotate
    {
        protected override float Acceleration { get; set; }

        float IAccelerationRotate.Acceleration
        {
            get => Acceleration;
            set
            {
                if (value > 1)
                {
                    Acceleration = 1;
                }
                else if (value < -1)
                {
                    Acceleration = -1;
                }
                else
                {
                    Acceleration = value;
                }
            }
        }
        
        public PlayerRotation(float startAngle, float speed) : base(startAngle, Vector3.UnitZ, speed)
        {
        }
    }
}