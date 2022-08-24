using System;
using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core
{
    public interface IAmmunitionFactory
    {
        IObject GetAmmo(MoveOptions moveOptions, Vector2 size, Action addScore);
        IObject GetLaser(MoveOptions options, Vector2 size, Action addScore);
    }
}