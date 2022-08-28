using System;
using System.Numerics;

namespace CoreEngine.Core;

public interface ICollisionObject
{
    void OnCollision(ICollisionObject sender);
    bool IsCollision(ICollisionObject item);
    event Action<ICollisionObject> Updated;
    Vector2 Position { get; }
    Vector2 Size { get; }
}