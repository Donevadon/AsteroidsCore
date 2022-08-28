using System;

namespace CoreEngine.Behaviors.ControlledBehaviors;

public interface IShoot
{
    event Action Fire;
    event Action LaunchLaser;
}