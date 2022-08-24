using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects.Factory;

namespace CoreEngine.Entities.Objects
{
    public class PlayerShip : ControlledGameObject
    {
        private int _score;
        private readonly IController _controller;
        private readonly IMetricView _metric;
        private readonly IAccelerationMovement _acceleration;
        private readonly IAccelerationRotate _rotate;
        private readonly IGun _gun;

        public PlayerShip(IController controller, IMetricView metric, PlayerModel model)
            : base(controller, new PlayerMovement(model.MoveOptions.Position, model.MoveOptions.Angle, model.MoveOptions.Speed, model.MoveOptions.ScreenSize), 
                new PlayerRotation(model.MoveOptions.Angle, model.RotateSpeed), model.Size)
        {
            _controller = controller;
            _metric = metric;
            _acceleration = Movement as IAccelerationMovement;
            _rotate = Rotation as IAccelerationRotate;
            _gun = new Gun(model.Factory, model.GunOptions, model.MoveOptions.ScreenSize);

            SubscribeMetric();
            SubscribeController();
        }

        private void SubscribeMetric()
        {
            Destroyed += _metric.OnPlayerDead;
            Rotation.RotationChanged += _metric.OnUpdateAngle;
            Movement.PositionChanged += _metric.OnUpdatePosition;
            _acceleration.SpeedChanged += _metric.OnUpdateSpeed;
            _gun.LaserTimeUpdated += _metric.OnLaserRollbackTime;
            _gun.LaserReloaded += _metric.OnUpdateLaserCount;
            _gun.ScoreAdded += GunOnScoreAdded;
        }

        private void SubscribeController()
        {
            _controller.Fire += ControllerOnFire;
            _controller.LaunchLaser += ControllerOnLaunchLaser;
        }
        
        private void UnsubscribeMetric()
        {
            Destroyed -= _metric.OnPlayerDead;
            Rotation.RotationChanged -= _metric.OnUpdateAngle;
            Movement.PositionChanged -= _metric.OnUpdatePosition;
            _acceleration.SpeedChanged -= _metric.OnUpdateSpeed;
            _gun.LaserTimeUpdated -= _metric.OnLaserRollbackTime;
            _gun.LaserReloaded -= _metric.OnUpdateLaserCount;
            _gun.ScoreAdded -= GunOnScoreAdded;
        }

        private void UnsubscribeController()
        {
            _controller.Fire -= ControllerOnFire;
            _controller.LaunchLaser -= ControllerOnLaunchLaser;
        }
        
        private void GunOnScoreAdded()
        {
            _metric.ScoreUpdate(++_score);
        }

        private void ControllerOnLaunchLaser()
        {
            _gun.LaunchLaser(Movement.Position, Rotation.Angle);
        }

        private void ControllerOnFire()
        {
            _gun.Fire(Movement.Position, Rotation.Angle);
        }

        public override void Update(float deltaTime)
        {
            Movement.Move(deltaTime);
            Rotation.Rotate(deltaTime);
            _gun.Reload();
            
            base.Update(deltaTime);
        }

        protected override void OnRotate(float acceleration)
        {
            _rotate.Acceleration = acceleration;
        }

        protected override void OnMove()
        {
            _acceleration.Acceleration += 0.02f;
            Movement.CalculateDirection(Rotation.Angle);
        }

        public override bool IsCollision(IObject obj)
        {
            return !(obj is Bullet) && !(obj is Laser) && base.IsCollision(obj);
        }

        public override void OnCollision(IObject sender)
        {
            Destroy();
        }

        protected override void Destroy()
        {
            base.Destroy();
            
            UnsubscribeController();
            UnsubscribeMetric();
        }
    }
}