using System.Collections.Generic;
using System.Linq;

namespace CoreEngine.Core
{
    public class CollisionTracker
    {
        private readonly List<ICollisionObject> _objects = new();

        public void Add(object obj)
        {
            if (obj is not ICollisionObject collisionObject) return;
            collisionObject.Updated += GameObjectOnUpdated;
            _objects.Add(collisionObject);
        }

        public void Remove(object obj)
        {
            if (obj is not ICollisionObject collisionObject) return;
             _objects.Remove(collisionObject);
             collisionObject.Updated -= GameObjectOnUpdated;
        }
        
        private void GameObjectOnUpdated(ICollisionObject sender)
        {
            var collisions = _objects
                .Where(item => !item.Equals(sender)
                               && sender.IsCollision(item))
                .ToArray();
            foreach (var collision in collisions)
            {
                sender.OnCollision(collision);
                collision.OnCollision(sender);
            }
        }
    }
}