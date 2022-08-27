namespace CoreEngine.Core.Factory
{
    public class Factory
    {
        private readonly CoreEngine _engine;
        private static readonly CollisionTracker Collision = new();

        protected Factory(CoreEngine engine)
        {
            _engine = engine;
        }

        protected void InitInEngine(IObject? obj)
        {
            _engine.FrameUpdated += obj.Update;
            obj.Destroyed += sender => _engine.FrameUpdated -= sender.Update;
            Collision.Add(obj);
        }
    }
}