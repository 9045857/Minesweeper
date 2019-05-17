using Logic;
using System;
using System.Windows.Forms;

namespace Gui
{
    public partial class MainForm : Form
    {
        private FormNewGameOptions formNewGameOptions;
        private HighScoreDialog highScoreDialog;
        private HighScoreForm highScoreForm;

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
            highScoreForm = new HighScoreForm(mineswepperGame);
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
            highScoreForm.ShowDialog();
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }      
    }
}
