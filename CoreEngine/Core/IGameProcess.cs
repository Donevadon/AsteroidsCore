using System;

namespace CoreEngine.Core;

public interface IGameProcess : IObject
{
    public event Action<int>? ScoreAdded;
}