using System;
using System.Diagnostics.CodeAnalysis;
#pragma warning disable CS8618

namespace CoreEngine.Core.Configurations;

[Serializable]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
public record PlayerOptions
{
    public float StartPositionX;
    public float StartPositionY;
    public float MoveSpeed;
    public float StartAngle;
    public float RotateSpeed;
    public GunOptions GunOptions;
    public Vector2Option Size;   
    public float Breaking;
    public float MoveRate;
}