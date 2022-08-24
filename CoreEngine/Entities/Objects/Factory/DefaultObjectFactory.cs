using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Factory;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects.Factory
{
    public class DefaultObjectFactory : ObjectFactory
    {
        private readonly IController _controller;
        private readonly IMetricView _metric;


        public DefaultObjectFactory(Core.CoreEngine engine, IController controller, IMetricView metric) : base(engine)
        {
            _controller = controller;
            _metric = metric;
        }

        protected override IObject CreatePlayer(PlayerModel model) => new PlayerShip(_controller, _metric, model);

        protected override IObject CreateAlien(AlienModel model) =>
            new Alien(model);

        protected override IObject CreateAsteroid(AsteroidModel model) => new Asteroid(model);
    }

    public interface IMetricView
    {
        void OnUpdatePosition(Vector2 position);
        void OnUpdateAngle(float angle);
        void OnUpdateSpeed(float speed);
        void OnUpdateLaserCount(int count);
        void OnLaserRollbackTime(TimeSpan time);
        void OnPlayerDead(IObject sender);
        void ScoreUpdate(int score);
    }
}