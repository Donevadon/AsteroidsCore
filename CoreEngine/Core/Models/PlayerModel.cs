using System.Numerics;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Core.Models
{
    public class PlayerModel
    {
        public IAmmunitionFactory Factory { get; set; }
        public MoveOptions MoveOptions { get; set; }
        public float RotateSpeed{ get; set; }
        public GunOptions GunOptions { get; set; }
        public Vector2 Size { get; set; }
    };
}