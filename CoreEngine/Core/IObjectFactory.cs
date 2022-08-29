using System;
using CoreEngine.Core.Models;

namespace CoreEngine.Core
{
    public interface IObjectFactory
    {
        IPlayer GetPlayer(PlayerModel model);
        IObject GetAsteroid(AsteroidModel model);
        IObject GetAlien(AlienModel model);
        event Action<IObject> ObjectCreated;
    }
}