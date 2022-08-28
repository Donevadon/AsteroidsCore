using System;
using System.Numerics;

namespace CoreEngine.Core;

internal interface IPursuer
{
    event Action<Vector2> PositionChanged;
    event Action<float> RotationChanged;
    event Action<object> Destroyed;
}