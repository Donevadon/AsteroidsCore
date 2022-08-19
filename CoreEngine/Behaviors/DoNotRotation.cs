using System;
using System.Numerics;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors
{
    public class DoNotRotation : IRotate
    {
        public void Rotate(float acceleration)
        {
            
        }

        public event Action<Vector3> RotationChanged;
    }
}