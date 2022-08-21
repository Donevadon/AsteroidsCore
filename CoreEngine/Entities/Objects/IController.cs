using System;

namespace CoreEngine.Entities.Objects
{
    public interface IController
    {
        event Action Move;
        event Action<float> Rotate;
    }
}