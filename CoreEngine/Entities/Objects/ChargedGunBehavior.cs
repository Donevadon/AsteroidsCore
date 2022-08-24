using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Entities.Objects
{
    public class ChargedGunBehavior : IGunBehavior
    {
        private readonly IAmmunitionFactory _factory;

        public ChargedGunBehavior(IAmmunitionFactory factory)
        {
            _factory = factory;
        }

        public IGunBehavior Reload(float time)
        {
            return this;
        }

        public int Count()
        {
            return 1;
        }

        public IGunBehavior Fire(MoveOptions options, Vector2 size, Action addScore)
        {
            _factory.GetAmmo(options, size, addScore);

            return new DischargedGunBehavior(_factory);
        }
    }
}