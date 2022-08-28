using System;
using System.Numerics;
using CoreEngine.Behaviors;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core
{
    public abstract class CoreEngine
    {
        public event Action<float>? FrameUpdated;
        private DateTime _asteroidTime = DateTime.Now;
        private DateTime _alienTime = DateTime.Now;
        private readonly Options _options;
        private readonly PlayerOptions _playerOptions;
        private readonly AsteroidOptions _asteroidOptions;
        private readonly AlienOptions _alienOptions;
        private readonly Vector2 _screenSize;
        private IObject _player;
        private Mock _controller;

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
            SpawnAlien();
        }

        public void UpdateFrame(float deltaTime)
        {
            FrameUpdated?.Invoke(deltaTime);
            //Timer(ref _asteroidTime, TimeSpan.FromSeconds(3), SpawnAsteroid);
            //Timer(ref _alienTime, TimeSpan.FromSeconds(3), SpawnAlien);
            _controller?.Update();
        }

        private void CreatePlayer()
        {
            var moveOptions = new MoveOptions(new Vector2(_playerOptions.StartPositionX, _playerOptions.StartPositionY),
                _playerOptions.MoveSpeed,
                _playerOptions.StartAngle, _screenSize);
            var size = _playerOptions.Size;
            var model = new PlayerModel()
            {
                Factory = AmmunitionFactory,
                MoveOptions = moveOptions,
                RotateSpeed = _playerOptions.RotateSpeed,
                GunOptions = _playerOptions.GunOptions,
                Size = new Vector2(size.X, size.Y),
                Breaking = _playerOptions.Breaking
            };
            _player = Pool.GetPlayer(model);
        }

        private void SpawnAsteroid()
        {
            var random = new Random();
            var options = new MoveOptions(new Vector2(-_screenSize.X, _screenSize.Y),
                _options.AsteroidOptions.MoveSpeed, random.Next(0, 360), _screenSize);
            var size = _asteroidOptions.Size;
            var model = new AsteroidModel()
            {
                Factory = FragmentsFactory,
                FragmentCount = _asteroidOptions.FragmentCount,
                FragmentOptions = _asteroidOptions.FragmentAsteroidOptions,
                MoveOptions = options,
                RotateSpeed = _asteroidOptions.RotateSpeed,
                Size = new Vector2(size.X, size.Y)
            };
            _ = Pool.GetAsteroid(model);
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
            var options = new MoveOptions(/*new Vector2(_screenSize.X, -_screenSize.Y)*/Vector2.One, _alienOptions.MoveSpeed,
                random.Next(0, 360), _screenSize);
            _controller = new Mock(_player);
            var size = _alienOptions.Size;
            var model = new AlienModel()
            {
                Controller = _controller,
                MoveOptions = options,
                RotateSpeed = _alienOptions.RotateSpeed,
                Size = new Vector2(size.X, size.Y),
            };
            var alien = Pool.GetAlien(model);

            _controller._alien = alien;
        }
    }

    internal class Mock : IMotion
    {
        private readonly IObject _player;
        public IObject? _alien;

        public Mock(IObject player)
        {
            _player = player;
        }

        public event Action? Move;
        public event Action<float>? Rotate;

        public void Update()
        {
            var expectRotate = RotateForTarget(_player.Position, _alien.Position);

            if (_alien.Angle > 350 && expectRotate < 10)
            {
                Rotate?.Invoke(1);
            }else if (expectRotate > 350 && _alien.Angle < 10)
            {
                Rotate?.Invoke(-1);
            }
            else if(expectRotate < _alien.Angle)
            {
                Rotate?.Invoke(-1);
            }
            else
            {
                Rotate?.Invoke(1);
            }
            
            Move?.Invoke();
        }

        private static float RotateForTarget(Vector2 target, Vector2 position)
        {
            var vector = target - position;
            var rotationZ = (float) (Math.Atan2(vector.X, vector.Y) * (360 / (Math.PI * 2)));
            var angle = -rotationZ + 90;
            if (angle > 360) angle -= 360;
            if (angle < 0) angle += 360;
            return angle;
        }
    }
}