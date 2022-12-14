using System;
using System.Numerics;

namespace CoreEngine.Behaviors.ControlledBehaviors;

internal interface IGun : IDisposable
{
    void Fire(Vector2 position, float angle);
    void Reload();
    void LaunchLaser(Vector2 movementPosition, float rotationAngle);
    event Action<TimeSpan> LaserTimeUpdated;
    event Action<int> LaserReloaded;
    event Action ScoreAdded;
}