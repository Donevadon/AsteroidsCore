using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Entities.Objects
{
    public class MultiChargingGun : IGunBehavior
    {
        private readonly IAmmunitionFactory _factory;
        private readonly IGunBehavior _next;
        private readonly ReloadTimer _timer;
        private DateTime _reloadTime = DateTime.Now;
        private readonly int _sequenceNumber;


        public MultiChargingGun(IAmmunitionFactory factory, IGunBehavior next, ReloadTimer timer)
        {
            _factory = factory;
            _next = next;
            _timer = timer;
            _sequenceNumber = next.Count() + 1;
        }
        
        public IGunBehavior Reload(float time)
        {
            IGunBehavior result = this;
            if (_timer.Timer(TimeSpan.FromSeconds(10), _sequenceNumber + 1))
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
            _factory.GetLaser(options, size, addScore);
            _timer.Reset(_sequenceNumber - 1);
            return _next;
        }
    }
}