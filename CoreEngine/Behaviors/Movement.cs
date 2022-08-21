using System;
using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class Movement : IMovement
    {
        private readonly float _speed;
        private readonly Vector2 _screenSize;

        private Vector2 _position;
        private Vector2 _direction;
        
        public event Action<Vector2> PositionChanged;

        protected virtual float Acceleration { get; set; } = 1f;
        public Vector2 Position => _position;

        public Movement(Vector2 startPosition, float angle, float speed, Vector2 screenSize)
        {
            _position = startPosition;
            _speed = speed;
            _screenSize = screenSize;
            CalculateDirection(angle);
        }
        
        public void CalculateDirection(float angle)
        {
            _direction = new Vector2((float) Math.Cos(Math.PI / 180 * angle), (float) Math.Sin(Math.PI / 180 * angle));
        }

        
        public virtual void Move(float deltaTime)
        {
            if (Acceleration != 0)
            {           
                _position += _direction * Acceleration * (_speed * deltaTime);
             
                _position.X = _position.X > _screenSize.X || _position.X < -_screenSize.X ? _position.X * -1 : _position.X;
                _position.Y = _position.Y > _screenSize.Y || _position.Y < -_screenSize.Y ? _position.Y * -1 : _position.Y;

                PositionChanged?.Invoke(_position);
            }
        }
    }
}