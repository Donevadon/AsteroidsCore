using System;

namespace CoreEngine.Core.Configurations
{
    [Serializable]
    public class AsteroidOptions
    {
        public float MoveSpeed;
        public float RotateSpeed;
        public int FragmentCount;
        public FragmentAsteroidOptions FragmentAsteroidOptions;
    }
}