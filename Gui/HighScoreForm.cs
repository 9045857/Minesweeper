using System;
using System.Windows.Forms;

namespace Gui
{
    public partial class HighScoreForm : Form
    {
        GameGraphics gameGraphics;

        public HighScoreForm(GameGraphics gameGraphics)
        {
            InitializeComponent();
            this.gameGraphics = gameGraphics;
        }

        private void HighScoreForm_Load(object sender, EventArgs e)
        {           
            textBoxHighScore.Text = gameGraphics.GetHighScore();
        }

        private void textBoxHighScore_Enter(object sender, EventArgs e)
        {
            buttonOK.Focus();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {            
            if (MessageBox.Show(GameOptionsConstants.WarningRemoveHighScore, GameOptionsConstants.CaptionRemoveHighScore, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gameGraphics.ClearHighScore();
                textBoxHighScore.Text = gameGraphics.GetHighScore();
            }
        }
    }
}
