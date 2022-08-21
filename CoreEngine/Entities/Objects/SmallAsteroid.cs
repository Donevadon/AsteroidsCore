using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects
{
    public class SmallAsteroid : GameObject
    {
        public SmallAsteroid(FragmentAsteroidModel model) : base(new Movement(model.MoveOption.Position, model.MoveOption.Angle, model.MoveOption.Speed, model.MoveOption.ScreenSize),
            new Rotation(model.MoveOption.Angle, Vector3.UnitZ, model.RotateSpeed))
        {
        }

        public override void OnCollision(IObject sender)
        {
            Destroy();
        }

        public override void Update(float deltaTime)
        {
            Movement.Move(deltaTime);
            Rotation.Rotate(deltaTime);
        }
    }
}