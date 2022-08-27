using System;
using System.Diagnostics.CodeAnalysis;
#pragma warning disable CS8618

namespace CoreEngine.Core.Configurations
{
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public record AlienOptions
    {
        public float MoveSpeed;
        public float RotateSpeed;
        public Vector2Option Size;
    }
}