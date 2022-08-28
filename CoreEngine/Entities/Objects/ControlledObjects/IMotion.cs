using System;

namespace CoreEngine.Entities.Objects.ControlledObjects
{
    public interface IMotion
    {
        event Action Move;
        event Action<float> Rotate;
    }
}