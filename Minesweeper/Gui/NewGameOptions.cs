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
    public partial class FormNewGameOptions : Form
    {
        //public delegate void GetNewGameParametersHeadler(object sender, EventArgs eventArgs);
        //public event GetNewGameParametersHeadler GetNewGameParameters;

        private GameParameters gameParameters = new GameParameters();

        public FormNewGameOptions()
        {
            InitializeComponent();
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            if (radioButtonCustom.Checked)
            {
                if (!IsInputCustomDataCorrect())
                {
                    return;
                }
            }

            Close();

            gameParameters.SetNewGameParameters(RowCount, ColumnCount, MinesCount, IsPossibleMarkQuestion);

            // MessageBox.Show(RowCount+"  "+ColumnCount+"  "+MinesCount);
           //  MessageBox.Show(gameParameters.RowCount + "  "+ gameParameters.ColumnCount + "  "+ gameParameters.MinesCount);

        }

        public GameParameters GetGameParameters()
        {
            gameParameters.RowCount = RowCount;
            gameParameters.ColumnCount = ColumnCount;
            gameParameters.MinesCount = MinesCount;
            gameParameters.IsPossibleMarkQuestion = IsPossibleMarkQuestion;

            return gameParameters;
        }

        private int GetOptionCount(int lowLevelOptionValue, int mediumLevelOptionValue, int highLevelOptionValue, string customOptionValue)
        {
            if (radioButtonLow.Checked)
            {
                return lowLevelOptionValue;
            }
            else if (radioButtonMedium.Checked)
            {
                return mediumLevelOptionValue;
            }
            else if (radioButtonHigh.Checked)
            {
                return highLevelOptionValue;
            }
            else
            {
                if (IsInputCustomDataCorrect())
                {
                    return Convert.ToInt16(customOptionValue);
                }
                else
                {
                    return lowLevelOptionValue;
                }
            }
        }

        private int RowCount
        {
            get
            {
                return GetOptionCount(GameOptionsConstants.LowLevelRowCount, GameOptionsConstants.MediumLevelRowCount, GameOptionsConstants.HighLevelRowCount, textBoxHeight.Text);
            }
        }

        private int ColumnCount
        {
            get
            {
                return GetOptionCount(GameOptionsConstants.LowLevelColumnCount, GameOptionsConstants.MediumLevelColumnCount, GameOptionsConstants.HighLevelColumnCount, textBoxWidth.Text);
            }
        }

        private int MinesCount
        {
            get
            {
                return GetOptionCount(GameOptionsConstants.LowLevelMinesCount, GameOptionsConstants.MediumLevelMinesCount, GameOptionsConstants.HighLevelMinesCount, textBoxMinesCount.Text);
            }
        }

        private bool IsPossibleMarkQuestion
        {
            get
            {
                return checkBoxMark.Checked;
            }
        }

        private bool IsInputCustomDataCorrect()
        {
            int rowCount;
            int columnCount;
            int minesCount;

            if (!int.TryParse(textBoxHeight.Text, out rowCount))
            {
                MessageBox.Show("Некорректно задана высота.");
                return false;
            }

            if (!int.TryParse(textBoxWidth.Text, out columnCount))
            {
                MessageBox.Show("Некорректно задана ширина.");
                return false;
            }

            if (!int.TryParse(textBoxMinesCount.Text, out minesCount))
            {
                MessageBox.Show("Некорректно задано количество мин.");
                return false;
            }

            return true;
        }

        private void FormNewGameOptions_Load(object sender, EventArgs e)
        {

        }
    }
}
