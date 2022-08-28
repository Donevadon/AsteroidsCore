using System;
using CoreEngine.Core.Models;

namespace CoreEngine.Core
{
    public interface IAmmunitionFactory
    {
        IObject GetAmmo(AmmunitionModel model);
        IObject GetLaser(AmmunitionModel model);
        event Action<IObject> ObjectCreated;
    }
}