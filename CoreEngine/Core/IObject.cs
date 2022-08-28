using System;

namespace CoreEngine.Core
{
    public interface IObject : IDisposable
    {
        void Update(float deltaTime);
        event Action<object> Destroyed;
    }
}