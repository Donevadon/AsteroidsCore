using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Factory;

namespace CoreEngine.Entities.Objects.Factory
{
    public class DefaultObjectFactory : ObjectFactory
    {
        private readonly IPlayerController _controller;
        
        public DefaultObjectFactory(Core.CoreEngine engine, IPlayerController controller) : base(engine)
        {
            _controller = controller;
        }

        protected override IObject CreatePlayer(Vector2 startPosition, IBulletFactory factory) => new PlayerShip(_controller, startPosition, factory);

        protected override IObject CreateAsteroid(Vector2 position, IFragmentsFactory factory) => new Asteroid(position, factory);

        protected override IObject CreateSmallAsteroid(Vector2 position) =>
            new SmallAsteroid(position, Vector2.One, 1.5f, Vector3.One);

        protected override IObject CreateBullet(Vector2 position, Vector3 direction) => new Bullet(position, direction);
    }
}