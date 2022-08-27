using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Guns.SingleShot;

public class DischargedGun : GunState
{
    private readonly IAmmunitionFactory _factory;
    private ChargedGun? _chargedState;
    private DateTime _reloadTime = DateTime.Now;

    private ChargedGun ChargedState => _chargedState ??= new ChargedGun(_factory);

    public DischargedGun(IAmmunitionFactory factory)
    {
        _factory = factory;
    }
    
    protected override GunState CreateChangedState()
    {
        return ChargedState;
    }

    protected override DateTime ReloadTime => _reloadTime;

    public override GunState Fire(MoveOptions moveOptions, Vector2 vector2, Action onScoreAdded)
    {
        return this;
    }

    public override int Count => 0;

    public void ResetTime()
    {
        _reloadTime = DateTime.Now;
    }
}