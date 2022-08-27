using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618

namespace CoreEngine.Core.Configurations
{
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public record GunOptions
    {
        public BulletOptions BulletOptions;
        public LaserOptions LaserOptions;
    }
}