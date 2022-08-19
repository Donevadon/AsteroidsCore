using System;
using System.Numerics;

namespace CoreEngine.Entities
{
    public interface IMovement
    {
        Vector2 Position { get; }
        void Move();
        void CalculateDirection(Vector3 rotationZ);
        event Action<Vector2> PositionChanged;
    }
}