using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Entities.Objects
{
    public class DischargedGunBehavior : IGunBehavior
    {
        private readonly IAmmunitionFactory _factory;
        private DateTime _reloadTime = DateTime.Now;

        public DischargedGunBehavior(IAmmunitionFactory factory)
        {
            _factory = factory;
        }

        public IGunBehavior Reload(float time)
        {
            return Timer(ref _reloadTime, TimeSpan.FromSeconds(time));
        }

        public int Count()
        {
            return 0;
        }

        private IGunBehavior Timer(ref DateTime lastTime, TimeSpan time)
        {
            IGunBehavior result = this;
            var delta = DateTime.Now - lastTime;
            if (delta > time)
            {
                lastTime = DateTime.Now;
                result = new ChargedGunBehavior(_factory);
            }

            return result;
        }

        public IGunBehavior Fire(MoveOptions options, Vector2 size, Action addScore)
        {
            return this;
        }
    }
}