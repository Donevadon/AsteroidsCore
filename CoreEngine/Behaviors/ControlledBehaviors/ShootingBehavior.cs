using System;
using CoreEngine.Core;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors.ControlledBehaviors;

internal class ShootingBehavior : IDisposable
{
    private readonly IGun _gun;
    private readonly IMovement _movement;
    private readonly IRotate _rotation;


    public ShootingBehavior(IGun gun, IMovement movement, IRotate rotation)
    {
        _gun = gun;
        _movement = movement;
        _rotation = rotation;
    }

    public void Update()
    {
        _gun.Reload();
    }

    public void LaunchLaser()
    {
        _gun.LaunchLaser(_movement.Position, _rotation.Angle);
    }

    public void Fire()
    {
        _gun.Fire(_movement.Position, _rotation.Angle);
    }
    
    public event Action<TimeSpan> LaserTimeUpdated
    {
        add => _gun.LaserTimeUpdated += value;
        remove => _gun.LaserTimeUpdated -= value;
    }

    public event Action<int> LaserReloaded
    {
        add => _gun.LaserReloaded += value;
        remove => _gun.LaserReloaded -= value;
    }

    public event Action ScoreAdded
    {
        add => _gun.ScoreAdded += value;
        remove => _gun.ScoreAdded -= value;
    }

    public void Dispose()
    {
        _gun.Dispose();
        _movement.Dispose();
        _rotation.Dispose();
    }
}