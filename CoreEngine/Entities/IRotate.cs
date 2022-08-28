using System;
using System.Numerics;

namespace CoreEngine.Entities
{
    public interface IRotate : IDisposable
    {
        void Rotate(float deltaTime);
        event Action<float> RotationChanged;
        float Angle { get; }
    }
}