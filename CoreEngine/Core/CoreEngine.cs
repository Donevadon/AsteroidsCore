using System;
using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;

namespace CoreEngine.Core
{
    public class CoreEngine : IDisposable
    {
        private readonly IObjectFactory _factory;
        private readonly IFragmentsFactory _fragments;
        private readonly IAmmunitionFactory _ammunition;
        private readonly Options _options;
        private readonly PlayerOptions _playerOptions;
        private readonly AsteroidOptions _asteroidOptions;
        private readonly AlienOptions _alienOptions;
        private readonly Vector2 _screenSize;
        private readonly CollisionTracker _collision = new();
        private DateTime _asteroidTime = DateTime.Now;
        private DateTime _alienTime = DateTime.Now;
        private IObject? _player;
        
        private event Action<float>? FrameUpdated;
        private event Action? Disposed;

        public CoreEngine(Options options, IAmmunitionFactory ammunition, IFragmentsFactory fragments, IObjectFactory factory)
        {
            _options = options;
            _ammunition = ammunition;
            _fragments = fragments;
            _factory = factory;
            _playerOptions = options.PlayerOptions;
            _asteroidOptions = options.AsteroidOptions;
            _alienOptions = options.AlienOptions;
            _screenSize = new Vector2(options.ScreenSize.Width, options.ScreenSize.Height);
            
            _factory.ObjectCreated += OnObjectCreated;
            _fragments.ObjectCreated += OnObjectCreated;
            _ammunition.ObjectCreated += OnObjectCreated;
        }

        private void OnObjectCreated(IObject obj)
        {
            FrameUpdated += obj.Update;
            Disposed += obj.Dispose;
            _collision.Add(obj);
            obj.Destroyed += ObjectOnDestroyed;
        }

        private void ObjectOnDestroyed(object obj)
        {
            if (obj is not IObject gameObj) return;
            FrameUpdated -= gameObj.Update;
            _collision.Remove(obj);
        }

        public void Start()
        {
            _player = CreatePlayer();
        }

        public void UpdateFrame(float deltaTime)
        {
            FrameUpdated?.Invoke(deltaTime);
            Timer(ref _asteroidTime, TimeSpan.FromSeconds(_options.AsteroidSpawnTime), SpawnAsteroid);
            Timer(ref _alienTime, TimeSpan.FromSeconds(_options.AlienSpawnTime), SpawnAlien);
        }

        private IObject CreatePlayer()
        {
            var moveOptions = new MoveOptions(new Vector2(_playerOptions.StartPositionX, _playerOptions.StartPositionY),
                _playerOptions.MoveSpeed,
                _playerOptions.StartAngle, _screenSize);
            var size = _playerOptions.Size;
            var model = new PlayerModel()
            {
                Factory = _ammunition,
                MoveOptions = moveOptions,
                RotateSpeed = _playerOptions.RotateSpeed,
                GunOptions = _playerOptions.GunOptions,
                Size = new Vector2(size.X, size.Y),
                Breaking = _playerOptions.Breaking,
                MoveRate = _playerOptions.MoveRate
            };
            return _factory.GetPlayer(model);
        }

        private void SpawnAsteroid()
        {
            var random = new Random();
            var options = new MoveOptions(new Vector2(-_screenSize.X, _screenSize.Y),
                _options.AsteroidOptions.MoveSpeed, random.Next(0, 360), _screenSize);
            var size = _asteroidOptions.Size;
            var model = new AsteroidModel()
            {
                Factory = _fragments,
                FragmentCount = _asteroidOptions.FragmentCount,
                FragmentOptions = _asteroidOptions.FragmentAsteroidOptions,
                MoveOptions = options,
                RotateSpeed = _asteroidOptions.RotateSpeed,
                Size = new Vector2(size.X, size.Y)
            };
            _ = _factory.GetAsteroid(model);
        }

        private static void Timer(ref DateTime lastTime, TimeSpan time, Action action)
        {
            var delta = DateTime.Now - lastTime;
            if (delta <= time) return;
            action();
            lastTime = DateTime.Now;
        }

        private void SpawnAlien()
        {
            var random = new Random();
            var options = new MoveOptions(new Vector2(_screenSize.X, -_screenSize.Y), _alienOptions.MoveSpeed,
                random.Next(0, 360), _screenSize);
            var controller = new PursueTarget(_player as IPursuedTarget ?? throw new ArgumentException());
            var size = _alienOptions.Size;
            var model = new AlienModel()
            {
                Controller = controller,
                MoveOptions = options,
                RotateSpeed = _alienOptions.RotateSpeed,
                Size = new Vector2(size.X, size.Y),
                MoveRate = _alienOptions.MoveRate
            };
            var alien = _factory.GetAlien(model);
            controller.SetPursuer(alien as IPursuer ?? throw new ArgumentException());
        }

        public void Dispose()
        {
            Disposed?.Invoke();
            _factory.ObjectCreated -= OnObjectCreated;
            _fragments.ObjectCreated -= OnObjectCreated;
            _ammunition.ObjectCreated -= OnObjectCreated;
            FrameUpdated = null;
            Disposed = null;
        }
    }
}