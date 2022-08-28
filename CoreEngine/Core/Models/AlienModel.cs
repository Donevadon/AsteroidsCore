using System.Numerics;
using CoreEngine.Core.Configurations;
using CoreEngine.Entities.Objects.ControlledObjects;

#pragma warning disable CS8618

namespace CoreEngine.Core.Models
{
    public class AlienModel
    {
        public IMotion Controller { get; set; }
        public MoveOptions MoveOptions { get; set; }
        public float RotateSpeed { get; set; }
        public Vector2 Size { get; set; }
        public float MoveRate { get; set; }
    }
}