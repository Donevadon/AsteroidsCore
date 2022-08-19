using System.Numerics;

namespace CoreEngine.Core
{
    public interface IBulletFactory
    {
        IObject GetBullet(Vector2 position, Vector3 direction);
    }
}