using System;
using System.Numerics;

namespace CoreEngine.Player
{
    public interface IRotate
    {
        void Rotate(float acceleration);
        event Action<Vector3> RotationChanged;
    }
}