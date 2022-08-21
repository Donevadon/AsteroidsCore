using System;
using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class DoNotRotation : IRotate
    {
        public void Rotate(float deltaTime)
        {
        }

        public event Action<float> RotationChanged;
        public float Angle => 0;
    }
}