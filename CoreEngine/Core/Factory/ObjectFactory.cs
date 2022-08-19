using System.Numerics;

namespace CoreEngine.Core.Factory
{
    public abstract class ObjectFactory : IObjectPool
    {
        private readonly CoreEngine _engine;

        protected ObjectFactory(CoreEngine engine)
        {
            _engine = engine;
        }
        
        public IObject GetPlayer(Vector2 startPosition)
        {
            var ship = CreatePlayer(startPosition);
            _engine.FrameUpdated += ship.Update;

            return ship;
        }

        protected abstract IObject CreatePlayer(Vector2 startPosition);

        
        public IObject GetAsteroid(Vector2 vector2)
        {
            var asteroid = CreateAsteroid(vector2);
            _engine.FrameUpdated += asteroid.Update;

            return asteroid;
        }

        protected abstract IObject CreateAsteroid(Vector2 position);
    }
}