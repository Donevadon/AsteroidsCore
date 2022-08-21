using System;

namespace CoreEngine.Core.Configurations
{
    [Serializable]
    public class Options
    {
        public ScreenSize ScreenSize;
        public PlayerOptions PlayerOptions;
        public AsteroidOptions AsteroidOptions;
        public AlienOptions AlienOptions;
    }
}