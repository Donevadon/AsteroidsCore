using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core.Factory
{
    public abstract class AmmunitionFactory : Factory, IAmmunitionFactory
    {
        protected AmmunitionFactory(CoreEngine engine) : base(engine)
        {
        }

        public IObject GetAmmo(MoveOptions moveOptions)
        {
            var bullet = CreateAmmo(moveOptions);
            InitInEngine(bullet);
            return bullet;
        }

        protected abstract IObject CreateAmmo(MoveOptions moveOptions);
    }
}