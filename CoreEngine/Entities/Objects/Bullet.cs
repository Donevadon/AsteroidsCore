using System;
using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Entities.Objects
{
    public sealed class Bullet : GameObject
    {
        private readonly Task _lifeTime;

        public Bullet(MoveOptions moveOptions) 
            : base(new Movement(moveOptions.Position, moveOptions.Angle, moveOptions.Speed, moveOptions.ScreenSize), new DoNotRotation())
        {
            _lifeTime = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                Destroy();
            });
        }

        public override void OnCollision(IObject sender)
        {
            
        }

        public override void Update(float deltaTime)
        {
            Movement.Move(deltaTime);
        }
    }
}