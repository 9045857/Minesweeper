﻿using System;
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
    public partial class MainForm : Form
    {
        private BitmapsResources bitmaps = new BitmapsResources();
        private FormNewGameOptions formNewGameOptions = new FormNewGameOptions();
        private GameGraphics mineswepperGame;
        private GameParameters gameParameters;

        public MainForm()
        {
            InitializeComponent();
        }       
                     
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadNewGame();
        }

        private void LoadNewGame()
        {
            //int rowCount = formNewGameOptions.RowCount;
            //int columnCount= formNewGameOptions.ColumnCount;
            //int minesCount= formNewGameOptions.MinesCount ;

            //mineswepperGame = new GameGraphics(rowCount, columnCount, minesCount);

            gameParameters = new GameParameters(9, 9, 10, false);

            mineswepperGame = new GameGraphics(gameParameters, pictureBoxGameArea);

           // mineswepperGame.DrawStartArea(pictureBoxGameArea /*panelGame*/);
            mineswepperGame.DrawInfoArea(panelInfo, pictureBoxSmileButton, pictureBoxMinesCount, pictureBoxTime,timerGame);
                       
        }

        private void toolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            formNewGameOptions.ShowDialog();
        }

        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GameOptionsConstants.GetAboutText());
        }
    }
}
