using System;
using System.Numerics;
using CoreEngine.Player;

namespace CoreEngine.Core
{
    public class Asteroid : GameObject
    {
        private readonly IMovement _movement;
        private readonly IRotate _rotation;

        public event Action<Vector2> PositionChanged
        {
            add => _movement.PositionChanged += value;
            remove => _movement.PositionChanged -= value;
        }

        public event Action<Vector3> RotationChanged
        {
            add => _rotation.RotationChanged += value;
            remove => _rotation.RotationChanged -= value;
        }

        public Asteroid(Vector2 vector2)
        {
            _movement = new PlayerMovement(vector2, Vector2.UnitY, 0.1f);
            _rotation = new PlayerRotation(Vector3.Zero, Vector3.UnitZ, 5);
        }

        public override void Update()
        {
            _movement.Move(1);
            _rotation.Rotate(1);
        }
    }
}