using System.Collections.Generic;
using System.Linq;

namespace CoreEngine.Core
{
    public class CollisionTracker
    {
        private readonly List<IObject> _objects = new List<IObject>();

        public void Add(IObject obj)
        {
            obj.PositionChanged += GameObjectOnPositionChanged;
            obj.Destroyed += sender =>
            {
                _objects.Remove(sender);
                obj.PositionChanged -= GameObjectOnPositionChanged;
            };
            _objects.Add(obj);
        }
        
        private void GameObjectOnPositionChanged(IObject sender)
        {
            var collisions = _objects
                .Where(item => !item.Equals(sender)
                               && sender.IsCollision(item))
                .ToArray();
            foreach (var collision in collisions)
            {
                sender.OnCollision(collision);
            }
        }
    }
}