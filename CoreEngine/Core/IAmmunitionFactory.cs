using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core
{
    public interface IAmmunitionFactory
    {
        IObject GetAmmo(MoveOptions moveOptions);
    }
}