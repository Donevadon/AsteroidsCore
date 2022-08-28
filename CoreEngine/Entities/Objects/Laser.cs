using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Entities.Objects.ControlledObjects;
using CoreEngine.Entities.Objects.ControlledObjects.Player;

namespace CoreEngine.Entities.Objects
{
    public class Laser : GameObject
    {
        private readonly Vector2 _size;
        private readonly Action? _addScore;
        private DateTime _lifeTime = DateTime.Now;
        public Laser(IMovement movement, IRotate rotate, Vector2 size, Action? addScore) : base(movement, rotate, size)
        {
            _size = size;
            _addScore = addScore;
        }

        public override void OnCollision(IObject sender)
        {
            _addScore();
        }

        public override bool IsCollision(IObject? obj)
        {
            var a = Movement.Direction * _size.X + Movement.Position;
            var p = Vector2.Distance(a, Position);
            var p1 = Vector2.Distance(a, obj.Position) + Vector2.Distance(Position, obj.Position);

            return !(obj is PlayerShip) && !(obj is Bullet) && Math.Abs(p - p1) < 0.04f;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            Timer(ref _lifeTime, TimeSpan.FromMilliseconds(20));
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