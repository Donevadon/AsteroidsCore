using System.Numerics;

namespace CoreEngine.Core.Configurations
{
    public class MoveOptions
    {
        public MoveOptions(Vector2 position, float speed, float angle, Vector2 screenSize)
        {
            Position = position;
            Speed = speed;
            Angle = angle;
            ScreenSize = screenSize;
        }

        public Vector2 Position { get; }
        public float Speed { get; }
        public float Angle { get; }
        public Vector2 ScreenSize { get; set; }
    }
}