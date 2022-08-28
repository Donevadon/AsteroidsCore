using System.Numerics;

namespace CoreEngine.Behaviors.ControlledBehaviors
{
    public class RotationByDynamicAcceleration : RotationByStaticAcceleration, IAccelerationRotate
    {
        protected override float Acceleration { get; set; }

        float IAccelerationRotate.Acceleration
        {
            get => Acceleration;
            set
            {
                Acceleration = value switch
                {
                    > 1 => 1,
                    < -1 => -1,
                    _ => value
                };
            }
        }
        
        public RotationByDynamicAcceleration(float startAngle, float speed) : base(startAngle, Vector3.UnitZ, speed, 0)
        {
        }
    }
}