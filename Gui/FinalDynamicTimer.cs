using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Gui
{
    class FinalDynamicTimer
    {
        public delegate void StartTimer();
        public event StartTimer OnStartTimer;

        public delegate void StartAnimation();
        public event StartAnimation OnStartAnimation;

        public delegate void StopAnimation();
        public event StopAnimation OnStopAnimation;

        private Timer timer;

        private int beforeAnimationTime;
        private int animationTime;

        private int currentStage;

        /// <summary>
        /// Таймер для финальной анимации.
        /// Сценарий:
        /// 1- небольшая пауза перед появлением картики анимации, что бы пользователь увидел результат нажатия
        /// 2- некоторое непродолжительное время картинка должна быть и анимироваться
        /// 3- конец анимации
        /// </summary>
        public FinalDynamicTimer()
        {
            timer = new Timer(new TimerCallback(ChangeTime), null, Timeout.Infinite, Timeout.Infinite);
        }

        private void ChangeTime(object o)
        {
            switch (currentStage)
            {
                case 0:
                    OnStartTimer?.Invoke();

                    timer.Change(beforeAnimationTime,0);
                    currentStage++;
                    break;

                case 1:
                    OnStartAnimation?.Invoke();

                    timer.Change(animationTime, 0);
                    currentStage++;
                    break;

                case 2:
                    OnStopAnimation?.Invoke();

                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    currentStage=0;
                    break;
            }
        }

        public void Start(int beforeAnimationTime, int animationTime)
        {
            this.beforeAnimationTime = beforeAnimationTime;
            this.animationTime = animationTime;

            currentStage = 0;
            timer.Change(0, 0);
        }
    }
}
