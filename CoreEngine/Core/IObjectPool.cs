using System.Numerics;
using CoreEngine.Core.Models;

namespace CoreEngine.Core
{
    public interface IObjectPool
    {
        IObject? GetPlayer(PlayerModel model);
        IObject? GetAsteroid(AsteroidModel model);
        IObject? GetAlien(AlienModel model);
    }
}