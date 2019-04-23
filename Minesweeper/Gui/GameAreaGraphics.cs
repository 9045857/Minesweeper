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
        private readonly Color backFormColor;

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
            this.gameLogic = gameLogic;
            gameLogic.BeginNewGame +=new GameLogic.BeginNewGameHeadler(DrawNewGameAreaPictureBox);

            this.gameAreaPictureBox = gameAreaPictureBox;
            gameAreaPictureBox.MouseUp += new MouseEventHandler(GameAreaPictureBox_MouseUp);
            gameAreaPictureBox.MouseDown += new MouseEventHandler(GameAreaPictureBox_MouseDown);

            SetRowColumnMinesCount();

            backFormColor = gameAreaPictureBox.Parent.BackColor;
            cellSideLength = bitmapsResources.cellStart.Height;

            DrawStartGamePanel();
        }

        private void SetRowColumnMinesCount()
        {
            rowCount = gameLogic.RowCount;
            columnCount = gameLogic.ColumnCount;
            minesCount = gameLogic.MinesCount;
        }

        private void DrawNewGameAreaPictureBox(object sender, EventArgs eventArgs)
        {
            SetRowColumnMinesCount();
            DrawStartGamePanel();
        }

        private bool AreBothMouseButtonsDown
        {
            get
            {
                return isMouseLeftButtonDown && isMouseRightButtonDown;
            }
        }

        private bool areBothMouseButtonDownAction;

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

                //    //SetTimerFalseIfGameFinish();//имеет отношение к дисплею стоит скорее всего не на своем месте
                //    //DrawSmileButtonIfCellUp();//имеет отношение к дисплею стоит скорее всего не на своем месте

               if (e.Button == MouseButtons.Left)
                {
                    if (AreBothMouseButtonsDown)
                    {
                        PressCellsNearRightLeftMouseButtonsUp(currentGameAreaGraphics, gameLogic.cells[rowIndex, columnIndex]);
                        isMouseLeftButtonDown = false;

                        gameAreaPictureBox.Image = gameAreaImage;

                        return;

                        //SetTimerFalseIfGameFinish();//имеет отношение к дисплею
                        //DrawSmileButtonIfCellUp();//имеет отношение к дисплею
                    }
                    else if (areBothMouseButtonDownAction)
                    {
                        areBothMouseButtonDownAction = false;
                        isMouseLeftButtonDown = false;
                        return;
                    }

                    isMouseLeftButtonDown = false;

                    if (gameLogic.cells[rowIndex, columnIndex].markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        //SetTimerTrueIfGameBegin();//имеет отношение к дисплею

                        List<Cell> pressingCells = gameLogic.GetOpenCellsAfterPress(rowIndex, columnIndex);
                        DrawCellsListAfterPress(pressingCells);

                        // SetTimerFalseIfGameFinish();//имеет отношение к дисплею
                        // SetRemainigMinesCountIfGameOver();//имеет отношение к дисплею

                        // DrawSmileButtonIfCellUp();//имеет отношение к дисплею
                    }

                    gameAreaPictureBox.Image = gameAreaImage;
                }
                else if (e.Button == MouseButtons.Right)
                {

                    if (AreBothMouseButtonsDown)
                    {
                        PressCellsNearRightLeftMouseButtonsUp(currentGameAreaGraphics, gameLogic.cells[rowIndex, columnIndex]);
                        isMouseRightButtonDown = false;

                        gameAreaPictureBox.Image = gameAreaImage;

                        return;

                        //SetTimerFalseIfGameFinish();//имеет отношение к дисплею
                        //DrawSmileButtonIfCellUp();//имеет отношение к дисплею
                    }
                    else if (areBothMouseButtonDownAction)
                    {
                        areBothMouseButtonDownAction = false;
                        isMouseRightButtonDown = false;
                        return;
                    }

                    isMouseRightButtonDown = false;

                    gameAreaPictureBox.Image = gameAreaImage;

                    //SetTimerFalseIfGameFinish();//имеет отношение к дисплею
                    //DrawSmileButtonIfCellUp();//имеет отношение к дисплею
                }
            }
        }

        private bool IsMouseLeftRightButtonDownThenPressAreaNearCell(Graphics currentGameAreaGraphics, Cell cell)
        {
            if (isMouseLeftButtonDown && isMouseRightButtonDown)
            {
                cellsNearRightLeftMouseButtons = gameLogic.GetCellsListAvailableForPress(cell);

                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    if (element.markOnTop == Cell.MarkOnTopCell.Question)
                    {
                        DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmapsResources.questionPressCell);
                    }
                    else
                    {
                        DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmapsResources.minesNearCount[0]);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }


        private void GameAreaPictureBox_MouseDown(object sender, MouseEventArgs e)
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
                    isMouseLeftButtonDown = true;

                    if (IsMouseLeftRightButtonDownThenPressAreaNearCell(currentGameAreaGraphics, cell))
                    {
                        areBothMouseButtonDownAction = true;
                        //   return;
                    }
                    else if (!cell.IsPressed && cell.markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        if (isMouseRightButtonDown)
                        {
                            //DrawSmileButtonIfCellDown();//имеет отношение к дисплею
                            PressCellsNearRightLeftMouseButtonsDown(currentGameAreaGraphics, cell);
                        }
                        else
                        {
                            //DrawSmileButtonIfCellDown();//имеет отношение к дисплею
                            DrawOnBottomCellAfterMouseDown(currentGameAreaGraphics, cell);//TODO
                        }
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    isMouseRightButtonDown = true;

                    if (IsMouseLeftRightButtonDownThenPressAreaNearCell(currentGameAreaGraphics, cell))
                    {
                        areBothMouseButtonDownAction = true;
                        // return;
                    }
                    else if (!cell.IsPressed)
                    {
                        if (isMouseLeftButtonDown)
                        {
                            //DrawSmileButtonIfCellDown();//имеет отношение к дисплею
                            PressCellsNearRightLeftMouseButtonsDown(currentGameAreaGraphics, cell);
                        }
                        else
                        {

                            gameLogic.Mark(cell);

                            Bitmap bitmap = GetMarkCell(cell);
                            DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmap);
                        }

                        //DrawRemainingMinesCountAfterMarkOnDispley();//имеет отношение к дисплею
                    }
                }

                gameAreaPictureBox.Image = gameAreaImage;
            }
        }

        private void DrawOnBottomCellAfterMouseDown(Graphics currentGameAreaGraphics, Cell cell)
        {
            int rowIndex = cell.RowIndex;
            int columnIndex = cell.ColIndex;

            switch (cell.markOnTop)
            {
                case Cell.MarkOnTopCell.Question:
                    DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmapsResources.questionPressCell);
                    return;

                case Cell.MarkOnTopCell.Empty:
                    DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmapsResources.minesNearCount[0]);
                    return;
            }
        }

        private void DrawOnTopCellAfterMouseUp(Graphics currentGameAreaGraphics, Cell cell)
        {
            int rowIndex = cell.RowIndex;
            int columnIndex = cell.ColIndex;

            switch (cell.markOnTop)
            {
                case Cell.MarkOnTopCell.Question:
                    DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmapsResources.question);
                    return;

                case Cell.MarkOnTopCell.Empty:
                    DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmapsResources.cellStart);
                    return;
            }
        }

        private void PressCellsNearRightLeftMouseButtonsUp(Graphics currentGameAreaGraphics, Cell cell)
        {
            if (!cell.IsPressed)
            {
                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    Bitmap bitmap = GetMarkCell(gameLogic.cells[rowIndex, columnIndex]);

                    DrawCell(currentGameAreaGraphics, rowIndex, columnIndex, bitmap);
                }
            }
            else if (cellsNearRightLeftMouseButtons.Count != 0 && gameLogic.GetMarkedMinesNearCell(cell) == cell.MineNearCount)
            {
                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    List<Cell> pressingCells = gameLogic.GetOpenCellsAfterPress(rowIndex, columnIndex);

                    DrawCellsListAfterPress(pressingCells);
                }

                //SetRemainigMinesCountIfGameOver();//имеет отношение к дисплею

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
            gameLogic.GetAvailableIndexesNearCell(cell, out int starRowIndex, out int endRowIndex, out int starColumnIndex, out int endColumnIndex);

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColumnIndex; j <= endColumnIndex; j++)
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
