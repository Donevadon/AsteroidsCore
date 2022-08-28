using System;
using System.Numerics;
using CoreEngine.Core;

namespace CoreEngine.Entities
{
    public abstract class GameObject : IObject, ICollisionObject
    {
        protected IMovement Movement { get; }
        protected IRotate Rotation { get; }
        
        public Vector2 Position => Movement.Position;
        public Vector2 Size { get;}

        public event Action<Vector2> PositionChanged
        {
            add => Movement.PositionChanged += value;
            remove => Movement.PositionChanged -= value;
        }
        
        public event Action<float> RotationChanged
        {
            add => Rotation.RotationChanged += value;
            remove => Rotation.RotationChanged -= value;
        }
        
        public event Action<ICollisionObject>? Updated;
        public event Action<object>? Destroyed;

        protected GameObject(IMovement movement, IRotate rotate, Vector2 size)
        {
            Movement = movement;
            Rotation = rotate;
            Size = size;
        }

        public virtual bool IsCollision(ICollisionObject obj)
        {
            var distance = Movement.Position - obj.Position;
            var sizeX = SizeCollision(Size.X, obj.Size.X);
            var sizeY = SizeCollision(Size.Y, obj.Size.Y);

            return Math.Abs(distance.X) - sizeX <= 0 && Math.Abs(distance.Y) - sizeY <= 0;
        }

        private static float SizeCollision(float current, float target) => current / 2 + target / 2;

        public virtual void OnCollision(ICollisionObject sender) => Destroy();

        public virtual void Update(float deltaTime)
        {
            Movement.Move(deltaTime);
            Rotation.Rotate(deltaTime);
            Updated?.Invoke(this);
        }

        protected virtual void Destroy()
        {
            Destroyed?.Invoke(this);
            Destroyed = null;
            Updated = null;
        }

        public virtual void Dispose()
        {
            Destroy();
        }
    }
}