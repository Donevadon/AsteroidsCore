using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Entities.Objects
{
    public class Gun : IGun
    {
        private readonly Vector2 _screenSize;
        private IGunBehavior _bulletGun;
        private IGunBehavior _laserGun;
        private readonly BulletOptions _bulletOptions;
        private readonly LaserOptions _laserOptions;
        private readonly ReloadTimer _timer;
        
        public event Action<TimeSpan> LaserTimeUpdated
        {
            add => _timer.TimeUpdated += value;
            remove => _timer.TimeUpdated += value;
        }

        public event Action ScoreAdded;

        public event Action<int> LaserReloaded;

        public Gun(IAmmunitionFactory modelFactory, GunOptions options, Vector2 screenSize)
        {
            _screenSize = screenSize;
            _bulletOptions = options.BulletOptions;
            _laserOptions = options.LaserOptions;
            _timer = new ReloadTimer();
            _bulletGun = new ChargedGunBehavior(modelFactory);
            _laserGun = new DischargedMultiChargingGun(modelFactory, _timer);
            _timer.Reloaded += count => LaserReloaded?.Invoke(count);
        }

        private void OnScoreAdded()
        {
            ScoreAdded?.Invoke();
        }

        public void Fire(Vector2 position, float angle)
        {
            _bulletGun = _bulletGun.Fire(new MoveOptions(position, _bulletOptions.Speed, angle, _screenSize), new Vector2(_bulletOptions.SizeX, _bulletOptions.SizeY), ScoreAdded);
        }

        public void Reload()
        {
            _bulletGun = _bulletGun.Reload(_bulletOptions.ReloadTime);
            _laserGun = _laserGun.Reload(_laserOptions.ReloadTime);
        }

        public void LaunchLaser(Vector2 movementPosition, float rotationAngle)
        {
            _laserGun = _laserGun.Fire(new MoveOptions(movementPosition, 0, rotationAngle, _screenSize),
                new Vector2(_laserOptions.SizeX, _laserOptions.SizeY), ScoreAdded);
        }
    }
}