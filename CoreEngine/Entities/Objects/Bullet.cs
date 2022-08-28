using System;
using System.Numerics;
using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects.ControlledObjects.Player;

namespace CoreEngine.Entities.Objects
{
    public sealed class Bullet : GameObject
    {
        private DateTime _countdown = DateTime.Now;
        private readonly float _lifeTime;
        private readonly Action? _addScore; 
        public Bullet(AmmunitionModel model) 
            : base(new MovementByStaticAcceleration(model.MoveOptions.Position, model.MoveOptions.Angle, model.MoveOptions.Speed, 1, model.MoveOptions.ScreenSize),
                new RotationByStaticAcceleration(model.MoveOptions.Angle, Vector3.UnitZ, 0, 0), model.Size)
        {
            _addScore = model.AddScore;
            _lifeTime = model.LifeTime;
        }

        public override void OnCollision(ICollisionObject sender)
        {
            _addScore?.Invoke();
            Destroy();
        }

        public override bool IsCollision(ICollisionObject obj)
        {
            return obj is not PlayerShip
                   && obj is not Bullet
                   && obj is not Laser
                   && base.IsCollision(obj);
        }
        
        private static void Timer(ref DateTime lastTime, TimeSpan time, Action action)
        {
            var delta = DateTime.Now - lastTime;
            if (delta <= time) return;
            action();
            lastTime = DateTime.Now;
        }

        public override void Update(float deltaTime)
        {
            Movement.Move(deltaTime);
            
            Timer(ref _countdown, TimeSpan.FromSeconds(_lifeTime), Destroy);
            
            base.Update(deltaTime);
        }
    }
}