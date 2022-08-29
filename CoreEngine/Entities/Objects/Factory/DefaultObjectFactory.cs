using CoreEngine.Core;
using CoreEngine.Core.Factory;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects.ControlledObjects;
using CoreEngine.Entities.Objects.ControlledObjects.Enemy;
using CoreEngine.Entities.Objects.ControlledObjects.Player;

namespace CoreEngine.Entities.Objects.Factory
{
    public class DefaultObjectFactory : ObjectFactory
    {
        private readonly IMotionController _motionControllerController;
        private readonly IShootController _shootControllerController;


        public DefaultObjectFactory(IMotionController motionControllerController, IShootController shootControllerController)
        {
            _motionControllerController = motionControllerController;
            _shootControllerController = shootControllerController;
        }

        protected override IPlayer CreatePlayer(PlayerModel model) => new PlayerShip(_motionControllerController, _shootControllerController, model);

        protected override IObject CreateAlien(AlienModel model) => new PursuerAlien(model);

        protected override IObject CreateAsteroid(AsteroidModel model) => new Asteroid(model);
    }
}