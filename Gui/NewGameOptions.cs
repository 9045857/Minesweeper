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
    public partial class FormNewGameOptions : Form
    {
        public delegate void GetNewGameParametersHeadler();
        public event GetNewGameParametersHeadler OnGetNewGameParameters;

        private BitmapsResources bitmapsResources;
       
        private GameParameters gameParameters = new GameParameters();//важный параметр. Через его обновление происходит обновление игры с помощью событий

        public FormNewGameOptions(BitmapsResources bitmapsResources)
        {
            InitializeComponent();
            this.bitmapsResources = bitmapsResources;
            OnGetNewGameParameters?.Invoke();
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

            OnGetNewGameParameters?.Invoke();

            SetNewGameParametersWithCorrectionToPermissibleRange();
        }

        private void SetNewGameParametersWithCorrectionToPermissibleRange()
        {
            CorrectTextBoxesTextToPermissibleRange();
            gameParameters.SetNewGameParameters(RowCount, ColumnCount, MinesCount, IsPossibleMarkQuestion);
        }

        private void CorrectTextBoxesTextToPermissibleRange()
        {
            if (RowCount < GameOptionsConstants.CustomLevelRowCountMin)
            {
                textBoxRowCount.Text = GameOptionsConstants.CustomLevelRowCountMin.ToString();
            }
            else if (RowCount > GameOptionsConstants.CustomLevelRowCountMax)
            {
                textBoxRowCount.Text = GameOptionsConstants.CustomLevelRowCountMax.ToString();
            }

            if (ColumnCount < GameOptionsConstants.CustomLevelColumnCountMin)
            {
                textBoxColumnCount.Text = GameOptionsConstants.CustomLevelColumnCountMin.ToString();
            }
            else if (ColumnCount > GameOptionsConstants.CustomLevelColumnCountMax)
            {
                textBoxColumnCount.Text = GameOptionsConstants.CustomLevelColumnCountMax.ToString();
            }

            if (MinesCount < GameOptionsConstants.CustomLevelMinesCountMin)
            {
                textBoxMinesCount.Text = GameOptionsConstants.CustomLevelMinesCountMin.ToString();
            }
            else if (MinesCount >= RowCount * ColumnCount)
            {
                int freeCellCount = 1;
                textBoxMinesCount.Text = (RowCount * ColumnCount - freeCellCount).ToString();
            }
        }

        public GameParameters GetGameParameters()
        {
            gameParameters.RowCount = RowCount;
            gameParameters.ColumnCount = ColumnCount;
            gameParameters.MinesCount = MinesCount;
            gameParameters.IsPossibleMarkQuestion = IsPossibleMarkQuestion;

            return gameParameters;
        }

        public Color GetCellTopBackColor()
        {
            if (radioButtonCustomColor.Checked)
            {
                return colorDialogCellColor.Color;
            }

            return BackColor;
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
                return GetOptionCount(GameOptionsConstants.LowLevelRowCount, GameOptionsConstants.MediumLevelRowCount, GameOptionsConstants.HighLevelRowCount, textBoxRowCount.Text);
            }
        }

        private int ColumnCount
        {
            get
            {
                return GetOptionCount(GameOptionsConstants.LowLevelColumnCount, GameOptionsConstants.MediumLevelColumnCount, GameOptionsConstants.HighLevelColumnCount, textBoxColumnCount.Text);
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
            if (!int.TryParse(textBoxRowCount.Text, out int rowCount))
            {
                MessageBox.Show(GameOptionsConstants.WarningCorrectlySetRowCount);
                return false;
            }

            if (!int.TryParse(textBoxColumnCount.Text, out int columnCount))
            {
                MessageBox.Show(GameOptionsConstants.WarningCorrectlySetColumnCount);
                return false;
            }

            if (!int.TryParse(textBoxMinesCount.Text, out int minesCount))
            {
                MessageBox.Show(GameOptionsConstants.WarningCorrectlySetMinesCount);
                return false;
            }

            return true;
        }

        private Color cellColorButton = FormNewGameOptions.DefaultBackColor;

        private Bitmap GetColorButton(Color color, Bitmap bitmap)
        {
            int height = bitmap.Height;
            int width = bitmap.Width;

            Bitmap result = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(result))
            {

                SolidBrush backFormColor = new SolidBrush(color);
                graphics.FillRectangle(backFormColor, 0, 0, width, width);
                graphics.DrawImage(bitmap, 0, 0, width, width);
                backFormColor.Dispose();
            }

            return result;
        }


        private void LoadPictureBoxCellColor(Color color, Bitmap bitmap)
        {
            Bitmap result = GetColorButton(color, bitmap);
            pictureBoxCellColor.Size = result.Size;
            pictureBoxCellColor.Image = result;
        }

        private void LoadPictureBoxStandartCellColor(Bitmap bitmap)
        {
            Bitmap result = GetColorButton(BackColor, bitmap);
            pictureBoxStandartCellColor.Size = result.Size;
            pictureBoxStandartCellColor.Image = result;
        }

        private void FormNewGameOptions_Load(object sender, EventArgs e)
        {
            LoadPictureBoxCellColor(BackColor, bitmapsResources.cellStart);
            LoadPictureBoxStandartCellColor(bitmapsResources.cellStart);
        }

        private void pictureBoxCellColor_MouseDown(object sender, MouseEventArgs e)
        {
            LoadPictureBoxCellColor(DefaultBackColor, bitmapsResources.minesNearCount[0]);
        }

        private void pictureBoxCellColor_MouseUp(object sender, MouseEventArgs e)
        {
            colorDialogCellColor.ShowDialog();
            cellColorButton = colorDialogCellColor.Color;
            LoadPictureBoxCellColor(cellColorButton, bitmapsResources.cellStart);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            LoadPictureBoxStandartCellColor(bitmapsResources.cellStart);
        }

        private void pictureBoxStandartCellColor_MouseDown(object sender, MouseEventArgs e)
        {
            LoadPictureBoxStandartCellColor(bitmapsResources.minesNearCount[0]);
        }
    }
}
