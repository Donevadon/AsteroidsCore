using CoreEngine.Entities;

namespace CoreEngine.Behaviors.ControlledBehaviors;

public interface IAccelerationRotate
{
    float Acceleration { get; set; }
    float Angle { get; }
}