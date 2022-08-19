using System;
using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Behaviors;

namespace CoreEngine.Entities.Objects
{
    public sealed class Bullet : GameObject
    {
        private readonly Task _lifeTime;
        protected override IMovement Movement { get; }
        protected override IRotate Rotation { get; }

        public Bullet(Vector2 position, Vector3 direction)
        {
            Movement = new Movement(position, direction, 2);
            Rotation = new DoNotRotation();

            _lifeTime = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                Destroy();
            });
        }
        
        public override Task Update()
        {
            return Task.Run(() => Movement.Move());
        }
    }
}