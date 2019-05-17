using System.Threading;

namespace Logic
{
    class GameTimer
    {
        public delegate void TimeChange(int currentTime);
        public event TimeChange OnTimeChange;

        private readonly int pauseTime = GameLogicConstants.PauseTime;

        public int CurrentTime { get; private set; }

        private Timer timer;

        public GameTimer()
        {
            timer = new Timer(new TimerCallback(ChangeTime), null, Timeout.Infinite, Timeout.Infinite);
        }

        private void ChangeTime(object o)
        {
            CurrentTime++;
            OnTimeChange?.Invoke(CurrentTime);
        }

        public void Start()
        {
            CurrentTime = 0;
            timer.Change(pauseTime, pauseTime);
        }

        public void Stop()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
