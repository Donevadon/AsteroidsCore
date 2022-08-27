using System;
using System.Numerics;
using CoreEngine.Behaviors;
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
        public Vector2 Size { get;}
        public float Angle => Rotation.Angle;

        public virtual bool IsCollision(IObject? obj)
        {
            var distance = Position - obj.Position;
            var sizeX = Size.X / 2 + obj.Size.X / 2;
            var sizeY = Size.Y / 2 + obj.Size.Y / 2;

            return Math.Abs(distance.X) - sizeX <= 0 && Math.Abs(distance.Y) - sizeY <= 0;
        }

        public abstract void OnCollision(IObject sender);

        event Action<IObject> IObject.Updated
        {
            add => Collision += value;
            remove => Collision -= value;
        }

        public event Action<float> RotationChanged
        {
            add => Rotation.RotationChanged += value;
            remove => Rotation.RotationChanged -= value;
        }

        protected GameObject(IMovement movement, IRotate rotate, Vector2 size)
        {
            Movement = movement;
            Rotation = rotate;
            Size = size;
        }

        public event Action<IObject> Destroyed;

        public virtual void Update(float deltaTime)
        {
            Collision?.Invoke(this);
        }

        protected virtual void Destroy()
        {
            Destroyed?.Invoke(this);
            Destroyed = null;
        }
    }
}