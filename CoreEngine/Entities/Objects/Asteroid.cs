using System;
using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Behaviors;
using CoreEngine.Core;

namespace CoreEngine.Entities.Objects
{
    public class Asteroid : GameObject
    {
        private readonly IFragmentsFactory _factory;
        private Task testDestroy;

        public Asteroid(Vector2 vector2, IFragmentsFactory factory)
        {
            _factory = factory;
            Movement = new Movement(vector2, Vector2.UnitY, 0.1f);
            Rotation = new PlayerRotation(Vector3.Zero, Vector3.UnitZ, 5);
        }

        protected override IMovement Movement { get; }
        protected override IRotate Rotation { get; }

        public override Task Update()
        {
            if (testDestroy == null)
            {
                testDestroy = Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    Destroy();
                });
            }
            var task = Task.Run(() =>
            {
                Movement.Move();
                Rotation.Rotate(1);
            });

            return task;
        }

        public override void Destroy()
        {
            for (var i = 0; i < 3; i++)
            {
                _factory.GetSmallAsteroid(Movement.Position);
            }
            base.Destroy();
        }
    }
}