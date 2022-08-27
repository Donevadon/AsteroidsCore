using System;
using System.Numerics;
using System.Threading.Tasks;

namespace CoreEngine.Core
{
    public interface IObject
    {
        void Update(float deltaTime);
        event Action<IObject> Destroyed;
        event Action<IObject> Updated;
        Vector2 Position { get; }
        Vector2 Size { get; }
        float Angle { get; }
        bool IsCollision(IObject? obj);
        void OnCollision(IObject sender);
    }
}