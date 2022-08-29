using System;
using System.Numerics;
using CoreEngine.Entities.Objects.ControlledObjects;

namespace CoreEngine.Core;

internal class PursueTarget : IMotion, IDisposable
{
    private IPursuer? _alien;
    private readonly IPursuedTarget _target;
    private Vector2? _playerPosition;
    private Vector2 _alienPosition;
    private float _alienAngle;
    public event Action? Move;
    public event Action<float>? Rotate;

    public PursueTarget(IPursuedTarget target)
    {
        _target = target;
        _playerPosition = target.Position;
        target.PositionChanged += PlayerPositionUpdate;
        target.Destroyed += OnPlayerDestroyed;
    }

    private void OnPlayerDestroyed(object sender)
    {
        _playerPosition = null;
        _target.PositionChanged -= PlayerPositionUpdate;
        _target.Destroyed -= OnPlayerDestroyed;
    }

    private void PlayerPositionUpdate(Vector2 position)
    {
        _playerPosition = position;
    }

    private void AlienPositionUpdate(Vector2 position)
    {
        _alienPosition = position;
        Update();
    }

    private void AlienAngleUpdate(float angle)
    {
        _alienAngle = angle;
    }

    private void Update()
    {
        if (_playerPosition != null)
        {
            var expectRotate = RotateForTarget(_playerPosition.Value, _alienPosition);

            if (_alienAngle > 350 && expectRotate < 10)
            {
                Rotate?.Invoke(1);
            }
            else if (expectRotate > 350 && _alienAngle < 10)
            {
                Rotate?.Invoke(-1);
            }
            else if (expectRotate < _alienAngle)
            {
                Rotate?.Invoke(-1);
            }
            else
            {
                Rotate?.Invoke(1);
            }
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

    public void SetPursuer(IPursuer alien)
    {
        _alien = alien;
        alien.PositionChanged += AlienPositionUpdate;
        alien.RotationChanged += AlienAngleUpdate;
        alien.Destroyed += OnAlienDestroyed;
        Move?.Invoke();
    }

    private void OnAlienDestroyed(object sender)
    {
        Dispose();
    }

    public void Dispose()
    {
        _target.PositionChanged -= PlayerPositionUpdate;
        _target.Destroyed -= OnPlayerDestroyed;
        _alien!.PositionChanged -= AlienPositionUpdate;
        _alien.RotationChanged -= AlienAngleUpdate;
        _alien.Destroyed -= OnAlienDestroyed;
        Move = null;
        Rotate = null;
    }
}