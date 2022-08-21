using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects.Factory;

namespace CoreEngine.Entities.Objects
{
    public class PlayerShip : ControlledGameObject
    {
        private readonly IMetricView _metric;
        private readonly IAmmunitionFactory _factory;
        private readonly IAccelerationMovement _acceleration;
        private readonly IAccelerationRotate _rotate;
        private readonly IGun _gun;
        private float _angle;

        public PlayerShip(IController controller, IMetricView metric, PlayerModel model)
            : base(controller, new PlayerMovement(model.MoveOptions.Position, model.MoveOptions.Angle, model.MoveOptions.Speed, model.MoveOptions.ScreenSize), 
                new PlayerRotation(model.MoveOptions.Angle, model.RotateSpeed))
        {
            _metric = metric;
            _factory = model.Factory;
            metric.UpdateAngle(model.MoveOptions.Angle);
            metric.UpdatePosition(model.MoveOptions.Position);

            _acceleration = Movement as IAccelerationMovement;
            _rotate = Rotation as IAccelerationRotate;
            Rotation.RotationChanged += rotation => _angle = rotation;
            Rotation.RotationChanged += _metric.UpdateAngle;
            Movement.PositionChanged += _metric.UpdatePosition;
        }

        public override void Update(float deltaTime)
        {
            Movement.Move(deltaTime);
            Rotation.Rotate(deltaTime);
        }

        public void Fire()
        {
            _factory.GetAmmo(null);
        }

        protected override void OnRotate(float acceleration)
        {
            _rotate.Acceleration = acceleration;
        }

        protected override void OnMove()
        {
            _acceleration.Acceleration += 0.02f;
            Movement.CalculateDirection(_angle);
        }

        public override bool IsCollision(IObject obj)
        {
            return !(obj is Bullet) && base.IsCollision(obj);
        }

        public override void OnCollision(IObject sender)
        {
            
        }
    }

    internal interface IAccelerationRotate : IRotate
    {
        float Acceleration { get; set; }
    }

    internal interface IGun
    {
    }
}