using System;

namespace CoreEngine.Core;

public interface IMetricSource : IObject
{
    event Action<float> RotationChanged;
    event Action<float> SpeedChanged;
    event Action<TimeSpan> LaserTimeUpdated;
    event Action<int> LaserReloaded;
    float Angle { get; }
}