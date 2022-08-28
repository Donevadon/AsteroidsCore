using System;
using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;

namespace CoreEngine.Guns;

public abstract class GunState
{
    protected virtual DateTime ReloadTime { get; } = DateTime.Now;

    public virtual GunState Fire(AmmunitionModel model)
    {
        var state = CreateDischargeState(model);
        Reloaded?.Invoke(state.Count);
        TimeUpdated = null;
        Reloaded = null;
        return state;
    }

    public virtual GunState Reload(float time)
    {
        return Timer(TimeSpan.FromSeconds(time));
    }

    public event Action<TimeSpan>? TimeUpdated;
    public event Action<int>? Reloaded;

    private GunState Timer(TimeSpan time)
    {
        var state = this;
        var delta = DateTime.Now - ReloadTime;
        var result = delta > time;
        if (result)
        {
            state = GetChargedState();
        }
        TimeUpdated?.Invoke(time - delta);

        return state;
    }

    private GunState GetChargedState()
    {
        var state = CreateChangedState();
        Reloaded?.Invoke(state.Count);
        state.Reloaded = Reloaded;
        state.TimeUpdated = TimeUpdated;

        return state;
    }

    protected virtual GunState CreateChangedState()
    {
        return this;
    }

    protected virtual GunState CreateDischargeState(AmmunitionModel model)
    {
        return this;
    }

    public abstract int Count { get; }
}