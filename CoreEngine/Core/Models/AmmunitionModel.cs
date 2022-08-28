using System;
using System.Numerics;
using CoreEngine.Core.Configurations;
#pragma warning disable CS8618

namespace CoreEngine.Core.Models;

public class AmmunitionModel
{
    public MoveOptions MoveOptions { get; set; }
    public Vector2 Size { get; set; }
    public Action? AddScore { get; set; }
    public float LifeTime { get; set; }
}