using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace CoreEngine.Core
{
    public abstract class CoreEngine
    {
        private readonly List<GameObject> _objects = new List<GameObject>();
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
            CreatePlayer();
            _updateObjectsThread.Start();
            _spawnAsteroidsThread.Start();
        }

        private void CreatePlayer()
        {
            var player = Pool.GetPlayer();
            Console.WriteLine("Player Created");
            Add(player);
        }

        private void UpdateObjects()
        {
            while (true)
            {
                for (var i = 0; i < _objects.Count; i++)
                {
                    var gameObject = Get(i);
                    gameObject.Update();
                }
                Thread.Sleep(20);
            }
        }

        private void Add(GameObject obj)
        {
            lock (this)
            {
                _objects.Add(obj);
            }
        }

        private GameObject Get(int index)
        {
            lock (this)
            {
                return _objects[index];
            }
        }

        private void SpawnAsteroids()
        {
            while (true)
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                var asteroid = Pool.GetAsteroid(new Vector2(1, 1));
                Add(asteroid);
            }
        }
    }
}