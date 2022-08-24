using System;
using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class Rotation : IRotate
    {
        private readonly Vector3 _direction;
        private readonly float _speed;
        
        public event Action<float> RotationChanged;
        public float Angle { get; private set; }

        protected virtual float Acceleration { get; set; } = 1f;

        public Rotation(float startAngle, Vector3 direction, float speed)
        {
            Angle = startAngle;
            _direction = direction;
            _speed = speed;
        }
        
        public virtual void Rotate(float deltaTime)
        {
            if (Acceleration != 0)
            {
                Angle += (_direction * Acceleration * (_speed * deltaTime)).Z;

                if (Angle > 180)
                {
                    var a = Angle - 180;
                    Angle = -180 + a;
                }
                else if (Angle < -180)
                {
                    var a = -180 - Angle;
                    Angle = 180 - a;
                }

                RotationChanged?.Invoke(Angle);
            }
        }
    }
}