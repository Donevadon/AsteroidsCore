using System;
using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core
{
    public interface IAmmunitionFactory
    {
        IObject GetAmmo(AmmunitionModel model);
        IObject GetLaser(AmmunitionModel model);
        event Action<IObject> ObjectCreated;
    }
}