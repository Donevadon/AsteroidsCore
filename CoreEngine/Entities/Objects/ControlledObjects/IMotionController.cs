using System;

namespace CoreEngine.Entities.Objects.ControlledObjects
{
    public interface IMotionController
    {
        event Action Move;
        event Action<float> Rotate;
    }
}