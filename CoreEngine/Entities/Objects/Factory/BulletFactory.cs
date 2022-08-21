using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Factory;

namespace CoreEngine.Entities.Objects.Factory
{
    public class BulletFactory : AmmunitionFactory
    {
        public BulletFactory(Core.CoreEngine engine) : base(engine)
        {
        }
        
        protected override IObject CreateAmmo(MoveOptions moveOptions) => new Bullet(moveOptions);
    }
}