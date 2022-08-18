using System;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace CoreEngine.Player
{
    public class Movement : IAccelerationMovement
    {
        private Vector2 _position;
        private Vector2 _direction;
        public virtual float Acceleration { get; set; } = 1f;

        public void CalculateDirection(Vector3 rotationZ)
        {
            var z = rotationZ.Z;

            _direction = new Vector2(1 * (float) Math.Cos(Math.PI / 180 * z), 1 * (float) Math.Sin(Math.PI / 180 * z));
        }

        public event Action<Vector2> PositionChanged;

        private readonly float _speed;

        public Movement(Vector2 startPosition, Vector2 direction, float speed)
        {
            _position = startPosition;
            _direction = direction;
            _speed = speed;
        }

        public virtual void Move()
        {
            _position += (_direction * Acceleration * (_speed * 0.02f));

            _position.X = _position.X > 9.5f || _position.X < -9.5f ? _position.X * -1 : _position.X;
            _position.Y = _position.Y > 5 || _position.Y < -5 ? _position.Y * -1 : _position.Y;
            PositionChanged?.Invoke(_position);
        }
    }
}