using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;


namespace Gui
{
    public partial class MainForm : Form
    {
        private FormNewGameOptions formNewGameOptions;
        private HighScoreDialog highScoreDialog;

        private BitmapsResources bitmapsResources = new BitmapsResources();
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

            highScoreDialog = new HighScoreDialog();

            LoadNewGame();
        }

        private void SetCellTopColor()
        {
            mineswepperGame.SetCellTopColor(formNewGameOptions.GetCellTopBackColor());
        }

        private void LoadNewGame()
        {
            gameParameters = formNewGameOptions.GetGameParameters();
            mineswepperGame = new GameGraphics(gameParameters, pictureBoxGameArea, bitmapsResources, pictureBoxSmileButton, pictureBoxMinesCount, pictureBoxTime, highScoreDialog);
        }

        private void toolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            formNewGameOptions.ShowDialog();
        }

        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GameOptionsConstants.GetAboutText(), GameOptionsConstants.CaptionAboutMessage, MessageBoxButtons.OK);
        }

        private void toolStripMenuItemHighScore_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mineswepperGame.GetHighScore(), GameOptionsConstants.CaptionHighScore, MessageBoxButtons.OK);
        }
    }
}
