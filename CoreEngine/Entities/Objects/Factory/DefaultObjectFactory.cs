﻿using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Factory;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects.ControlledObjects;
using CoreEngine.Entities.Objects.ControlledObjects.Player;

namespace CoreEngine.Entities.Objects.Factory
{
    public class DefaultObjectFactory : ObjectFactory
    {
        private readonly IMotion _motionController;
        private readonly IShoot _shootController;
        private readonly IMetricView _metric;


        public DefaultObjectFactory(Core.CoreEngine engine, IMotion motionController, IShoot shootController, IMetricView metric) : base(engine)
        {
            _motionController = motionController;
            _shootController = shootController;
            _metric = metric;
        }

        protected override IObject CreatePlayer(PlayerModel model) => new PlayerShip(_motionController, _shootController, _metric, model);

        protected override IObject CreateAlien(AlienModel model) => new Alien(model);

        protected override IObject CreateAsteroid(AsteroidModel model) => new Asteroid(model);
    }
}