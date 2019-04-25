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
    public partial class MainForm : Form
    {
        private BitmapsResources bitmapsResources = new BitmapsResources();
        private FormNewGameOptions formNewGameOptions;
        private GameGraphics mineswepperGame;
        private GameParameters gameParameters;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            formNewGameOptions = new FormNewGameOptions(bitmapsResources);
            formNewGameOptions.OnGetNewGameParameters += SetCellTopColor;

            LoadNewGame();
        }

        private void SetCellTopColor()
        {
            mineswepperGame.SetCellTopColor(formNewGameOptions.GetCellTopBackColor());
        }

        private void LoadNewGame()
        {
            gameParameters = formNewGameOptions.GetGameParameters();
            mineswepperGame = new GameGraphics(gameParameters, pictureBoxGameArea, bitmapsResources, pictureBoxSmileButton, pictureBoxMinesCount, pictureBoxTime, timerGame);
        }

        private void toolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            formNewGameOptions.ShowDialog();
        }

        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                GameOptionsConstants.GetAboutText(),
                GameOptionsConstants.CaptionAboutMessage,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }
    }
}
