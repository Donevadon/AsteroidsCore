using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Entities.Objects
{
    public class DischargedMultiChargingGun : IGunBehavior
    {
        private readonly IAmmunitionFactory _factory;
        private readonly ReloadTimer _timer;
        private DateTime _reloadTime = DateTime.Now;
        private readonly int _sequenceNumber = 0;

        public DischargedMultiChargingGun(IAmmunitionFactory factory, ReloadTimer timer)
        {
            _factory = factory;
            _timer = timer;
        }
        public IGunBehavior Reload(float time)
        {
            IGunBehavior result = this;
            if(_timer.Timer(TimeSpan.FromSeconds(10), _sequenceNumber + 1))
            {
                result = new MultiChargingGun(_factory, this, _timer);
            }

            return result;
        }
        
        public int Count()
        {
            return _sequenceNumber;
        }

        public IGunBehavior Fire(MoveOptions options, Vector2 size, Action addScore)
        {
            return this;
        }
    }
}