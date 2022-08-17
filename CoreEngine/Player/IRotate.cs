using System;
using System.Numerics;

namespace CoreEngine.Player
{
    internal interface IRotate
    {
        void Rotate(float acceleration);
        event Action<Vector3> RotationChanged;
    }
}