using System;
using CoreEngine.Entities;

namespace CoreEngine.Behaviors.ControlledBehaviors;

internal class ShootingBehavior : IDisposable
{
    private readonly IShoot _controller;
    private readonly IGun _gun;
    private readonly IMovement _movement;
    private readonly IRotate _rotation;


    public ShootingBehavior(IShoot controller, IGun gun, IMovement movement, IRotate rotation)
    {
        _controller = controller;
        _gun = gun;
        _movement = movement;
        _rotation = rotation;
            
        _controller.Fire += ControllerOnFire;
        _controller.LaunchLaser += ControllerOnLaunchLaser;
    }

    public void Update()
    {
        _gun.Reload();
    }

    private void ControllerOnLaunchLaser()
    {
        _gun.LaunchLaser(_movement.Position, _rotation.Angle);
    }

    private void ControllerOnFire()
    {
        _gun.Fire(_movement.Position, _rotation.Angle);
    }

    public void Dispose()
    {
        _controller.Fire -= ControllerOnFire;
        _controller.LaunchLaser -= ControllerOnLaunchLaser;
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
}