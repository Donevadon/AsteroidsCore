using System.Numerics;

namespace CoreEngine.Core
{
    public interface IObjectPool
    {
        GameObject GetPlayer();
        GameObject GetAsteroid(Vector2 vector2);
    }
}