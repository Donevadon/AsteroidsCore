using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Behaviors;

namespace CoreEngine.Entities.Objects
{
    public class PlayerShip : GameObject
    {
        private readonly IPlayerController _controller;
        private readonly IAccelerationMovement _acceleration;
        private Vector3 _rotation = Vector3.Zero;
        protected sealed override IMovement Movement => _acceleration;
        protected sealed override IRotate Rotation { get; }


        public PlayerShip(IPlayerController controller, Vector2 startPosition)
        {
            _controller = controller;

            _acceleration = new PlayerMovement(startPosition, Vector2.UnitY, 0.5f);
            Rotation = new PlayerRotation(Vector3.Zero, Vector3.UnitZ, 5);
            Rotation.RotationChanged += rotation => _rotation = rotation;
            _controller.Move += _ =>
            {
                _acceleration.Acceleration = 1;
                Movement.CalculateDirection(_rotation);
            };
            _controller.Rotate += Rotation.Rotate;
        }
        
        public override Task Update()
        {
            return Task.Run(Movement.Move);
        }
    }
}