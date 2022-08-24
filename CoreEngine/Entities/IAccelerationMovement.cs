using System;

namespace CoreEngine.Entities
{
    public interface IAccelerationMovement: IMovement
    {
        float Acceleration { get; set; }
        event Action<float> SpeedChanged;
    }
}