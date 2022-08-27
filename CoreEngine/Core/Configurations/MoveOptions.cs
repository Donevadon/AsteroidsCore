using System;
using System.Numerics;

namespace CoreEngine.Core.Configurations
{
    [Serializable]
    public record MoveOptions(Vector2 Position, float Speed, float Angle, Vector2 ScreenSize)
    {
        public Vector2 Position { get; } = Position;
        public float Speed { get; } = Speed;
        public float Angle { get; } = Angle;
        public Vector2 ScreenSize { get; set; } = ScreenSize;
    }
}