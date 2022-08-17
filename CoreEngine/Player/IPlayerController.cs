using System;

namespace CoreEngine.Player
{
    public interface IPlayerController
    {
        event Action<float> Move;
        event Action<float> Rotate;
    }
}