using System;
using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Behaviors;
using CoreEngine.Core;

namespace CoreEngine.Entities.Objects
{
    public class PlayerShip : GameObject
    {
        private readonly IPlayerController _controller;
        private readonly IBulletFactory _factory;
        private readonly IAccelerationMovement _acceleration;
        private readonly IGun _gun;
        private Vector3 _rotation = Vector3.Zero;
        protected sealed override IMovement Movement => _acceleration;
        protected sealed override IRotate Rotation { get; }


        public PlayerShip(IPlayerController controller, Vector2 startPosition, IBulletFactory factory)
        {
            _controller = controller;
            _factory = factory;

            _acceleration = new PlayerMovement(startPosition, Vector2.UnitY, 1.5f);
            Rotation = new PlayerRotation(Vector3.Zero, Vector3.UnitZ, 10);
            Rotation.RotationChanged += rotation => _rotation = rotation;
            _controller.Move += _ =>
            {
                _acceleration.Acceleration += 0.2f;
                Movement.CalculateDirection(_rotation);
            };
            _controller.Rotate += acceleration => Rotation.Rotate(acceleration);

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    Fire();
                }

            });
        }
        
        public override Task Update()
        {
            return Task.Run(Movement.Move);
        }

        public void Fire()
        {
            _factory.GetBullet(Movement.Position, _rotation);
        }
    }

    internal interface IGun
    {
    }
}