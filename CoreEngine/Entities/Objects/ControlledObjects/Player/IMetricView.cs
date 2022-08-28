using System;
using System.Numerics;
using CoreEngine.Core;

namespace CoreEngine.Entities.Objects.ControlledObjects.Player;

public interface IMetricView
{
    void OnUpdatePosition(Vector2 position);
    void OnUpdateAngle(float angle);
    void OnUpdateSpeed(float speed);
    void OnUpdateLaserCount(int count);
    void OnLaserRollbackTime(TimeSpan time);
    void OnPlayerDead(object sender);
    void ScoreUpdate(int score);
}