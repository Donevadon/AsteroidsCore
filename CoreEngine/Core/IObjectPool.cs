using System.Numerics;

namespace CoreEngine.Core
{
    public interface IObjectPool
    {
        IObject GetPlayer(Vector2 startPosition);
        IObject GetAsteroid(Vector2 vector2);
    }
}