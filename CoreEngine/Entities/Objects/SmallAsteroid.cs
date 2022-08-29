using System.Numerics;
using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects.ControlledObjects.Enemy;

namespace CoreEngine.Entities.Objects
{
    public class SmallAsteroid : GameObject
    {
        public SmallAsteroid(FragmentAsteroidModel model) 
            : base(new MovementByStaticAcceleration(model.MoveOption.Position, model.MoveOption.Angle, model.MoveOption.Speed, 1, model.MoveOption.ScreenSize),
            new RotationByStaticAcceleration(model.MoveOption.Angle, Vector3.UnitZ, model.RotateSpeed, 1), model.Size)
        {
        }

        public override void OnCollision(ICollisionObject sender)
        {
            Destroy();
        }

        public override bool IsCollision(ICollisionObject obj)
        {
            return obj is not Asteroid 
                   && obj is not SmallAsteroid
                   && obj is not Alien
                   && base.IsCollision(obj);
        }

        public override void Update(float deltaTime)
        {
            Movement.Move(deltaTime);
            Rotation.Rotate(deltaTime);
        }
    }
}