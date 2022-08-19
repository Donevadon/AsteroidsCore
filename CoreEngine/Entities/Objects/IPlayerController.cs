using System;

namespace CoreEngine.Entities.Objects
{
    public interface IPlayerController
    {
        event Action<float> Move;
        event Action<float> Rotate;
    }
}