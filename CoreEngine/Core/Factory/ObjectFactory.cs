using System;
using CoreEngine.Core.Models;

namespace CoreEngine.Core.Factory
{
    public abstract class ObjectFactory : IObjectFactory
    {
        public event Action<IObject>? ObjectCreated;

        public IPlayer GetPlayer(PlayerModel model)
        {
            var ship = CreatePlayer(model);
            ObjectCreated?.Invoke(ship);

            return ship;
        }

        protected abstract IPlayer CreatePlayer(PlayerModel model);
        

        public IObject GetAsteroid(AsteroidModel model)
        {
            var asteroid = CreateAsteroid(model);
            ObjectCreated?.Invoke(asteroid);

            return asteroid;
        }
        
        protected abstract IObject CreateAsteroid(AsteroidModel model);

        public IObject GetAlien(AlienModel model)
        {
            var alien = CreateAlien(model);
            ObjectCreated?.Invoke(alien);

            return alien;
        }

        protected abstract IObject CreateAlien(AlienModel model);
    }
}