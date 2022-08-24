using System.Collections.Generic;
using System.Linq;

namespace CoreEngine.Core
{
    public class CollisionTracker
    {
        private readonly List<IObject> _objects = new List<IObject>();

        public void Add(IObject obj)
        {
            obj.Updated += GameObjectOnUpdated;
            obj.Destroyed += sender =>
            {
                _objects.Remove(sender);
                obj.Updated -= GameObjectOnUpdated;
            };
            _objects.Add(obj);
        }
        
        private void GameObjectOnUpdated(IObject sender)
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