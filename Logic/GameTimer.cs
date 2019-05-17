using System.Threading;

namespace Logic
{
    class GameTimer
    {
        public delegate void TimeChange(int currentTime);
        public event TimeChange OnTimeChange;

        private Thread timeThread;
        ManualResetEvent threadValid = new ManualResetEvent(false);

        private readonly int pauseTime = GameLogicConstants.PauseTime;

        public int CurrentTime { get; private set; }

        public GameTimer()
        {
            timeThread = new Thread(ChangeTime);
            timeThread.IsBackground = true;

            timeThread.Start();
        }

        private void ChangeTime()
        {
            while (true)
            {
                threadValid.WaitOne();

                OnTimeChange?.Invoke(CurrentTime);

                Thread.Sleep(pauseTime);
                CurrentTime++;
            }
        }

        public void Start()
        {
            CurrentTime = 0;
            threadValid.Set();
        }

        public void Stop()
        {
            threadValid.Reset();
        }
    }
}
