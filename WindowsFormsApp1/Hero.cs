using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Hero
    {


        // описание события
        [Browsable(true)]
        public event HealthChangedHandler HealthChanged;

        // делегат для подписывающихся на событие обработчиков
        public delegate void HealthChangedHandler(object sender, EventArgs eventArgs);


        //[Browsable(true)]
        //public event HealthChangedPlusHandler HealthChangedPlus;

        //// делегат для подписывающихся на событие обработчиков
        //public delegate void HealthChangedPlusHandler(object sender, HealthChangedArgs eventArgs);


        private int _health = 100;
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
                // если на событие не подписан ни один обработчик, здесь будет null
                if (HealthChanged != null)
                {
                    EventArgs eventArgs = new EventArgs();
                    HealthChanged(this, eventArgs);

                    //// второй вариант события, в котором обработчику передаются параметры
                    //HealthChangedArgs healthChangedArgs = new HealthChangedArgs(_health);
                    //HealthChangedPlus(this, healthChangedArgs);
                }

            }
        }
    }



}

