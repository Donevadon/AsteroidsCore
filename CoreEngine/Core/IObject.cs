using System;
using System.Threading.Tasks;

namespace CoreEngine.Core
{
    public interface IObject
    {
        Task Update();
        event Action Destroyed;
    }
}