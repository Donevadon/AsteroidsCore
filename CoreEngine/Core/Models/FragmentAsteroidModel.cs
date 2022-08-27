using System.Numerics;
using CoreEngine.Core.Configurations;
#pragma warning disable CS8618

namespace CoreEngine.Core.Models
{
    public class FragmentAsteroidModel
    {
        public MoveOptions MoveOption { get; set; }
        public float RotateSpeed { get; set; }
        public Vector2 Size { get; set; }
    }
}