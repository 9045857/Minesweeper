using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Logic;
using System.Windows.Forms;
using System.Drawing;

namespace Minesweeper.Gui
{
    class GameInfoGraphics
    {
        private readonly BitmapsResources bitmapsResources;

        private GameLogic gameLogic;
        //  private GameAreaGraphics areaGraphics;

        private PictureBox smileButtonImage;
        private PictureBox timeImage;
        private PictureBox minesCountImage;

        private Timer gameTimer;
        int currentGameTime;
        private readonly int maxTime = 999;

        public GameInfoGraphics
        (
            GameLogic gameLogic,
            BitmapsResources bitmapsResources,
            PictureBox pictureBoxSmileButton,
            PictureBox pictureBoxMinesCount,
            PictureBox pictureBoxTime,
            Timer timerGame
        )
        {
            this.gameLogic = gameLogic;
            this.gameLogic.OnMark += new GameLogic.MarkFlagCellHeadler(RedrawMarkedMinesCount);
            this.gameLogic.OnStartGame += new GameLogic.StartGameHeadler(StartTimer);
            this.gameLogic.OnFinishGame += new GameLogic.FinishGameHeadler(StopTimer);
            this.gameLogic.OnBeginNewGame += RestartDislays;

            this.bitmapsResources = bitmapsResources;
            smileButtonImage = pictureBoxSmileButton;
            minesCountImage = pictureBoxMinesCount;
            timeImage = pictureBoxTime;
            gameTimer = timerGame;

            DrawInfoPanel();
        }

        private void DrawInfoPanel()
        {
            smileButtonImage.Image = bitmapsResources.smileButton;
            smileButtonImage.MouseUp += new MouseEventHandler(SmileButtonPictureBox_MouseUp);
            smileButtonImage.MouseDown += new MouseEventHandler(SmileButtonPictureBox_MouseDown);

            int startTime = 0;
            timeImage.Image = GetBitmapNumericDisplay(startTime);

            int minesCount = gameLogic.MinesCount;
            minesCountImage.Image = GetBitmapNumericDisplay(minesCount);

            gameTimer.Interval = 1000;
            gameTimer.Enabled = false;
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
        }





        private void RedrawMarkedMinesCount(int markMinesCount)//Метод для отрисовки события маркировки
        {
            minesCountImage.Image = GetBitmapNumericDisplay(markMinesCount);
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

        private void RestartGame()
        {
            gameLogic.RestartCurrentGame();
            RestartDislays();
        }

        private void StartTimer()
        {
            gameTimer.Enabled = true;
        }

        private void StopTimer()
        {
            gameTimer.Enabled = false;
        }


        private void RestartDislays()
        {
            gameTimer.Enabled = false;

            currentGameTime = 0;
            timeImage.Image = GetBitmapNumericDisplay(currentGameTime);

            minesCountImage.Image = GetBitmapNumericDisplay(gameLogic.MinesCount);
        }








    }
}
