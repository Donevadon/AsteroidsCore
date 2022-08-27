using CoreEngine.Core.Models;

namespace CoreEngine.Core.Factory
{
    public abstract class ObjectFactory : Factory, IObjectPool
    {
        protected ObjectFactory(CoreEngine engine) : base(engine)
        {
        }
        
        public IObject? GetPlayer(PlayerModel model)
        {
            var ship = CreatePlayer(model);
            InitInEngine(ship);

            return ship;
        }

        protected abstract IObject? CreatePlayer(PlayerModel model);
        

        public IObject? GetAsteroid(AsteroidModel model)
        {
            var asteroid = CreateAsteroid(model);
            InitInEngine(asteroid);

            return asteroid;
        }

        public IObject? GetAlien(AlienModel model)
        {
            var alien = CreateAlien(model);
            InitInEngine(alien);

            return alien;
        }

        protected abstract IObject? CreateAlien(AlienModel model);

        protected abstract IObject? CreateAsteroid(AsteroidModel model);
    }
}