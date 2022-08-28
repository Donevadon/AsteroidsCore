using System;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;
using CoreEngine.Guns;

namespace CoreEngine.Entities.Objects.ControlledObjects.Player;

public class PlayerShip : GameObject, IPursuedTarget
{
    private int _score;
    private readonly IMetricView _metric;
    private readonly IGameResultView _gameResultView;
    private readonly MovingBehavior _moving;
    private readonly ShootingBehavior _shootingBehavior;
    
    public PlayerShip(IMotion motion, IShoot shoot, IMetricView metric, IGameResultView gameResultView, PlayerModel model)
        : base(new MovementByDynamicAcceleration(model.MoveOptions.Position, model.MoveOptions.Angle,
                model.MoveOptions.Speed, model.MoveOptions.ScreenSize, model.Breaking),
            new RotationByDynamicAcceleration(model.MoveOptions.Angle, model.RotateSpeed), model.Size)
    {
        _metric = metric;
        _gameResultView = gameResultView;
        _moving = new MovingBehavior(motion,
            Movement as IAccelerationMovement ?? throw new InvalidOperationException(),
            Rotation as IAccelerationRotate ?? throw new InvalidOperationException(), model.MoveRate);
        var gun = new Gun(model.Factory, model.GunOptions, model.MoveOptions.ScreenSize);
        _shootingBehavior = new ShootingBehavior(shoot, gun, Movement, Rotation);
        
        ResetMetrics();
        SubscribeMetric();
    }

    private void ResetMetrics()
    {
        _gameResultView.ScoreUpdate(_score);
        _metric.OnUpdateAngle(Rotation.Angle);
        _metric.OnUpdatePosition(Movement.Position);
        _metric.OnUpdateLaserCount(0);
    }

    private void GunOnScoreAdded()
    {
        _gameResultView.ScoreUpdate(++_score);
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

        _shootingBehavior.Dispose();
        _moving.Dispose();
        UnsubscribeMetric();
    }
        
    private void SubscribeMetric()
    {
        Destroyed += _gameResultView.OnPlayerDead;
        Rotation.RotationChanged += _metric.OnUpdateAngle;
        Movement.PositionChanged += _metric.OnUpdatePosition;
        _moving.SpeedChanged += _metric.OnUpdateSpeed;
        _shootingBehavior.LaserTimeUpdated += _metric.OnLaserRollbackTime;
        _shootingBehavior.LaserReloaded += _metric.OnUpdateLaserCount;
        _shootingBehavior.ScoreAdded += GunOnScoreAdded;
    }

    private void UnsubscribeMetric()
    {
        Destroyed -= _gameResultView.OnPlayerDead;
        Rotation.RotationChanged -= _metric.OnUpdateAngle;
        Movement.PositionChanged -= _metric.OnUpdatePosition;
        _moving.SpeedChanged -= _metric.OnUpdateSpeed;
        _shootingBehavior.LaserTimeUpdated -= _metric.OnLaserRollbackTime;
        _shootingBehavior.LaserReloaded -= _metric.OnUpdateLaserCount;
        _shootingBehavior.ScoreAdded -= GunOnScoreAdded;
    }
}