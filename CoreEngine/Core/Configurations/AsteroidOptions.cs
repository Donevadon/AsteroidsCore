using System;
using System.Diagnostics.CodeAnalysis;
#pragma warning disable CS8618

namespace CoreEngine.Core.Configurations
{
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public record AsteroidOptions
    {
        public float MoveSpeed;
        public float RotateSpeed;
        public int FragmentCount;
        public FragmentAsteroidOptions FragmentAsteroidOptions;
        public Vector2Option Size;
    }
}