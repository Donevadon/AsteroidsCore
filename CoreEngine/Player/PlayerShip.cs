using System;
using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Core;

namespace CoreEngine.Player
{
    public class PlayerShip : GameObject
    {
        private readonly IPlayerController _controller;
        private readonly IAccelerationMovement _acceleration;
        protected sealed override IMovement Movement => _acceleration;
        protected sealed override IRotate Rotation { get; }


        public PlayerShip(IPlayerController controller, Vector2 startPosition)
        {
            _controller = controller;

            _acceleration = new PlayerMovement(startPosition, Vector2.UnitY, 0.5f);
            Rotation = new PlayerRotation(Vector3.Zero, Vector3.UnitZ, 5);
            Rotation.RotationChanged += Movement.CalculateDirection;
            _controller.Move += _ => _acceleration.Acceleration = 1;
            _controller.Rotate += Rotation.Rotate;
        }
        
        public override Task Update()
        {
            return Task.Run(Movement.Move);
        }
    }
}