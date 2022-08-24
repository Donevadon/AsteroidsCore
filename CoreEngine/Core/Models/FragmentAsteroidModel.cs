using System.Numerics;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Core.Models
{
    public class FragmentAsteroidModel
    {
        public MoveOptions MoveOption { get; set; }
        public float RotateSpeed { get; set; }
        public Vector2 Size { get; set; }
    }
}