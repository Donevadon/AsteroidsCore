using System;

namespace CoreEngine.Entities.Objects
{
    public class ReloadTimer
    {
        private DateTime _lastTime = DateTime.Now;
        public event Action<TimeSpan> TimeUpdated;
        public event Action<int> Reloaded;
        
        public bool Timer(TimeSpan time, int sequenceNumber)
        {
            var delta = DateTime.Now - _lastTime;
            var result = delta > time;
            if (result)
            {
                _lastTime = DateTime.Now;
                Reloaded?.Invoke(sequenceNumber);
            }
            TimeUpdated?.Invoke(time - delta);

            return result;
        }

        public void Reset(int sequenceNumber)
        {
            Reloaded?.Invoke(sequenceNumber);
        }
    }
}