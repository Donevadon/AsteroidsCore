using System;
using System.Numerics;

namespace CoreEngine.Entities
{
    public interface IMovement
    {
        Vector2 Position { get; }
        Vector2 Direction { get; }
        void Move(float deltaTime);
        void CalculateDirection(float angle);
        event Action<Vector2> PositionChanged;
    }
}