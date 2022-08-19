using System.Numerics;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core
{
    public interface IObjectPool
    {
        IObject GetPlayer(Vector2 startPosition, IBulletFactory factory);
        IObject GetAsteroid(Vector2 vector2, IFragmentsFactory factory);
    }
}