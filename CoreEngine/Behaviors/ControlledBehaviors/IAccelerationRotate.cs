using System;

namespace CoreEngine.Behaviors.ControlledBehaviors;

public interface IAccelerationRotate : IDisposable
{
    float Acceleration { get; set; }
    float Angle { get; }
}