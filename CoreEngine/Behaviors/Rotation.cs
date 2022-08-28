using System;
using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class Rotation : IRotate
    {
        private readonly Vector3 _direction;
        private readonly float _speed;
        private float _angle;
        
        public event Action<float>? RotationChanged;
        
        public float Angle
        {
            get => _angle;
            private set
            {
                _angle = value switch
                {
                    > 360 => value - 360,
                    < 0 => value + 360,
                    _ => value
                };
            } 
        }

        protected virtual float Acceleration { get; set; } = 1f;

        public Rotation(float startAngle, Vector3 direction, float speed)
        {
            Angle = startAngle;
            _direction = direction;
            _speed = speed;
        }
        
        public virtual void Rotate(float deltaTime)
        {
            if (Acceleration == 0) return;
            
            Angle += (_direction * Acceleration * (_speed * deltaTime)).Z;
                
            RotationChanged?.Invoke(Angle);
        }
    }
}