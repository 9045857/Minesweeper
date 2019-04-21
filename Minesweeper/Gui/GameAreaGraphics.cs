using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Minesweeper.Logic;

namespace Minesweeper.Gui
{
    class GameAreaGraphics
    {
        private PictureBox gameAreaPictureBox;
        private Bitmap gameAreaImage;
        private Color backFormColor;

        private readonly BitmapsResources bitmapsResources = new BitmapsResources();
        private readonly int cellSideLength;

        GameLogic gameLogic;

        private int rowCount;
        private int columnCount;
        private int minesCount;

        private bool isMouseLeftButtonDown;
        private bool isMouseRightButtonDown;

        public GameAreaGraphics(PictureBox gameAreaPictureBox, GameLogic gameLogic)
        {
            rowCount = gameLogic.RowCount;
            columnCount = gameLogic.ColumnCount;
            minesCount = gameLogic.MinesCount;

            this.gameLogic = gameLogic;
            this.gameAreaPictureBox = gameAreaPictureBox;
            backFormColor = gameAreaPictureBox.Parent.BackColor;

            cellSideLength = bitmapsResources.cellStart.Height;

            CreateGameAreaPictureBox();

            
        }

        private void CreateGameAreaPictureBox()
        {
            DrawStartGamePanel();

            gameAreaPictureBox.MouseUp += new MouseEventHandler(GameAreaPictureBox_MouseUp);


        }


        private void SetMouseButtonsDownFalse()
        {
            isMouseLeftButtonDown = false;
            isMouseRightButtonDown = false;
        }



        private void GameAreaPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!gameLogic.IsGameContinue)
            {
                return;
            }

            int rowIndex = GetMouseEventCellRowIndex(e);
            int columnIndex = GetMouseEventCellColumnIndex(e);

            if (isMouseLeftButtonDown && isMouseRightButtonDown)
            {
                SetMouseButtonsDownFalse();
                //PressCellsNearRightLeftMouseButtonsUp(gameLogic.cells[rowIndex, columnIndex]);

                //SetTimerFalseIfGameFinish();
                //DrawSmileButtonIfCellUp();
            }
            else if (e.Button == MouseButtons.Left)
            {
                SetMouseButtonsDownFalse();

                if (gameLogic.cells[rowIndex, columnIndex].markOnTop != Cell.MarkOnTopCell.Flag)
                {
                    //SetTimerTrueIfGameBegin();

                    List<Cell> pressingCells = gameLogic.GetOpenCellsAfterPress(rowIndex, columnIndex);
                    DrawCellsListAfterPress(pressingCells);

                    // SetTimerFalseIfGameFinish();
                    // SetRemainigMinesCountIfGameOver();

                    // DrawSmileButtonIfCellUp();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                SetMouseButtonsDownFalse();
                //SetTimerFalseIfGameFinish();
                //DrawSmileButtonIfCellUp();
            }
        }




       




        private void DrawCellsListAfterPress(List<Cell> cellsList)//TODO переименовать
        {
            if (cellsList.Count != 0)
            {
                using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
                {
                    foreach (Cell element in cellsList)
                    {
                        int rowIndex = element.RowIndex;
                        int columnIndex = element.ColIndex;

                        int xRight = columnIndex * cellSideLength;
                        int yTop = rowIndex * cellSideLength;

                        if (element.IsPressed)
                        {
                            Bitmap bitmap = new Bitmap(cellSideLength, cellSideLength);

                            switch (element.markOnBottom)
                            {
                                case Cell.MarkOnBottomCell.Mine:
                                    bitmap = bitmapsResources.mine;
                                    break;

                                case Cell.MarkOnBottomCell.MineBombed:
                                    bitmap = bitmapsResources.mineBombed;
                                    break;

                                case Cell.MarkOnBottomCell.MineError:
                                    bitmap = bitmapsResources.mineError;
                                    break;

                                case Cell.MarkOnBottomCell.MineNearCount:
                                    int minesCountBitmapIndex = element.MineNearCount;
                                    bitmap = bitmapsResources.minesNearCount[minesCountBitmapIndex];
                                    break;
                                default:
                                    bitmap = bitmapsResources.mineError;
                                    break;
                            }

                            DrawCell(currentGameAreaGraphics, xRight, yTop, bitmap);
                        }
                        else
                        {
                            switch (element.markOnTop)
                            {
                                case Cell.MarkOnTopCell.Flag:
                                    Bitmap bitmap = bitmapsResources.flag;
                                    DrawCell(currentGameAreaGraphics, xRight, yTop, bitmap);
                                    break;
                            }
                        }
                    }
                    gameAreaPictureBox.Image = gameAreaImage;
                }
            }
        }

        private void DrawCell(Graphics currentGameAreaGraphics, int xRight, int yTop, Bitmap bitmap)
        {
            SolidBrush backFormColor = new SolidBrush(this.backFormColor);
            currentGameAreaGraphics.FillRectangle(backFormColor, xRight, yTop, cellSideLength, cellSideLength);
            currentGameAreaGraphics.DrawImage(bitmap, xRight, yTop, cellSideLength, cellSideLength);
            backFormColor.Dispose();
        }

        private void DrawStartGamePanel()
        {
            Bitmap cellStart = bitmapsResources.cellStart;
            int length = cellSideLength;

            int panelHeight = rowCount * length;
            int panelWidth = columnCount * length;

            //создаем и заполняем стартовую картинку для всего поля
            Bitmap currentGameAreaBitmap = new Bitmap(panelWidth, panelHeight);

            using (Graphics currentGameAreaGraphics = Graphics.FromImage(currentGameAreaBitmap))
            {
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        int xRight = j * length;
                        int yTop = i * length;

                        currentGameAreaGraphics.DrawImage(cellStart, xRight, yTop);
                    }
                }
            }

            int onePixel = 1;

            int additionToPanelWidthForGoodDesign = 28;
            int additionToPanelHeightForGoodDesign = 127;

            Panel gamePanel = gameAreaPictureBox.Parent as Panel;

            gamePanel.Parent.Width = panelWidth + additionToPanelWidthForGoodDesign;
            gamePanel.Parent.Height = panelHeight + additionToPanelHeightForGoodDesign;

            gamePanel.Height = panelHeight + gamePanel.Margin.Left + onePixel;
            gamePanel.Width = panelWidth + gamePanel.Margin.Top + onePixel;

            gameAreaPictureBox.Size = new Size(panelWidth, panelHeight);

            gameAreaPictureBox.Image = currentGameAreaBitmap;
            gameAreaImage = currentGameAreaBitmap;
        }

        private int GetMouseEventCellRowIndex(MouseEventArgs e)
        {           
                return e.Y / cellSideLength;           
        }

        private int GetMouseEventCellColumnIndex(MouseEventArgs e)
        {
            return e.X / cellSideLength;
        }

       

    }
}
