using System;
using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class Movement : IMovement
    {
        private readonly float _speed;
        private readonly Vector2 _screenSize;

        public event Action<Vector2>? PositionChanged;

        
        private Vector2 _position;
        public Vector2 Position => _position;
        public Vector2 Direction { get; private set; }
        protected virtual float Acceleration { get; set; } = 1f;

        public Movement(Vector2 startPosition, float angle, float speed, Vector2 screenSize)
        {
            _position = startPosition;
            _speed = speed;
            _screenSize = screenSize;
            CalculateDirection(angle);
        }
        
        public void CalculateDirection(float angle)
        {
            Direction = new Vector2((float) Math.Cos(Math.PI / 180 * angle), (float) Math.Sin(Math.PI / 180 * angle));
        }

        
        public virtual void Move(float deltaTime)
        {
            if (Acceleration == 0) return;
            
            _position += Direction * Acceleration * (_speed * deltaTime);
             
            _position.X = PositionRelativeToTheScreen(_position.X, _screenSize.X);
            _position.Y = PositionRelativeToTheScreen(_position.Y, _screenSize.Y);

            PositionChanged?.Invoke(_position);
        }

        private static float PositionRelativeToTheScreen(float positionSide, float screenSide) => 
            positionSide > screenSide || positionSide < -screenSide ? positionSide * -1 : positionSide;
    }
}