using System.Numerics;

namespace CoreEngine.Core
{
    public interface IFragmentsFactory
    {
        IObject GetSmallAsteroid(Vector2 position);
    }
}