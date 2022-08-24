using System;
using System.Numerics;

namespace CoreEngine.Entities.Objects
{
    internal interface IGun
    {
        void Fire(Vector2 position, float angle);
        void Reload();
        void LaunchLaser(Vector2 movementPosition, float rotationAngle);
        event Action<TimeSpan> LaserTimeUpdated;
        event Action<int> LaserReloaded;
        event Action ScoreAdded;
    }
}