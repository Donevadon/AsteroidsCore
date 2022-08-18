using System.Numerics;

namespace CoreEngine.Core
{
    public interface IObjectPool
    {
        GameObject GetPlayer(Vector2 startPosition);
        GameObject GetAsteroid(Vector2 vector2);
    }
}