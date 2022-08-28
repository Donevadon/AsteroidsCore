using System;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;
using CoreEngine.Guns;

namespace CoreEngine.Entities.Objects.ControlledObjects.Player;

public class PlayerShip : GameObject
{
    private int _score;
    private readonly IMetricView _metric;
    private readonly MovingBehavior _moving;
    private readonly ShootingBehavior _shootingBehavior;

    public PlayerShip(IMotion motion, IShoot shoot, IMetricView metric, PlayerModel model)
        : base(new MovementWithAcceleration(model.MoveOptions.Position, model.MoveOptions.Angle,
                model.MoveOptions.Speed, model.MoveOptions.ScreenSize, model.Breaking),
            new RotationWithAcceleration(model.MoveOptions.Angle, model.RotateSpeed), model.Size)
    {
        _metric = metric;
        _moving = new MovingBehavior(motion,
            Movement as IAccelerationMovement ?? throw new InvalidOperationException(),
            Rotation as IAccelerationRotate ?? throw new InvalidOperationException(), 0.02f);
        var gun = new Gun(model.Factory, model.GunOptions, model.MoveOptions.ScreenSize);
        _shootingBehavior = new ShootingBehavior(shoot, gun, Movement, Rotation);

        SubscribeMetric();
    }
        
    private void GunOnScoreAdded()
    {
        _metric.ScoreUpdate(++_score);
    }

    public override bool IsCollision(IObject obj)
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

        _shootingBehavior.Dispose();
        _moving.Dispose();
        UnsubscribeMetric();
    }
        
    private void SubscribeMetric()
    {
        Destroyed += _metric.OnPlayerDead;
        Rotation.RotationChanged += _metric.OnUpdateAngle;
        Movement.PositionChanged += _metric.OnUpdatePosition;
        _moving.SpeedChanged += _metric.OnUpdateSpeed;
        _shootingBehavior.LaserTimeUpdated += _metric.OnLaserRollbackTime;
        _shootingBehavior.LaserReloaded += _metric.OnUpdateLaserCount;
        _shootingBehavior.ScoreAdded += GunOnScoreAdded;
    }

    private void UnsubscribeMetric()
    {
        Destroyed -= _metric.OnPlayerDead;
        Rotation.RotationChanged -= _metric.OnUpdateAngle;
        Movement.PositionChanged -= _metric.OnUpdatePosition;
        _moving.SpeedChanged -= _metric.OnUpdateSpeed;
        _shootingBehavior.LaserTimeUpdated -= _metric.OnLaserRollbackTime;
        _shootingBehavior.LaserReloaded -= _metric.OnUpdateLaserCount;
        _shootingBehavior.ScoreAdded -= GunOnScoreAdded;
    }
}