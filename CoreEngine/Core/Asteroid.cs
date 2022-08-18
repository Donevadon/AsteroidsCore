using System;
using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Player;

namespace CoreEngine.Core
{
    public class Asteroid : GameObject
    {
        public Asteroid(Vector2 vector2)
        {
            Movement = new Movement(vector2, Vector2.UnitY, 0.1f);
            Rotation = new PlayerRotation(Vector3.Zero, Vector3.UnitZ, 5);
        }

        protected override IMovement Movement { get; }
        protected override IRotate Rotation { get; }

        public override Task Update()
        {
            var task = Task.Run(() =>
            {
                Movement.Move();
                Rotation.Rotate(1);
            });

            return task;
        }
    }
}