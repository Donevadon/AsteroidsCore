using System;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects.ControlledObjects;

public class Alien : GameObject, IPursuer
{
    private readonly MovingBehavior _movingBehavior;
    public Alien(AlienModel model)
        : base(new MovementByDynamicAcceleration(model.MoveOptions.Position, model.MoveOptions.Angle, model.MoveOptions.Speed, model.MoveOptions.ScreenSize, 0),
            new RotationByDynamicAcceleration(model.MoveOptions.Angle, model.RotateSpeed), model.Size)
    {
        _movingBehavior = new MovingBehavior(model.Controller,
            Movement as IAccelerationMovement ?? throw new InvalidOperationException(),
            Rotation as IAccelerationRotate ?? throw new InvalidOperationException(), model.MoveRate);
    }

    public override bool IsCollision(ICollisionObject obj)
    {
        return obj is not Asteroid
               && obj is not SmallAsteroid
               && obj is not Alien
               && base.IsCollision(obj);
    }

    protected override void Destroy()
    {
        base.Destroy();

        _movingBehavior.Dispose();
    }
}