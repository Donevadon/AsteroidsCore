using CoreEngine.Core;
using CoreEngine.Core.Factory;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects.Factory
{
    public class SmallAsteroidFactory : FragmentFactory
    {
        protected override IObject CreateSmallAsteroid(FragmentAsteroidModel model) =>
            new SmallAsteroid(model);
    }
}