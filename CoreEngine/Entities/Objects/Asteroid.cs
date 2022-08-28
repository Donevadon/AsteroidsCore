using System;
using System.Numerics;
using CoreEngine.Behaviors;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects.ControlledObjects;

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
            : base(new MovementByStaticAcceleration(model.MoveOptions.Position, model.MoveOptions.Angle, model.MoveOptions.Speed, 1, model.MoveOptions.ScreenSize),
                new RotationByStaticAcceleration(model.MoveOptions.Angle, Vector3.UnitZ, model.RotateSpeed, 1), model.Size)
        {
            _factory = model.Factory;
            _fragmentCount = model.FragmentCount;
            _options = model.FragmentOptions;
            _screenSize = model.MoveOptions.ScreenSize;
            _speed = model.MoveOptions.Speed;
        }
        
        public override bool IsCollision(ICollisionObject obj)
        {
            return obj is not SmallAsteroid 
                   && obj is not Alien 
                   && obj is not Asteroid 
                   && base.IsCollision(obj);
        }

        protected override void Destroy()
        {           
            base.Destroy();

            for (var i = 0; i < _fragmentCount; i++)
            {
                var random = new Random();
                var option = new MoveOptions(Movement.Position, _speed + _options.Acceleration, random.Next(0, 360),
                    _screenSize);
                var size = _options.Size;
                var model = new FragmentAsteroidModel()
                {
                    MoveOption = option,
                    RotateSpeed =  (float) random.NextDouble() * _options.MaxRotateSpeed,
                    Size = new Vector2(size.X, size.Y)
                };
                
                _factory.GetSmallAsteroid(model);
            }
        }

        public override void Dispose()
        {
            base.Destroy();
        }
    }
}