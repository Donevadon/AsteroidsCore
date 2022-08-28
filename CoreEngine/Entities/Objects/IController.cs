using System;

namespace CoreEngine.Entities.Objects
{
    public interface IMotion
    {
        event Action Move;
        event Action<float> Rotate;
    }
}