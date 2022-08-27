using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Factory;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects.Factory
{
    public class SmallAsteroidFactory : FragmentFactory
    {
        public SmallAsteroidFactory(Core.CoreEngine engine) : base(engine)
        {
        }
        
        protected override IObject? CreateSmallAsteroid(FragmentAsteroidModel model) =>
            new SmallAsteroid(model);
    }
}