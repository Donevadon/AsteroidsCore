using System;
using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;

namespace CoreEngine.Core.Factory
{
    public abstract class AmmunitionFactory : IAmmunitionFactory
    {
        public event Action<IObject>? ObjectCreated;
        
        public IObject GetAmmo(AmmunitionModel model)
        {
            var bullet = CreateAmmo(model);
            ObjectCreated?.Invoke(bullet);
            
            return bullet;
        }
        
        protected abstract IObject CreateAmmo(AmmunitionModel model);

        public IObject GetLaser(AmmunitionModel model)
        {
            var laser = CreateLaser(model);
            ObjectCreated?.Invoke(laser);

            return laser;
        }

        protected abstract IObject CreateLaser(AmmunitionModel model);
    }
}