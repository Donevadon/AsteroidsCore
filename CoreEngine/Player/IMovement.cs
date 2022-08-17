using System;
using System.Numerics;

namespace CoreEngine.Player
{
    internal interface IMovement
    {
        void Move(float acceleration);
        void CalculateDirection(Vector3 rotationZ);
        event Action<Vector2> PositionChanged;
    }
}