using System;
using CoreEngine.Core.Models;

namespace CoreEngine.Core.Factory
{
    public abstract class FragmentFactory : IFragmentsFactory
    {
        public event Action<IObject>? ObjectCreated;

        public IObject GetSmallAsteroid(FragmentAsteroidModel model)
        {
            var asteroid = CreateSmallAsteroid(model);
            ObjectCreated?.Invoke(asteroid);
            return asteroid;
        }
        
        protected abstract IObject CreateSmallAsteroid(FragmentAsteroidModel model);
    }
}