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

        protected override IObject CreatePlayer(Vector2 startPosition) => new PlayerShip(_controller, startPosition);

        protected override IObject CreateAsteroid(Vector2 position) => new Asteroid(position);
    }
}