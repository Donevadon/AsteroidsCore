using System.Numerics;
using CoreEngine.Core.Configurations;

namespace CoreEngine.Core.Models
{
    public class AsteroidModel
    {
       public IFragmentsFactory Factory { get; set; }
       public MoveOptions MoveOptions { get; set; }
       public float RotateSpeed { get; set; }
       public int FragmentCount { get; set; }
       public FragmentAsteroidOptions FragmentOptions { get; set; }
       public Vector2 Size { get; set; }
    }
}