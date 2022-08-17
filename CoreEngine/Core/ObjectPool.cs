using System.Numerics;
using CoreEngine.Player;

namespace CoreEngine.Core
{
    public class ObjectPool : IObjectPool
    {
        public GameObject GetPlayer()
        {
            return new PlayerShip(null);
        }

        public GameObject GetAsteroid(Vector2 vector2)
        {
            return new Asteroid(vector2);
        }
    }
}