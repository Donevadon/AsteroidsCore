using System.Numerics;
using CoreEngine.Player;

namespace CoreEngine.Core
{
    public class ObjectPool : IObjectPool
    {
        private readonly CoreEngine _engine;
        private readonly IPlayerController _controller;

        public ObjectPool(CoreEngine engine, IPlayerController controller)
        {
            _engine = engine;
            _controller = controller;
        }
        public GameObject GetPlayer(Vector2 startPosition)
        {
            var ship = new PlayerShip(_controller, startPosition);
            _engine.FrameUpdated += ship.Update;

            return ship;
        }

        public GameObject GetAsteroid(Vector2 vector2)
        {
            var asteroid = new Asteroid(vector2);
            _engine.FrameUpdated += asteroid.Update;

            return asteroid;
        }
    }
}