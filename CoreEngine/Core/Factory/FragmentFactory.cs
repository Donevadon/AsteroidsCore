using CoreEngine.Core.Models;

namespace CoreEngine.Core.Factory
{
    public abstract class FragmentFactory : Factory, IFragmentsFactory
    {
        protected FragmentFactory(CoreEngine engine) : base(engine)
        {
        }
        
        public IObject? GetSmallAsteroid(FragmentAsteroidModel model)
        {
            var asteroid = CreateSmallAsteroid(model);
            InitInEngine(asteroid);
            return asteroid;
        }

        protected abstract IObject? CreateSmallAsteroid(FragmentAsteroidModel model);
    }
}