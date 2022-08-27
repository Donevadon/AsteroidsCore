using System;
using System.Diagnostics.CodeAnalysis;

namespace CoreEngine.Core.Configurations;

[Serializable]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
public record ScreenSize
{
    public float Height;
    public float Width;
}