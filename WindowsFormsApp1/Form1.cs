using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class GameForm : Form
    {

        private Hero _hero = null;

        public GameForm()
        {
            InitializeComponent();
            _hero = new Hero();
            _hero.HealthChanged += new Hero.HealthChangedHandler(_hero_HealthChanged);

            //_hero.HealthChangedPlus += new Hero.HealthChangedPlusHandler(_hero_HealthChangedPlus);
        }

        //void _hero_HealthChangedPlus(object sender, HealthChangedArgs healthChangedArgs)
        //{
        //    _heroHealthTextBox.Text = healthChangedArgs.Health.ToString();
        //}


        //private void _damageHeroButton_Click(object sender, EventArgs e)
        //{
        //    // уменьшаем жизнь на случайное значение от 10 до 20
        //    Random random = new Random();
        //    _hero.Health = _hero.Health - random.Next(10, 20);
        //}

        void _hero_HealthChanged(object sender, EventArgs eventArgs)
        {
            // так как в качестве параметром передан базовый класс EventArgs, означающий отсутствие параметров, цифру придется читать напрямую из переменной героя
            _heroHealthLabel.Text = _hero.Health.ToString();
            if (_hero.Health <= 20 && _hero.Health > 0)
            {
                MessageBox.Show("Вы скоро умрете. Удачного вам дня!");
            }

            if (_hero.Health <= 0)
            {
                MessageBox.Show("Поздравляем, вы умерли! Наша игра - самая лучшая игра в мире!");
            }
        }

        private void _damageHeroButton_Click_1(object sender, EventArgs e)
        {
            // уменьшаем жизнь на случайное значение от 10 до 20
            Random random = new Random();
            _hero.Health = _hero.Health - random.Next(10, 20);
        }
    }

}
