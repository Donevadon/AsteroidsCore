using System;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects.ControlledObjects;

public class Alien : GameObject
{
    private readonly MovingBehavior _movingBehavior;
    public Alien(AlienModel model)
        : base(new MovementWithAcceleration(model.MoveOptions.Position, model.MoveOptions.Angle, model.MoveOptions.Speed, model.MoveOptions.ScreenSize, 0),
            new RotationWithAcceleration(model.MoveOptions.Angle, model.RotateSpeed), model.Size)
    {
        _movingBehavior = new MovingBehavior(model.Controller,
            Movement as IAccelerationMovement ?? throw new InvalidOperationException(),
            Rotation as IAccelerationRotate ?? throw new InvalidOperationException(), 0.002f);
    }

    public override bool IsCollision(IObject obj)
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