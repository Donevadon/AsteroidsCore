using System;
using System.Numerics;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Guns.MultiShot;
using ChargedGun = CoreEngine.Guns.SingleShot.ChargedGun;

namespace CoreEngine.Guns
{
    public class Gun : IGun
    {
        private readonly Vector2 _screenSize;
        private GunState _bulletGun;
        private GunState _laserGun;
        private readonly BulletOptions _bulletOptions;
        private readonly LaserOptions _laserOptions;
        
        public event Action<TimeSpan>? LaserTimeUpdated
        {
            add => _laserGun.TimeUpdated += value;
            remove => _laserGun.TimeUpdated -= value;
        }

        public event Action<int>? LaserReloaded
        {
            add => _laserGun.Reloaded += value;
            remove => _laserGun.Reloaded -= value;
        }

        public event Action? ScoreAdded;

        public Gun(IAmmunitionFactory modelFactory, GunOptions options, Vector2 screenSize)
        {
            _screenSize = screenSize;
            _bulletOptions = options.BulletOptions;
            _laserOptions = options.LaserOptions;
            _bulletGun = new ChargedGun(modelFactory);
            _laserGun = new DischargedGun(modelFactory);
        }

        public void Fire(Vector2 position, float angle)
        {
            var size = _bulletOptions.Size;
            var model = new AmmunitionModel()
            {
                Size = new Vector2(size.X, size.Y),
                AddScore = ScoreAdded,
                MoveOptions = new MoveOptions(position, _bulletOptions.Speed, angle, _screenSize),
                LifeTime = _bulletOptions.LifeTime
            };
            _bulletGun = _bulletGun.Fire(model);
        }

        public void Reload()
        {
            _bulletGun = _bulletGun.Reload(_bulletOptions.ReloadTime);
            _laserGun = _laserGun.Reload(_laserOptions.ReloadTime);
        }

        public void LaunchLaser(Vector2 movementPosition, float rotationAngle)
        {
            var size = _laserOptions.Size;
            var model = new AmmunitionModel()
            {
                Size = new Vector2(size.X, size.Y),
                AddScore = ScoreAdded,
                MoveOptions = new MoveOptions(movementPosition, 0, rotationAngle, _screenSize),
                LifeTime = _laserOptions.LifeTime
            };
            _laserGun = _laserGun.Fire(model);
        }

        public void Dispose()
        {
            ScoreAdded = null;
            _laserGun.Dispose();
            _bulletGun.Dispose();
        }
    }
}