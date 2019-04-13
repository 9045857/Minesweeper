using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Logic;


namespace Minesweeper.Gui
{
    public partial class mainForm : Form
    {
        private BitmapsResources bitmaps = new BitmapsResources();

        public mainForm()
        {
            InitializeComponent();
        }       

        private GameGraphics mineswepperGame;

        private void MainForm_Load(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            mineswepperGame = new GameGraphics(9, 9, 10);//начинающий
            mineswepperGame.DrawStartArea(panelGame);
            mineswepperGame.DrawInfoArea(panelInfo, pictureBoxSmileButton, pictureBoxMinesCount, pictureBoxTime);
        }

        private void pictureBoxSmileButton_Click(object sender, EventArgs e)
        {           
            //NewMethod();
        }        
    }
}
