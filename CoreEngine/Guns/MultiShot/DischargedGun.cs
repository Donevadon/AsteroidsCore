using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;

namespace CoreEngine.Guns.MultiShot;

public class DischargedGun : MultiGunState
{
    private readonly IAmmunitionFactory _factory;

    public DischargedGun(IAmmunitionFactory factory)
    {
        _factory = factory;
    }

    protected override GunState CreateChangedState()
    {
        return new ChargedGun(_factory, this);
    }

    public override GunState Fire(AmmunitionModel model)
    {
        return this;
    }

    public override int Count => 0;
}