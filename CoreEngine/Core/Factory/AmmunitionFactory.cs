using System;
using System.Numerics;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Core.Factory
{
    public abstract class AmmunitionFactory : Factory, IAmmunitionFactory
    {
        protected AmmunitionFactory(CoreEngine engine) : base(engine)
        {
        }

        public IObject GetAmmo(MoveOptions moveOptions, Vector2 size, Action? addScore)
        {
            var bullet = CreateAmmo(moveOptions, size, addScore);
            InitInEngine(bullet);
            return bullet;
        }
        
        protected abstract IObject CreateAmmo(MoveOptions moveOptions, Vector2 size, Action? addScore);

        public IObject GetLaser(MoveOptions options, Vector2 size, Action? addScore)
        {
            var laser = CreateLaser(options, size, addScore);
            InitInEngine(laser);
            return laser;
        }
        
        protected abstract IObject CreateLaser(MoveOptions moveOptions, Vector2 size, Action? addScore);
    }
}