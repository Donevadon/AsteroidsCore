using System;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;
using CoreEngine.Guns;

namespace CoreEngine.Entities.Objects.ControlledObjects.Player;

public class PlayerShip : GameObject, IPlayer
{
    private readonly IMotionController _motionController;
    private readonly IShootController _shootController;
    private int _score;
    private readonly MovingBehavior _moving;
    private readonly ShootingBehavior _shootingBehavior;
    
    public event Action<float>? SpeedChanged
    {
        add => _moving.SpeedChanged += value;
        remove => _moving.SpeedChanged -= value;
    }

    public event Action<TimeSpan>? LaserTimeUpdated
    {
        add => _shootingBehavior.LaserTimeUpdated += value;
        remove => _shootingBehavior.LaserTimeUpdated -= value;
    }

    public event Action<int>? LaserReloaded
    {
        add => _shootingBehavior.LaserReloaded += value;
        remove => _shootingBehavior.LaserReloaded -= value;
    }

    public float Angle => Rotation.Angle;

    public event Action<int>? ScoreAdded;

    public PlayerShip(IMotionController motionController, IShootController shootController, PlayerModel model)
        : base(new MovementByDynamicAcceleration(model.MoveOptions.Position, model.MoveOptions.Angle,
                model.MoveOptions.Speed, model.MoveOptions.ScreenSize, model.Breaking),
            new RotationByDynamicAcceleration(model.MoveOptions.Angle, model.RotateSpeed), model.Size)
    {
        _motionController = motionController;
        _shootController = shootController;
        _moving = new MovingBehavior(Movement as IAccelerationMovement ?? throw new InvalidOperationException(),
            Rotation as IAccelerationRotate ?? throw new InvalidOperationException(), model.MoveRate);
        var gun = new Gun(model.Factory, model.GunOptions, model.MoveOptions.ScreenSize);
        _shootingBehavior = new ShootingBehavior(gun, Movement, Rotation);

        Subscribes();
    }

    private void Subscribes()
    {
        _motionController.Move += _moving.Move;
        _motionController.Rotate += _moving.Rotate;
        _shootController.Fire += _shootingBehavior.Fire;
        _shootController.LaunchLaser += _shootingBehavior.LaunchLaser;
        _shootingBehavior.ScoreAdded += OnScoreAdded;
    }

    public override bool IsCollision(ICollisionObject obj)
    {
        return obj is not Bullet
               && obj is not Laser
               && base.IsCollision(obj);
    }

    public override void Update(float deltaTime)
    {
        _shootingBehavior.Update();
        
        base.Update(deltaTime);
    }

    protected override void Destroy()
    {
        base.Destroy();

        Unsubscribes();
        _shootingBehavior.Dispose();
        _moving.Dispose();
    }

    private void Unsubscribes()
    {
        _shootingBehavior.ScoreAdded -= OnScoreAdded;
        _motionController.Move -= _moving.Move;
        _motionController.Rotate -= _moving.Rotate;
        _shootController.Fire -= _shootingBehavior.Fire;
        _shootController.LaunchLaser -= _shootingBehavior.LaunchLaser;
    }

    private void OnScoreAdded()
    {
        ScoreAdded?.Invoke(++_score);
    }
}