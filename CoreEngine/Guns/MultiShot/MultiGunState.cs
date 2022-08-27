using System;

namespace CoreEngine.Guns.MultiShot;

public abstract class MultiGunState : GunState
{
    private DateTime _reloadTime = DateTime.Now;

    protected override DateTime ReloadTime => _reloadTime;

    public void SetReloadTime(DateTime time)
    {
        _reloadTime = time;
    }
}