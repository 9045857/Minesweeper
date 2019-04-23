using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Logic;
using System.Drawing;
using System.Windows.Input;

namespace Minesweeper.Gui
{
    class GameGraphics
    {
        private readonly BitmapsResources bitmapsResources = new BitmapsResources();
        //private readonly int cellSideLength;

        //private readonly int rowCount;
        //private readonly int columnCount;
        private readonly int minesCount;

        private GameLogic gameLogic;
        private GameAreaGraphics gameAreaGraphics;

      //  private GameParameters gameParameters;

        private PictureBox smileButtonImage;
        private PictureBox timeImage;
        private PictureBox minesCountImage;

        private Timer gameTimer;
        int currentGameTime;
        private readonly int maxTime = 999;

        private int panelsWidth;


        List<Cell> cellsNearRightLeftMouseButtons = new List<Cell>();

        public GameGraphics(GameParameters gameParameters,PictureBox gameAreaPictureBox)
        {
            gameLogic = new GameLogic(gameParameters);
          // gameLogic.BeginNewGame += new GameLogic.BeginNewGameHeadler();

            gameAreaGraphics = new GameAreaGraphics(gameAreaPictureBox, gameLogic);

            //cellSideLength = bitmapsResources.cellStart.Height;

            currentGameTime = 0;
        }

        public void DrawInfoArea(Panel infoPanel, PictureBox smileButtonImage, PictureBox minesCountImage, PictureBox timeImage, Timer timer)
        {
            DrawInfoPanel(infoPanel, smileButtonImage, minesCountImage, timeImage, timer);
        }

        private void RestartDislays()
        {
            int startTime = 0;
            timeImage.Image = GetBitmapNumericDisplay(startTime);

            minesCountImage.Image = GetBitmapNumericDisplay(minesCount);

            gameTimer.Enabled = false;
            currentGameTime = 0;
        }

        private void RestartGame()
        {
            // gameLogic.ClearCellsOptions();
            RestartDislays();
        }

        private void SetTimerTrueIfGameBegin()
        {
            if (!gameLogic.AreMinesSet)
            {
                gameTimer.Enabled = true;
            }
        }

        private void SetTimerFalseIfGameFinish()
        {
            if (!gameLogic.IsGameContinue)
            {
                gameTimer.Enabled = false;
            }
        }

        private void SetRemainigMinesCountIfGameOver()
        {
            if (!gameLogic.IsGameContinue && gameLogic.isDontExploded)
            {
                int remaingMines = 0;
                minesCountImage.Image = GetBitmapNumericDisplay(remaingMines);
            }
        }

        private void DrawRemainingMinesCountAfterMarkOnDispley()
        {
            int remainingMinesCountAfterMark = gameLogic.MinesCount - gameLogic.MarkedMinesCount;
            minesCountImage.Image = GetBitmapNumericDisplay(remainingMinesCountAfterMark);
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

            if (number < minNumber || number > maxNumber)
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

            int hundred = 100;
            int ten = 10;
            int hundredRank = number / hundred;
            int tenRank = (number - hundredRank * hundred) / ten;
            int unitRank = number - hundredRank * hundred - tenRank * ten;//TODO check this on correct

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

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (currentGameTime >= maxTime)
            {
                gameTimer.Enabled = false;
                gameLogic.FinishGame();

                return;
            }

            currentGameTime++;
            timeImage.Image = GetBitmapNumericDisplay(currentGameTime);
        }

        private void DrawSmileButtonIfCellDown()
        {
            smileButtonImage.Image = bitmapsResources.smileButtonAttention;
        }

        private void DrawSmileButtonIfCellUp()
        {
            if (gameLogic.isDontExploded)
            {
                smileButtonImage.Image = bitmapsResources.smileButton;
            }
            else
            {
                smileButtonImage.Image = bitmapsResources.smileButtonCry;
            }
        }

        private void DrawInfoPanel(Panel infoPanel, PictureBox smileButtonImage, PictureBox minesCountImage, PictureBox timeImage, Timer timer)
        {
            //  infoPanel.Width = panelsWidth;//так как размер определятся формой, то эта ширина

            this.smileButtonImage = smileButtonImage;
            this.smileButtonImage.Image = bitmapsResources.smileButton;
            smileButtonImage.MouseUp += new MouseEventHandler(SmileButtonPictureBox_MouseUp);
            smileButtonImage.MouseDown += new MouseEventHandler(SmileButtonPictureBox_MouseDown);

            this.timeImage = timeImage;
            int startTime = 0;
            this.timeImage.Image = GetBitmapNumericDisplay(startTime);

            this.minesCountImage = minesCountImage;
            this.minesCountImage.Image = GetBitmapNumericDisplay(minesCount);

            gameTimer = timer;
            gameTimer.Interval = 1000;
            gameTimer.Enabled = false;
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
        }

        private void SmileButtonPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                smileButtonImage.Image = bitmapsResources.smileButton;
                RestartGame();
            }
        }

        private void SmileButtonPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                smileButtonImage.Image = bitmapsResources.smileButtonPressed;
            }
        }


    }
}
