using System;
using System.Numerics;
using CoreEngine.Behaviors;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Factory;

namespace CoreEngine.Entities.Objects.Factory
{
    public class BulletFactory : AmmunitionFactory
    {
        public BulletFactory(Core.CoreEngine engine) : base(engine)
        {
        }

        protected override IObject CreateLaser(MoveOptions moveOptions, Vector2 size, Action? addScore) => new Laser(
            new MovementWithAcceleration(moveOptions.Position, moveOptions.Angle, 0, moveOptions.ScreenSize, 0), new DoNotRotation(),
            size, addScore);

        protected override IObject CreateAmmo(MoveOptions moveOptions, Vector2 size, Action? addScore) => new Bullet(moveOptions, size, addScore);
    }
}