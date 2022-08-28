using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;

namespace CoreEngine.Guns.MultiShot;

public class ChargedGun : MultiGunState
{
    private readonly IAmmunitionFactory _factory;
    private readonly MultiGunState _previous;

    public ChargedGun(IAmmunitionFactory factory, MultiGunState previous)
    {
        _factory = factory;
        _previous = previous;
    }

    protected override GunState CreateChangedState()
    {
        return new ChargedGun(_factory, this);
    }

    protected override GunState CreateDischargeState(AmmunitionModel model)
    {
        _factory.GetLaser(model);
        _previous.SetReloadTime(ReloadTime);
        return _previous;
    }

    public override int Count => _previous.Count + 1;
}