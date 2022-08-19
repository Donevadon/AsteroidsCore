using System;
using System.Numerics;
using System.Threading.Tasks;
using CoreEngine.Core;

namespace CoreEngine.Entities
{

    public abstract class GameObject : IObject
    {
        protected abstract IMovement Movement { get; }
        protected abstract IRotate Rotation { get; }

        public event Action<Vector2> PositionChanged
        {
            add => Movement.PositionChanged += value;
            remove => Movement.PositionChanged -= value;
        }
        
        public event Action<Vector3> RotationChanged
        {
            add => Rotation.RotationChanged += value;
            remove => Rotation.RotationChanged -= value;
        }

        public event Action Destroyed;

        public abstract Task Update();

        public virtual void Destroy()
        {
            Destroyed?.Invoke();
        }
    }
}