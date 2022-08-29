using System.Numerics;
using CoreEngine.Core.Configurations;

#pragma warning disable CS8618

namespace CoreEngine.Core.Models
{
    public record PlayerModel
    {
        public IAmmunitionFactory Factory { get; set; }
        public MoveOptions MoveOptions { get; set; }
        public float RotateSpeed{ get; set; }
        public GunOptions GunOptions { get; set; }
        public Vector2 Size { get; set; }
        public float Breaking { get; set; }
        public float MoveRate { get; set; }
    };
}