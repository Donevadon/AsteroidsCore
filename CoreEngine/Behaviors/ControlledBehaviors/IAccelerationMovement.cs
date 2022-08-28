using System;

namespace CoreEngine.Behaviors.ControlledBehaviors;

public interface IAccelerationMovement
{
    float Acceleration { get; set; }
    event Action<float> SpeedChanged;
    void CalculateDirection(float rotateAngle);
}