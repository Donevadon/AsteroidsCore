using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core.Factory
{
    public abstract class FragmentFactory : Factory, IFragmentsFactory
    {
        protected FragmentFactory(CoreEngine engine) : base(engine)
        {
        }
        
        public IObject GetSmallAsteroid(FragmentAsteroidModel model)
        {
            var asteroid = CreateSmallAsteroid(model);
            InitInEngine(asteroid);
            return asteroid;
        }

        protected abstract IObject CreateSmallAsteroid(FragmentAsteroidModel model);
    }
}