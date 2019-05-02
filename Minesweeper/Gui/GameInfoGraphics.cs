﻿using System;
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
            gameAreaGraphics.OnSetGamePanelWidth += SetInfoPanelWidth;
            gameAreaGraphics.OnMouseDownCells +=smileButtonImage_OnMouseDownCells;
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

            int startTime = 0;
            timeImage.Image = GetBitmapNumericDisplay(startTime);

            int minesCount = gameLogic.UnfoundMinesCount;
            minesCountImage.Image = GetBitmapNumericDisplay(minesCount);
        }
        
        private void RedrawMarkedMinesCount(int markMinesCount)
        {
            minesCountImage.Image = GetBitmapNumericDisplay(markMinesCount);
        }

        private void SetTimeOnDisplay(int currentTime)//TODO
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
     
        private void RestartDislays()
        {
            minesCountImage.Image = GetBitmapNumericDisplay(gameLogic.UnfoundMinesCount);
            smileButtonImage.Image = bitmapsResources.smileButton;
        }
    }
}
