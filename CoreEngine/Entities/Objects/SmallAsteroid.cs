using System;
using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Behaviors;

namespace CoreEngine.Entities.Objects
{
    public class SmallAsteroid : GameObject
    {
        protected override IMovement Movement { get; }
        protected override IRotate Rotation { get; }

        public SmallAsteroid(Vector2 position, Vector2 direction, float speed, Vector3 rotation)
        {
            Movement = new Movement(position, direction, speed);
            Rotation = new PlayerRotation(rotation, Vector3.UnitZ, speed);
        }
        
        public override Task Update()
        {
            return Task.Run(() =>
            {
                Movement.Move();
                Rotation.Rotate(1);
            });
        }
    }
}