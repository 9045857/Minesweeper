using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gui
{
    public partial class HighScoreDialog : Form
    {
        public delegate void SetUserNameEvent();
        public event SetUserNameEvent OnSetUserName;


        public bool IsSaveHighScore { get; private set; }

        public HighScoreDialog()
        {
            InitializeComponent();
        }

        private void HighScoreDialog_Load(object sender, EventArgs e)
        {
            labelCongratulation.Text = GameOptionsConstants.HighScoreCongratulation;
        }

        public string UserName { get; private set; }

        private int gameTime;

        public int GameTime
        {
            get
            {
                return gameTime;
            }
            set
            {
                gameTime = value;
                labelTimeResult.Text = value.ToString();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SetUserName();
        }

        private void SetUserName()
        {
            if (string.IsNullOrEmpty(textBoxUserName.Text))
            {
                UserName = "игрок";
            }
            else
            {
                UserName = textBoxUserName.Text;

                if (UserName.Length > GameOptionsConstants.UserNameMaxLengh)
                {
                    UserName = UserName.Substring(0, GameOptionsConstants.UserNameMaxLengh);
                }
            }

            IsSaveHighScore = true;
            
            OnSetUserName?.Invoke();
            Close();
           // Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxUserName.Text = "";
            UserName = "";
            IsSaveHighScore = false;
            Close();
            //  Dispose();
        }

        private void textBoxUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetUserName();
            }
        }
    }
}
