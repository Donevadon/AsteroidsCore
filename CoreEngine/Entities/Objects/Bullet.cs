using System;
using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Entities.Objects.ControlledObjects;
using CoreEngine.Entities.Objects.ControlledObjects.Player;

namespace CoreEngine.Entities.Objects
{
    public sealed class Bullet : GameObject
    {
        private DateTime _lifeTime = DateTime.Now;
        private readonly Action? _addScore; 
        public Bullet(MoveOptions moveOptions, Vector2 size, Action? addScore) 
            : base(new Movement(moveOptions.Position, moveOptions.Angle, moveOptions.Speed, moveOptions.ScreenSize), new DoNotRotation(), size)
        {
            _addScore = addScore;
        }

        public override void OnCollision(IObject sender)
        {
            _addScore?.Invoke();
            Destroy();
        }

        public override bool IsCollision(IObject obj)
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
            
            Timer(ref _lifeTime, TimeSpan.FromSeconds(3), Destroy);
            
            base.Update(deltaTime);
        }
    }
}