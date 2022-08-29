using System;
using System.Numerics;

namespace CoreEngine.Core
{
    public interface IObject : IDisposable
    {
        void Update(float deltaTime);
        event Action<object> Destroyed;
        event Action<Vector2> PositionChanged;
        Vector2 Position { get; }
    }
}