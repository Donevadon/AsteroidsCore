using System;
using System.Numerics;

namespace CoreEngine.Player
{
    public interface IMovement
    {
        void Move();
        void CalculateDirection(Vector3 rotationZ);
        event Action<Vector2> PositionChanged;
    }
}