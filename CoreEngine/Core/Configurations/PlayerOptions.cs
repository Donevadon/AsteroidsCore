using System;
using System.Numerics;

namespace CoreEngine.Core.Configurations
{
    [Serializable]
    public class PlayerOptions
    {
        public float StartPositionX;
        public float StartPositionY;
        public float MoveSpeed;
        public float StartAngle;
        public float RotateSpeed;
        public GunOptions GunOptions;
        public float SizeX;
        public float SizeY;
    }
}