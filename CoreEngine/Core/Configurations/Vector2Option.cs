using System;
using System.Diagnostics.CodeAnalysis;

namespace CoreEngine.Core.Configurations;

[Serializable]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
public record Vector2Option()
{
    public float X;
    public float Y;
};