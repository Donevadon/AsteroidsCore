using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects
{
    public class Alien : ControlledGameObject
    {
        private readonly IAccelerationRotate _accelerationRotate;
        public Alien(AlienModel model)
            : base(model.Controller, new Movement(model.MoveOptions.Position, model.MoveOptions.Angle, model.MoveOptions.Speed, model.MoveOptions.ScreenSize),
                new PlayerRotation(model.MoveOptions.Angle, model.RotateSpeed), model.Size)
        {
            _accelerationRotate = Rotation as IAccelerationRotate;
        }

        public override void OnCollision(IObject sender)
        {
            Destroy();
        }

        public override void Update(float deltaTime)
        {
            Movement.Move(deltaTime);
            Rotation.Rotate(deltaTime);
        }

        protected override void OnRotate(float obj)
        {
            _accelerationRotate.Acceleration += obj;
            Movement.CalculateDirection(Rotation.Angle);
        }

        protected override void OnMove()
        {
        }
    }
}