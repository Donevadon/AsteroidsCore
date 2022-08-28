using System;
using System.Numerics;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Entities.Objects;
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

        public event Action ScoreAdded;


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
            _bulletGun = _bulletGun.Fire(new MoveOptions(position, _bulletOptions.Speed, angle, _screenSize), new Vector2(size.X, size.Y), ScoreAdded);
        }

        public void Reload()
        {
            _bulletGun = _bulletGun.Reload(_bulletOptions.ReloadTime);
            _laserGun = _laserGun.Reload(_laserOptions.ReloadTime);
        }

        public void LaunchLaser(Vector2 movementPosition, float rotationAngle)
        {
            var size = _laserOptions.Size;
            _laserGun = _laserGun.Fire(new MoveOptions(movementPosition, 0, rotationAngle, _screenSize),
                new Vector2(size.X, size.Y), ScoreAdded);
        }
    }
}