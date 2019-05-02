using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Minesweeper.Logic
{
    class GameTime
    {
        public delegate void TimeChange(int currentTime);
        public event TimeChange OnTimeChange;

        public delegate void TimeIsOver();
        public event TimeIsOver OnTimeIsOver;

        private Thread timeThread;
        private bool isThrearValid;

        public int CurrentTime { get; private set; }

        public GameTime()
        {
            CurrentTime = 0;
            timeThread = new Thread(ChangeTime);
        }

        private void ChangeTime()
        {
            int pauseTime = GameLogicConstants.PauseTime;
            int maxGameTime = GameLogicConstants.MaxGameTime;

            while (isThrearValid)
            {
                Thread.Sleep(pauseTime);
                CurrentTime++;

                OnTimeChange?.Invoke(CurrentTime);

                if (CurrentTime == maxGameTime)
                {
                    isThrearValid = false;
                    OnTimeIsOver?.Invoke();
                }
            }
        }

        public void Start()
        {
            CurrentTime = 0;
            isThrearValid = true;

            timeThread.Start();
        }

        public void Stop()
        {
            isThrearValid = false;
        }
    }
}
