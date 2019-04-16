using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper.Gui
{
    public partial class FormNewGameOptions : Form
    {
        public FormNewGameOptions()
        {
            InitializeComponent();
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

        public int RowCount
        {
            get
            {
                return GetOptionCount(GameOptionsConstants.LowLevelRowCount, GameOptionsConstants.MediumLevelRowCount, GameOptionsConstants.HighLevelRowCount, textBoxHeight.Text);
            }
        }

        public int ColumnCount
        {
            get
            {
                return GetOptionCount(GameOptionsConstants.LowLevelColumnCount, GameOptionsConstants.MediumLevelColumnCount, GameOptionsConstants.HighLevelColumnCount, textBoxWidth.Text);
            }
        }

        public int MinesCount
        {
            get
            {
                return GetOptionCount(GameOptionsConstants.LowLevelMinesCount, GameOptionsConstants.MediumLevelMinesCount, GameOptionsConstants.HighLevelMinesCount, textBoxMinesCount.Text);
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
        }

        private void FormNewGameOptions_Load(object sender, EventArgs e)
        {

        }
    }
}
