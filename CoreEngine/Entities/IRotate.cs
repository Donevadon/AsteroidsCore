using System;
using System.Numerics;

namespace CoreEngine.Entities
{
    public interface IRotate
    {
        void Rotate(float acceleration);
        event Action<Vector3> RotationChanged;
    }
}