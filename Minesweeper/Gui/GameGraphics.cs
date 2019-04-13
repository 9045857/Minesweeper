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

        private int rowCount;
        private int columnCount;
        private int minesCount;

        private bool areMinesSet;//индикатор, что создана новая игра логического блока
        private GameLogic gameLogic;

        // private PictureBox[,] cellsPictures;
        private CellDraw[,] cells;

        private PictureBox smileButtonImage;
        private PictureBox timeImage;
        private PictureBox minesCountImage;

        private int panelsWidth;

        public GameGraphics(int rowCount, int columnCount, int minesCount)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.minesCount = minesCount;

            gameLogic = new GameLogic(rowCount, columnCount, minesCount);

            // areMinesSet = false;
        }

        private void DrawStartGamePanel(Panel gamePanel)
        {
            cells = new CellDraw[rowCount, columnCount];

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

                    CellDraw cellDraw = new CellDraw();

                    cellDraw.Parent = gamePanel;
                    cellDraw.Location = new Point(xRight, yTop);
                    cellDraw.Height = length;
                    cellDraw.Width = length;
                    cellDraw.Name = "CellPictureBox_" + i + "_" + j;// нужно ли вообще имя?
                    cellDraw.Image = cellStart;

                    cellDraw.rowIndex = i;
                    cellDraw.columnIndex = j;

                    //TODO прописать события для стартовых иконок
                    //cellDraw.MouseMove += new MouseEventHandler(CellPictureBox_MouseMove);
                    //cellDraw.MouseClick += new MouseEventHandler(CellcellDraw_MouseClick);

                    //cellDraw.MouseLeave += new EventHandler(CellPictureBox_MouseLeave);
                    cellDraw.MouseUp += new MouseEventHandler(CellPictureBox_MouseUp);
                    cellDraw.MouseDown += new MouseEventHandler(CellPictureBox_MouseDown);

                    cells[i, j] = cellDraw;
                }
            }
        }


        private void PressCellsList(List<Cell> cellsList)
        {
            if (cellsList.Count != 0)
            {
                foreach (Cell element in cellsList)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    int minesCountBitmapIndex = element.MineNearCount;

                    cells[rowIndex, columnIndex].Image = bitmapsResources.minesNearCount[minesCountBitmapIndex];
                }
            }
        }

        private void CellPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //isLeftMouseButtonDown = false;

                if (!gameLogic.AreMinesSet)
                {
                    int rowIndex = (sender as CellDraw).rowIndex;
                    int columnIndex = (sender as CellDraw).columnIndex;


                    //if (gameLogic.cells[rowIndex, columnIndex].markOnTop == Cell.MarkOnTopCell.Flag)
                    //{
                    //    return;
                    //}


                    List<Cell> pressingCells = gameLogic.GetOpenCellsNearPressed(rowIndex, columnIndex);//TODO 

                    PressCellsList(pressingCells);


                }
                //else
                //{
                //    //игра, проверка

                //}
            }

            // TODO самый важный метод нужно как-то передать в логику индексы нажатых кнопок и обратно получить список индексов для открытия

        }



        private void CellPictureBox_MouseLeave(object sender, EventArgs e)
        {
        }

        //private bool isLeftMouseButtonDown;

        private void CellPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //isLeftMouseButtonDown = true;

                int rowIndex = (sender as CellDraw).rowIndex;
                int columnIndex = (sender as CellDraw).columnIndex;

                Cell cell = gameLogic.cells[rowIndex, columnIndex];

                if (!cell.IsPressed && cell.markOnTop != Cell.MarkOnTopCell.Flag)
                {
                    (sender as CellDraw).Image = bitmapsResources.minesNearCount[0];//TODO вариант с вопросом нужно отдельно сделать
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                int rowIndex = (sender as CellDraw).rowIndex;
                int columnIndex = (sender as CellDraw).columnIndex;

                Cell cell = gameLogic.cells[rowIndex, columnIndex];

                cell.Mark();

                if (!cell.IsPressed)
                {
                    switch (cell.markOnTop)
                    {
                        case Cell.MarkOnTopCell.Flag:
                            (sender as CellDraw).Image = bitmapsResources.flag;
                            break;

                        case Cell.MarkOnTopCell.Question:
                            (sender as CellDraw).Image = bitmapsResources.question;
                            break;

                        case Cell.MarkOnTopCell.Empty:
                            (sender as CellDraw).Image = bitmapsResources.cellStart;
                            break;
                    }
                }
            }

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
