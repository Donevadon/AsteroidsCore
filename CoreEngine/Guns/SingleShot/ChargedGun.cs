using System;
using System.Numerics;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Guns.SingleShot;

public class ChargedGun : GunState
{
    private readonly IAmmunitionFactory _factory;
    private DischargedGun? _dischargedState;

    private DischargedGun DischargedState => _dischargedState ??= new DischargedGun(_factory);

    public ChargedGun(IAmmunitionFactory factory)
    {
        _factory = factory;
    }
    
    public override GunState Reload(float time)
    {
        return this;
    }

    protected override GunState CreateDischargeState(MoveOptions moveOptions, Vector2 vector2, Action onScoreAdded)
    {
        _factory.GetAmmo(moveOptions, vector2, onScoreAdded);
        DischargedState.ResetTime();
        return DischargedState;
    }

    public override int Count => 1;
}