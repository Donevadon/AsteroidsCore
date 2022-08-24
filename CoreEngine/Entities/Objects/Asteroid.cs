using System;
using System.Numerics;
using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects
{
    public class Asteroid : GameObject
    {
        private readonly IFragmentsFactory _factory;
        private readonly int _fragmentCount;
        private readonly FragmentAsteroidOptions _options;
        private readonly Vector2 _screenSize;
        private readonly float _speed;

        public Asteroid(AsteroidModel model)
            : base(new Movement(model.MoveOptions.Position, model.MoveOptions.Angle, model.MoveOptions.Speed, model.MoveOptions.ScreenSize),
                new Rotation(model.MoveOptions.Angle, Vector3.UnitZ, model.RotateSpeed), model.Size)
        {
            _factory = model.Factory;
            _fragmentCount = model.FragmentCount;
            _options = model.FragmentOptions;
            _screenSize = model.MoveOptions.ScreenSize;
            _speed = model.MoveOptions.Speed;
        }
        
        public override void OnCollision(IObject sender)
        {
            Destroy();
        }

        public override bool IsCollision(IObject obj)
        {
            return !(obj is SmallAsteroid) 
                   && !(obj is Alien) 
                   && !(obj is Asteroid) 
                   && base.IsCollision(obj);
        }

        public override void Update(float deltaTime)
        {
            Movement.Move(deltaTime);
            Rotation.Rotate(deltaTime);
            base.Update(deltaTime);
        }

        protected override void Destroy()
        {           
            base.Destroy();

            for (var i = 0; i < _fragmentCount; i++)
            {
                var random = new Random();
                var option = new MoveOptions(Movement.Position, _speed + _options.Acceleration, random.Next(0, 360),
                    _screenSize);
                var model = new FragmentAsteroidModel()
                {
                    MoveOption = option,
                    RotateSpeed =  (float) random.NextDouble() * _options.MaxRotateSpeed,
                    Size = new Vector2(_options.SizeX, _options.SizeY)
                };
                
                _factory.GetSmallAsteroid(model);
            }
        }
    }
}