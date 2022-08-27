using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;

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

    public override GunState Fire(MoveOptions moveOptions, Vector2 vector2, Action onScoreAdded)
    {
        return this;
    }

    public override int Count => 0;
}