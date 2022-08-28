using System;
using System.Numerics;
using CoreEngine.Behaviors;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects.ControlledObjects;
using CoreEngine.Entities.Objects.ControlledObjects.Player;

namespace CoreEngine.Entities.Objects
{
    public class Laser : GameObject
    {
        private readonly Vector2 _size;
        private readonly Action? _addScore;
        private readonly float _lifeTime;
        private DateTime _countdown = DateTime.Now;
        
        public Laser(AmmunitionModel model) 
            : base(new MovementByStaticAcceleration(model.MoveOptions.Position, model.MoveOptions.Angle, 0,0, model.MoveOptions.ScreenSize),
                new RotationByStaticAcceleration(model.MoveOptions.Angle, Vector3.UnitZ, 0, 0), model.Size)
        {
            _size = model.Size;
            _addScore = model.AddScore;
            _lifeTime = model.LifeTime;
        }

        public override void OnCollision(ICollisionObject sender)
        {
            _addScore?.Invoke();
        }

        public override bool IsCollision(ICollisionObject obj)
        {
            var a = Movement.Direction * _size.X + Movement.Position;
            var p = Vector2.Distance(a, Movement.Position);
            var p1 = Vector2.Distance(a, obj.Position) + Vector2.Distance(Movement.Position, obj.Position);

            return obj is not PlayerShip
                   && obj is not Bullet
                   && Math.Abs(p - p1) < 0.04f;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            Timer(ref _countdown, TimeSpan.FromSeconds(_lifeTime));
        }
        
        private void Timer(ref DateTime lastTime, TimeSpan time)
        {
            var delta = DateTime.Now - lastTime;
            if (delta > time)
            {
                Destroy();
            }
        }
    }
}