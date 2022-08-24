using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Entities.Objects;

namespace CoreEngine.Core.Models
{
    public class AlienModel
    {
        public IController Controller { get; set; }
        public MoveOptions MoveOptions { get; set; }
        public float RotateSpeed { get; set; }
        public Vector2 Size { get; set; }
    }
}