using Logic;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gui
{
    class GameInfoGraphics
    {
        private readonly BitmapsResources bitmapsResources;

        private GameLogic gameLogic;
        private GameAreaGraphics gameAreaGraphics;

        private PictureBox smileButtonImage;
        private PictureBox timeImage;
        private PictureBox minesCountImage;

        private Panel infoPanel;

        public GameInfoGraphics
        (
            GameLogic gameLogic,
            GameAreaGraphics gameAreaGraphics,
            BitmapsResources bitmapsResources,
            PictureBox pictureBoxSmileButton,
            PictureBox pictureBoxMinesCount,
            PictureBox pictureBoxTime
        )
        {
            this.gameLogic = gameLogic;
            this.gameLogic.OnMark += new GameLogic.MarkFlagCellHeadler(RedrawMarkedMinesCount);
            this.gameLogic.OnBeginNewGame += RestartDislays;
            this.gameLogic.OnExploded += smileButtonImage_OnExploded;
            this.gameLogic.OnTimeChange += SetTimeOnDisplay;

            this.gameAreaGraphics = gameAreaGraphics;

            infoPanel = pictureBoxSmileButton.Parent.Parent as Panel;
            infoPanel.Width = gameAreaGraphics.GamePanelAreaWidth;

            gameAreaGraphics.OnSetGamePanelWidth += SetInfoPanelWidth;
            gameAreaGraphics.OnMouseDownCells += smileButtonImage_OnMouseDownCells;
            gameAreaGraphics.OnMouseUpCells += smileButtonImage_OnMouseUpCells;

            this.bitmapsResources = bitmapsResources;
            smileButtonImage = pictureBoxSmileButton;
            minesCountImage = pictureBoxMinesCount;
            timeImage = pictureBoxTime;

            DrawInfoPanel();
        }

        private void SetInfoPanelWidth(int width)
        {
            infoPanel.Width = width;
        }

        private void smileButtonImage_OnExploded(int remainedMinesCount)
        {
            smileButtonImage.Image = bitmapsResources.smileButtonCry;
            minesCountImage.Image = GetBitmapNumericDisplay(remainedMinesCount);
        }

        private void smileButtonImage_OnMouseDownCells()
        {
            smileButtonImage.Image = bitmapsResources.smileButtonAttention;
        }

        private void smileButtonImage_OnMouseUpCells()
        {
            smileButtonImage.Image = bitmapsResources.smileButton;
        }

        private void DrawInfoPanel()
        {
            smileButtonImage.Image = bitmapsResources.smileButton;
            smileButtonImage.MouseUp += new MouseEventHandler(SmileButtonPictureBox_MouseUp);
            smileButtonImage.MouseDown += new MouseEventHandler(SmileButtonPictureBox_MouseDown);
            smileButtonImage.MouseLeave += new EventHandler(SmileButtonPictureBox_MouseLeave);

            int startTime = 0;
            timeImage.Image = GetBitmapNumericDisplay(startTime);

            int minesCount = gameLogic.MinesCount;
            minesCountImage.Image = GetBitmapNumericDisplay(minesCount);
        }

        private void RedrawMarkedMinesCount(int markMinesCount)
        {
            minesCountImage.Image = GetBitmapNumericDisplay(markMinesCount);
        }

        private void SetTimeOnDisplay(int currentTime)
        {
            timeImage.Image = GetBitmapNumericDisplay(currentTime);
        }

        private Bitmap GetBitmapNumericDisplay(int number)
        {
            int numbersCount = 3;
            int numberWidth = bitmapsResources.numbers[0].Width;
            int numberHeight = bitmapsResources.numbers[0].Height;

            int bitmapWidth = numbersCount * numberWidth;
            int bitmapHeight = numberHeight;

            Bitmap resultBitmap = new Bitmap(bitmapWidth, bitmapHeight);

            int minNumber = -99;
            int maxNumber = 999;

            if (number < minNumber)
            {
                Bitmap minusBitmap = bitmapsResources.clockMinus;

                using (Graphics graphics = Graphics.FromImage(resultBitmap))
                {
                    graphics.DrawImage(minusBitmap, new Rectangle(0, 0, numberWidth, numberHeight));
                    graphics.DrawImage(minusBitmap, new Rectangle(numberWidth, 0, numberWidth, numberHeight));
                    graphics.DrawImage(minusBitmap, new Rectangle(numberWidth + numberWidth, 0, numberWidth, numberHeight));
                }

                return resultBitmap;
            }

            if (number > maxNumber)
            {
                number = maxNumber;
            }

            int hundred = 100;
            int ten = 10;
            int hundredRank = number / hundred;
            int tenRank = (number - hundredRank * hundred) / ten;
            int unitRank = number - hundredRank * hundred - tenRank * ten;

            Bitmap bitmapHandredRank;
            Bitmap bitmapTenRank;
            Bitmap bitmapUnitRank;

            if (number >= 0)
            {
                bitmapHandredRank = bitmapsResources.numbers[hundredRank];
                bitmapTenRank = bitmapsResources.numbers[tenRank];
                bitmapUnitRank = bitmapsResources.numbers[unitRank];
            }
            else
            {
                bitmapHandredRank = bitmapsResources.clockMinus;
                bitmapTenRank = bitmapsResources.numbers[Math.Abs(tenRank)];
                bitmapUnitRank = bitmapsResources.numbers[Math.Abs(unitRank)];
            }

            using (Graphics graphics = Graphics.FromImage(resultBitmap))
            {
                graphics.DrawImage(bitmapHandredRank, new Rectangle(0, 0, numberWidth, numberHeight));
                graphics.DrawImage(bitmapTenRank, new Rectangle(numberWidth, 0, numberWidth, numberHeight));
                graphics.DrawImage(bitmapUnitRank, new Rectangle(numberWidth + numberWidth, 0, numberWidth, numberHeight));
            }

            return resultBitmap;
        }

        private bool isSmileButtonDown = false;

        private void SmileButtonPictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (isSmileButtonDown)
            {
                smileButtonImage.Image = bitmapsResources.smileButton;
                isSmileButtonDown = false;
            }
        }

        private bool IsMouseOnSmileButton(object sender,MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0)
            {
                return false;
            }

            if (e.X > (sender as PictureBox).Width || e.Y > (sender as PictureBox).Height)
            {
                return false;
            }

            return true;
        }

        private void SmileButtonPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSmileButtonDown&&e.Button == MouseButtons.Left&&IsMouseOnSmileButton(sender,e))
            {
                smileButtonImage.Image = bitmapsResources.smileButton;
                isSmileButtonDown = false;
                RestartGame();
            }
        }

        private void SmileButtonPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                smileButtonImage.Image = bitmapsResources.smileButtonPressed;
                isSmileButtonDown = true;
            }
        }

        private void RestartGame()
        {
            gameLogic.RestartCurrentGame();
            RestartDislays();
        }

        private void RestartDislays()
        {
            minesCountImage.Image = GetBitmapNumericDisplay(gameLogic.MinesCount);
            smileButtonImage.Image = bitmapsResources.smileButton;
        }
    }
}
