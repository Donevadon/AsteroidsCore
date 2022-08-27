using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618

namespace CoreEngine.Core.Configurations
{
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public record Options
    {
        public ScreenSize ScreenSize;
        public PlayerOptions PlayerOptions;
        public AsteroidOptions AsteroidOptions;
        public AlienOptions AlienOptions;
    }
}