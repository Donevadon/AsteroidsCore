using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace CoreEngine.Core
{
    public abstract class CoreEngine : IDisposable
    {
        public event Func<Task> FrameUpdated;
        private readonly Thread _updateObjectsThread;
        private readonly Thread _spawnAsteroidsThread;

        protected abstract IObjectPool Pool { get; }

        protected CoreEngine()
        {
            _updateObjectsThread = new Thread(UpdateObjects);
            _spawnAsteroidsThread = new Thread(SpawnAsteroids);
        }

        public void Start()
        {
            CreatePlayer(Vector2.Zero);
            _updateObjectsThread.Start();
            _spawnAsteroidsThread.Start();
        }

        private void CreatePlayer(Vector2 position)
        {
            _ = Pool.GetPlayer(position);
        }

        private void UpdateObjects()
        {
            while (true)
            {
                _ = FrameUpdated?.Invoke();
                Thread.Sleep(20);
            }
        }
        
        private void SpawnAsteroids()
        {
            while (true)
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                _ = Pool.GetAsteroid(new Vector2(1, 1));
            }
        }

        public void Dispose()
        {
            _updateObjectsThread.Abort();
            _spawnAsteroidsThread.Abort();
        }
    }
}