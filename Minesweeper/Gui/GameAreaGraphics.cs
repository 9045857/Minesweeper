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
        List<Cell> cellsNearRightLeftMouseButtons = new List<Cell>();

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
            gameAreaPictureBox.MouseDown += new MouseEventHandler(CellPictureBox_MouseDown);

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

            using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
            {

                int rowIndex = GetMouseEventCellRowIndex(e);
                int columnIndex = GetMouseEventCellColumnIndex(e);

                if (isMouseLeftButtonDown && isMouseRightButtonDown)
                {
                    SetMouseButtonsDownFalse();
                    PressCellsNearRightLeftMouseButtonsUp(currentGameAreaGraphics, gameLogic.cells[rowIndex, columnIndex]);

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

                gameAreaPictureBox.Image = gameAreaImage;
            }
        }


        private void CellPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!gameLogic.IsGameContinue)
            {
                return;
            }

            using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
            {
                int rowIndex = GetMouseEventCellRowIndex(e);
                int columnIndex = GetMouseEventCellColumnIndex(e);

                Cell cell = gameLogic.cells[rowIndex, columnIndex];

                if (e.Button == MouseButtons.Left)
                {
                    if (cell.IsPressed && cell.MineNearCount != 0)
                    {
                        isMouseLeftButtonDown = true;

                        if (isMouseRightButtonDown)
                        {
                            //DrawSmileButtonIfCellDown();
                            PressCellsNearRightLeftMouseButtonsDown(currentGameAreaGraphics, cell);
                        }
                    }
                    else if (!cell.IsPressed && cell.markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        //DrawSmileButtonIfCellDown();
                        //DrawOnBottomCellAfterMouseDown(sender as CellDraw, cell);
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    if (cell.IsPressed && cell.MineNearCount != 0)
                    {
                        isMouseRightButtonDown = true;

                        if (isMouseLeftButtonDown)
                        {
                            //DrawSmileButtonIfCellDown();
                            PressCellsNearRightLeftMouseButtonsDown(currentGameAreaGraphics, cell);
                        }
                    }
                    else if (!cell.IsPressed)
                    {
                        gameLogic.Mark(cell);

                        Bitmap bitmap = GetMarkCell(cell);
                        DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmap);


                        //DrawRemainingMinesCountAfterMarkOnDispley();
                    }


                }

                gameAreaPictureBox.Image = gameAreaImage;
            }
        }


        private void PressCellsNearRightLeftMouseButtonsUp(Graphics currentGameAreaGraphics, Cell cell)
        {
            if (cellsNearRightLeftMouseButtons.Count != 0 && gameLogic.GetMarkedMinesNearCell(cell) == cell.MineNearCount)
            {
                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    List<Cell> pressingCells = gameLogic.GetOpenCellsAfterPress(rowIndex, columnIndex);

                    DrawCellsListAfterPress(pressingCells);
                    // PressCellsList(pressingCells);
                }

                //SetRemainigMinesCountIfGameOver();

                cellsNearRightLeftMouseButtons.Clear();
            }
            else if (cellsNearRightLeftMouseButtons.Count != 0)
            {
                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    Bitmap bitmap = GetMarkCell(element);
                    DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmap);
                }

                cellsNearRightLeftMouseButtons.Clear();
            }
        }


        private void PressCellsNearRightLeftMouseButtonsDown(Graphics currentGameAreaGraphics, Cell cell)
        {
            int rowIndex = cell.RowIndex;
            int columnIndex = cell.ColIndex;

            int indentFromInnerCell = 1;
            int borderCorrection = 1;

            int starRowIndex = rowIndex - indentFromInnerCell < 0 ? 0 : rowIndex - indentFromInnerCell;
            int endRowIndex = rowIndex + indentFromInnerCell == rowCount ? rowCount - borderCorrection : rowIndex + indentFromInnerCell;

            int starColIndex = columnIndex - indentFromInnerCell < 0 ? 0 : columnIndex - indentFromInnerCell;
            int endColIndex = columnIndex + indentFromInnerCell == columnCount ? columnCount - borderCorrection : columnIndex + indentFromInnerCell;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColIndex; j++)
                {
                    if (!gameLogic.cells[i, j].IsPressed && gameLogic.cells[i, j].markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        cellsNearRightLeftMouseButtons.Add(gameLogic.cells[i, j]);

                        if (gameLogic.cells[i, j].markOnTop == Cell.MarkOnTopCell.Question)
                        {
                            DrawCell(currentGameAreaGraphics, i, j, bitmapsResources.questionPressCell);
                        }
                        else
                        {
                            DrawCell(currentGameAreaGraphics, i, j, bitmapsResources.minesNearCount[0]);
                        }
                    }
                }
            }
        }




        private Bitmap GetMarkCell(Cell cell)
        {
            switch (cell.markOnTop)
            {
                case Cell.MarkOnTopCell.Flag:
                    return bitmapsResources.flag;

                case Cell.MarkOnTopCell.Question:
                    return bitmapsResources.question;

                case Cell.MarkOnTopCell.Empty:
                    return bitmapsResources.cellStart;

                default:
                    return bitmapsResources.cellStart;
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

                            DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmap);
                        }
                        else
                        {
                            switch (element.markOnTop)
                            {
                                case Cell.MarkOnTopCell.Flag:
                                    Bitmap bitmap = bitmapsResources.flag;
                                    DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmap);
                                    break;
                            }
                        }
                    }
                    gameAreaPictureBox.Image = gameAreaImage;
                }
            }
        }

        private void DrawCell(Graphics currentGameAreaGraphics, int rowIndex, int columnIndex, Bitmap bitmap)
        {
            int xRight = columnIndex * cellSideLength;
            int yTop = rowIndex * cellSideLength;

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
