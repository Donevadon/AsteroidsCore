using System;
using System.Diagnostics.CodeAnalysis;
#pragma warning disable CS8618

namespace CoreEngine.Core.Configurations
{
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public record LaserOptions
    {
        public Vector2Option Size;
        public float ReloadTime;
        public float LifeTime;
    }
}