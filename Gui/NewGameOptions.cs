using Logic;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gui
{
    public partial class FormNewGameOptions : Form
    {
        public delegate void GetNewGameParametersHeadler();
        public event GetNewGameParametersHeadler OnGetNewGameParameters;

        private BitmapsResources bitmapsResources;

        /// <summary>
        /// Объект группы Логики.
        /// Через обновление его параметров происходит обновление игры с помощью события.
        /// </summary>
        private GameParameters gameParameters = new GameParameters();

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

            ChangeCustomDataIfNeed();
        }

        private void ChangeCustomDataIfNeed()
        {
            if (radioButtonCustom.Checked)
            {
                textBoxRowCount.Text = gameParameters.RowCount.ToString();
                textBoxColumnCount.Text = gameParameters.ColumnCount.ToString();
                textBoxMinesCount.Text = gameParameters.MinesCount.ToString();
            }
        }

        private void SetNewGameParametersWithCorrectionToPermissibleRange()
        {
            gameParameters.SetNewGameParameters(RowCount, ColumnCount, MinesCount, IsPossibleMarkQuestion);
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
                    if (Int32.TryParse(customOptionValue, out int data))
                    {
                        return data;
                    }
                    else
                    {
                        return lowLevelOptionValue;//возможно тут нужно все-таки анализироват данные сразу, и предлагать точную замену до передачи в логику
                    }
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
                return GetOptionCount(GameLogicConstants.LowLevelRowCount, GameLogicConstants.MediumLevelRowCount, GameLogicConstants.HighLevelRowCount, textBoxRowCount.Text);
            }
        }

        private int ColumnCount
        {
            get
            {
                return GetOptionCount(GameLogicConstants.LowLevelColumnCount, GameLogicConstants.MediumLevelColumnCount, GameLogicConstants.HighLevelColumnCount, textBoxColumnCount.Text);
            }
        }

        private int MinesCount
        {
            get
            {
                return GetOptionCount(GameLogicConstants.LowLevelMinesCount, GameLogicConstants.MediumLevelMinesCount, GameLogicConstants.HighLevelMinesCount, textBoxMinesCount.Text);
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

            DisableCustomTextBoxes();
        }

        private void DisableCustomTextBoxes()
        {
            textBoxRowCount.Enabled = false;
            textBoxColumnCount.Enabled = false;
            textBoxMinesCount.Enabled = false;
        }

        private void AbleCustomTextBoxes()
        {
            textBoxRowCount.Enabled = true;
            textBoxColumnCount.Enabled = true;
            textBoxMinesCount.Enabled = true;
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

        private void radioButtonCustom_CheckedChanged(object sender, EventArgs e)
        {
            AbleCustomTextBoxes();
        }

        private void radioButtonLow_CheckedChanged(object sender, EventArgs e)
        {
            DisableCustomTextBoxes();
        }

        private void radioButtonMedium_CheckedChanged(object sender, EventArgs e)
        {
            DisableCustomTextBoxes();
        }

        private void radioButtonHigh_CheckedChanged(object sender, EventArgs e)
        {
            DisableCustomTextBoxes();
        }
    }
}
