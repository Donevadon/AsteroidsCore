using System;
using System.Numerics;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Entities.Objects
{
    public interface IGunBehavior
    {
        IGunBehavior Reload(float time);
        int Count();
        IGunBehavior Fire(MoveOptions options, Vector2 size, Action addScore);
    }
}