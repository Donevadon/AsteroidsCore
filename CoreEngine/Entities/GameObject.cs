using System;
using System.Numerics;
using CoreEngine.Core;

namespace CoreEngine.Entities
{

    public abstract class GameObject : IObject
    {
        protected IMovement Movement { get; }

        protected IRotate Rotation { get; }

        private event Action<IObject> Collision;
        public event Action<Vector2> PositionChanged
        {
            add => Movement.PositionChanged += value;
            remove => Movement.PositionChanged -= value;
        }

        public Vector2 Position => Movement.Position;
        public virtual float Size => 0.2f;
        public virtual bool IsCollision(IObject obj)
        {
            var distance = Position - obj.Position;
            var size = Size / 2 + obj.Size / 2;

            return Math.Abs(distance.X) - size <= 0 && Math.Abs(distance.Y) - size <= 0;
        }

        public abstract void OnCollision(IObject sender);

        event Action<IObject> IObject.PositionChanged
        {
            add => Collision += value;
            remove => Collision -= value;
        }

        public event Action<float> RotationChanged
        {
            add => Rotation.RotationChanged += value;
            remove => Rotation.RotationChanged -= value;
        }

        protected GameObject(IMovement movement, IRotate rotate)
        {
            Movement = movement;
            Rotation = rotate;
            Movement.PositionChanged += MovementOnPositionChanged;
        }

        private void MovementOnPositionChanged(Vector2 obj)
        {
            Collision?.Invoke(this);
        }
        
        public event Action<IObject> Destroyed;

        public abstract void Update(float deltaTime);

        protected virtual void Destroy()
        {
            Destroyed?.Invoke(this);
            Destroyed = null;
        }
    }
}