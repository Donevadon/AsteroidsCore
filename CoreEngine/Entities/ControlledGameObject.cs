using System.Numerics;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Entities
{
    public abstract class ControlledGameObject : GameObject
    {
        private readonly IController _controller;

        protected ControlledGameObject(IController controller, IMovement movement, IRotate rotate, Vector2 size) : base(movement, rotate, size)
        {
            _controller = controller;

            _controller.Move += OnMove;
            _controller.Rotate += OnRotate;
        }

        protected abstract void OnRotate(float obj);

        protected abstract void OnMove();

        protected override void Destroy()
        {
            _controller.Rotate -= OnRotate;
            _controller.Move -= OnMove;

            base.Destroy();
        }
    }
}