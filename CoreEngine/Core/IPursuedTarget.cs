using System;
using System.Numerics;

namespace CoreEngine.Core;

internal interface IPursuedTarget
{
    event Action<Vector2> PositionChanged;
    Vector2 Position { get; }
    event Action<object> Destroyed;
}