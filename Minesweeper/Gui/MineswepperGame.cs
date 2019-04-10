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
    class MineswepperGame
    {
        private readonly BitmapsResources bitmapsResources = new BitmapsResources();

        private int rowCount;
        private int columnCount;
        private int minesCount;

        private bool isMinesSetDid;

        private PictureBox[,] cellsPictures;

        private PictureBox smileButtonImage;
        private PictureBox timeImage;
        private PictureBox minesCountImage;

        private int panelsWidth;

        public MineswepperGame(int rowCount, int columnCount, int minesCount)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.minesCount = minesCount;

            isMinesSetDid = false;
        }

        private void DrawStartGamePanel(Panel gamePanel)
        {
            cellsPictures = new PictureBox[rowCount, columnCount];

            Bitmap cellStart = bitmapsResources.cellStart;
            int length = cellStart.Height;

            int panelHeight = rowCount * length;
            int panelWidth = columnCount * length;

            int onePixel = 1;

            int additionToPanelWidthForGoodDesign = 28;
            int additionToPanelHeightForGoodDesign = 127;

            gamePanel.Parent.Width = panelWidth + additionToPanelWidthForGoodDesign;
            gamePanel.Parent.Height = panelHeight + additionToPanelHeightForGoodDesign;

            gamePanel.Height = panelHeight + gamePanel.Margin.Left + onePixel;
            gamePanel.Width = panelWidth + gamePanel.Margin.Top + onePixel;

            this.panelsWidth = gamePanel.Width;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    int xRight = j * length;
                    int yTop = i * length;

                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Parent = gamePanel;
                    pictureBox.Location = new Point(xRight, yTop);
                    pictureBox.Height = length;
                    pictureBox.Width = length;
                    pictureBox.Name = "cellPictureBox_" + i + "_" + j;
                    pictureBox.Image = cellStart;

                    //TODO прописать события для стартовых иконок
                    pictureBox.MouseMove += new MouseEventHandler(CellPictureBox_MouseMove);
                    //pictureBox.MouseClick += new MouseEventHandler(CellPictureBox_MouseClick);
                    //pictureBox.MouseLeave += new EventHandler(CellPictureBox_MouseLeave);

                    pictureBox.MouseUp += new MouseEventHandler(CellPictureBox_MouseUp);

                    pictureBox.MouseDown += new MouseEventHandler(CellPictureBox_MouseDown);

                    cellsPictures[i, j] = pictureBox;
                }
            }
        }



        private void CellPictureBox_MouseUp(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = bitmapsResources.cellStart;

            // TODO нужно занести сюда кнопки и часы pictureBox2.Image = WindowsFormsApp1.Properties.Resources.smileButton31;


        }



        private void CellPictureBox_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = bitmapsResources.cellStart;

            // TODO нужно занести сюда кнопки и часы pictureBox2.Image = WindowsFormsApp1.Properties.Resources.smileButton31;


        }

        private void CellPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            //if ((sender as PictureBox).Image == bitmapsResources.cellStart && e.Button == MouseButtons.Left)
            //{
            //    (sender as PictureBox).Image = bitmapsResources.minesNearCount[0];
            //    smileButtonImage.Image = bitmapsResources.smileButtonAttention;
            //}


            // TODO нужно занести сюда кнопки и часы pictureBox2.Image = WindowsFormsApp1.Properties.Resources.smileButton31;


        }

        private void CellPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //if ((sender as PictureBox).Image == bitmapsResources.cellStart && e.LeftButton == MouseButtonState.Pressed)
            //{
            //    (sender as PictureBox).Image = bitmapsResources.minesNearCount[0];
            //    smileButtonImage.Image = bitmapsResources.smileButtonAttention;
            //}


            // TODO сделать что бы при нажатой левой клавише был эфект притопляемости

        }

        private Bitmap GetBitmapNumericDisplay(int number)//число не должно превышать 999
        {
            int hundred = 100;
            int ten = 10;
            int hundredRank = number / hundred;
            int tenRank = (number - hundredRank * hundred) / ten;
            int unitRank = number - hundredRank * hundred - tenRank * ten;

            int numbersCount = 3;
            int numberWidth = bitmapsResources.numbers[0].Width;
            int numberHeight = bitmapsResources.numbers[0].Height;

            int bitmapWidth = numbersCount * numberWidth;
            int bitmapHeight = numberHeight;

            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Bitmap bitmapHandredRank = bitmapsResources.numbers[hundredRank];
                Bitmap bitmapTenRank = bitmapsResources.numbers[tenRank];
                Bitmap bitmapUnitRank = bitmapsResources.numbers[unitRank];

                graphics.DrawImage(bitmapHandredRank, new Rectangle(0, 0, numberWidth, numberHeight));
                graphics.DrawImage(bitmapTenRank, new Rectangle(numberWidth, 0, numberWidth, numberHeight));
                graphics.DrawImage(bitmapUnitRank, new Rectangle(numberWidth + numberWidth, 0, numberWidth, numberHeight));
            }

            return bitmap;
        }

        private void DrawInfoPanel(Panel infoPanel, PictureBox smileButtonImage, PictureBox minesCountImage, PictureBox timeImage)
        {
            infoPanel.Width = panelsWidth;

            this.smileButtonImage = smileButtonImage;
            this.timeImage = timeImage;
            this.minesCountImage = minesCountImage;

            this.minesCountImage.Image = GetBitmapNumericDisplay(minesCount);
        }

        public void DrawStartArea(Panel gamePanel, Panel infoPanel, PictureBox smileButtonImage, PictureBox minesCountImage, PictureBox timeImage)
        {
            DrawStartGamePanel(gamePanel);

            DrawInfoPanel(infoPanel, smileButtonImage, minesCountImage, timeImage);
        }
    }
}
