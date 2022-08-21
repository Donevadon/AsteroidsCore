using System;
using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core
{
    public abstract class CoreEngine
    {
        public event Action<float> FrameUpdated;
        private DateTime _asteroidTime;
        private DateTime _alienTime;
        private readonly Options _options;
        private readonly PlayerOptions _playerOptions;
        private readonly AsteroidOptions _asteroidOptions;
        private readonly AlienOptions _alienOptions;
        private readonly Vector2 _screenSize;

        protected CoreEngine(Options options)
        {
            _options = options;
            _playerOptions = options.PlayerOptions;
            _asteroidOptions = options.AsteroidOptions;
            _alienOptions = options.AlienOptions;
            _screenSize = new Vector2(options.ScreenSize.Width, options.ScreenSize.Height);
        }

        protected abstract IObjectPool Pool { get; }
        protected abstract IFragmentsFactory FragmentsFactory { get; }
        protected abstract IAmmunitionFactory AmmunitionFactory { get; }


        public void Start()
        {
            CreatePlayer();
        }

        public void UpdateFrame(float deltaTime)
        {
            FrameUpdated?.Invoke(deltaTime);
            Timer(ref _asteroidTime, TimeSpan.FromSeconds(3), SpawnAsteroids);
            Timer(ref _alienTime, TimeSpan.FromMinutes(1), SpawnAliens);
        }

        private void CreatePlayer()
        {
            var moveOptions = new MoveOptions(new Vector2(_playerOptions.StartPositionX, _playerOptions.StartPositionY), _playerOptions.MoveSpeed,
                _playerOptions.StartAngle, _screenSize);
            var model = new PlayerModel()
            {
                Factory = AmmunitionFactory,
                MoveOptions = moveOptions,
                RotateSpeed = _playerOptions.RotateSpeed
            };
            _ = Pool.GetPlayer(model);
        }

        private void SpawnAsteroids()
        {
            var random = new Random();
            var options = new MoveOptions(new Vector2(-_screenSize.X, _screenSize.Y), _options.AsteroidOptions.MoveSpeed, random.Next(0,360), _screenSize);
            var model = new AsteroidModel()
            {
                Factory = FragmentsFactory,
                FragmentCount = _asteroidOptions.FragmentCount,
                FragmentOptions = _asteroidOptions.FragmentAsteroidOptions,
                MoveOptions = options,
                RotateSpeed = _asteroidOptions.RotateSpeed
            };
            _ = Pool.GetAsteroid(model); 
        }

        private void Timer(ref DateTime lastTime, TimeSpan time, Action action)
        {
            var delta = DateTime.Now - lastTime;
            if (delta > time)
            {
                action();
                lastTime = DateTime.Now;
            }
        }

        private void SpawnAliens()
        {
            var random = new Random();
            var options = new MoveOptions(new Vector2(_screenSize.X, -_screenSize.Y), _alienOptions.MoveSpeed, random.Next(0,360), _screenSize);
            var model = new AlienModel()
            {
                Controller = new Mock(),
                MoveOptions = options,
                RotateSpeed = _alienOptions.RotateSpeed
            };
            _ = Pool.GetAlien(model);
            
        }

    }

    internal class Mock : IController
    {
        public event Action Move;
        public event Action<float> Rotate;
    }
}