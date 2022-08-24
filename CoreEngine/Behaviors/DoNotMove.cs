using System;
using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class DoNotMove : IMovement
    {
        public Vector2 Position { get; }
        public Vector2 Direction { get; set; }

        public void Move(float deltaTime)
        {
            
        }

        public void CalculateDirection(float angle)
        {
        }

        public event Action<Vector2> PositionChanged;
    }
}