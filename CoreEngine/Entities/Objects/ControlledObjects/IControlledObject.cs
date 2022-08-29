using System;
using System.Numerics;

namespace CoreEngine.Entities.Objects.ControlledObjects;

public interface IControlledObject
{
    event Action<Vector2> PositionChanged;
    event Action<float> RotationChanged;
    event Action<object> Destroyed;
}