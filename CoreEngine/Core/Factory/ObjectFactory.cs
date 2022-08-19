using System.Numerics;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core.Factory
{
    public abstract class ObjectFactory : IObjectPool, IFragmentsFactory, IBulletFactory
    {
        private readonly CoreEngine _engine;

        protected ObjectFactory(CoreEngine engine)
        {
            _engine = engine;
        }
        
        public IObject GetPlayer(Vector2 startPosition, IBulletFactory factory)
        {
            var ship = CreatePlayer(startPosition, factory);
            _engine.FrameUpdated += ship.Update;

            return ship;
        }

        protected abstract IObject CreatePlayer(Vector2 startPosition, IBulletFactory factory);
        

        public IObject GetAsteroid(Vector2 vector2, IFragmentsFactory factory)
        {
            var asteroid = CreateAsteroid(vector2, factory);
            _engine.FrameUpdated += asteroid.Update;

            return asteroid;
        }

        protected abstract IObject CreateAsteroid(Vector2 position, IFragmentsFactory factory);

        public IObject GetSmallAsteroid(Vector2 position)
        {
            var asteroid = CreateSmallAsteroid(position);
            _engine.FrameUpdated += asteroid.Update;

            return asteroid;
        }

        protected abstract IObject CreateSmallAsteroid(Vector2 position);
        public IObject GetBullet(Vector2 position, Vector3 direction)
        {
            var bullet = CreateBullet(position, direction);
            _engine.FrameUpdated += bullet.Update;

            return bullet;
        }

        protected abstract IObject CreateBullet(Vector2 position, Vector3 direction);
    }
}