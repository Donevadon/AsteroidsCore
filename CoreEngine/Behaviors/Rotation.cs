using System;
using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class Rotation : IRotate
    {
        private float _angle;
        private readonly Vector3 _direction;
        private readonly float _speed;
        
        public event Action<float> RotationChanged;
        public float Angle => _angle;

        protected virtual float Acceleration { get; set; } = 1f;

        public Rotation(float startAngle, Vector3 direction, float speed)
        {
            _angle = startAngle;
            _direction = direction;
            _speed = speed;
        }
        
        public virtual void Rotate(float deltaTime)
        {
            if (Acceleration != 0)
            {
                _angle += (_direction * Acceleration * (_speed * deltaTime)).Z;

                if (_angle > 180)
                {
                    var a = _angle - 180;
                    _angle = -180 + a;
                }
                else if (_angle < -180)
                {
                    var a = -180 - _angle;
                    _angle = 180 - a;
                }

                RotationChanged?.Invoke(_angle);
            }
        }
    }
}