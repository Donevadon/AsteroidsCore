using System;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects.ControlledObjects.Enemy;

public class Alien : GameObject, IControlledObject
{
    private readonly IObjectMotionController _controller;
    private readonly MovingBehavior _movingBehavior;

    protected Alien(AlienModel model, IObjectMotionController controller)
        : base(new MovementByDynamicAcceleration(model.MoveOptions.Position, model.MoveOptions.Angle, model.MoveOptions.Speed, model.MoveOptions.ScreenSize, 0),
            new RotationByDynamicAcceleration(model.MoveOptions.Angle, model.RotateSpeed), model.Size)
    {
        _controller = controller;
        _movingBehavior = new MovingBehavior(Movement as IAccelerationMovement ?? throw new InvalidOperationException(),
            Rotation as IAccelerationRotate ?? throw new InvalidOperationException(), model.MoveRate);

        _controller.Move += _movingBehavior.Move;
        _controller.Rotate += _movingBehavior.Rotate;
        _controller.SetPursuer(this);
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
        _controller.Move -= _movingBehavior.Move;
        _controller.Rotate -= _movingBehavior.Rotate;
    }
}