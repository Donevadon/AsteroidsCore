using System;

namespace CoreEngine.Entities.Objects.ControlledObjects;

public interface IShootController
{
    event Action Fire;
    event Action LaunchLaser;
}