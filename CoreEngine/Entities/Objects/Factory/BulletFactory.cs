using CoreEngine.Core;
using CoreEngine.Core.Factory;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects.Factory
{
    public class BulletFactory : AmmunitionFactory
    {
        protected override IObject CreateLaser(AmmunitionModel model) => new Laser(model);

        protected override IObject CreateAmmo(AmmunitionModel model) => new Bullet(model);
    }
}