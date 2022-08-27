using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core
{
    public interface IFragmentsFactory
    {
        IObject? GetSmallAsteroid(FragmentAsteroidModel model);
    }
}