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
        public delegate void GetGamePanelWidth(int width);
        public event GetGamePanelWidth OnSetGamePanelWidth;

        public delegate void CheckCells();
        public event CheckCells OnMouseDownCells;
        public event CheckCells OnMouseUpCells;

        private PictureBox gameAreaPictureBox;
        private Bitmap gameAreaImage;
        private readonly Color backFormColor;

        private readonly BitmapsResources bitmapsResources;
        private readonly int cellSideLength;

        GameLogic gameLogic;
        List<Cell> cellsNearRightLeftMouseButtons = new List<Cell>();

        private int rowCount;
        private int columnCount;
        private int minesCount;

        private bool isMouseLeftButtonDown;
        private bool isMouseRightButtonDown;
        private bool areBothMouseButtonDownAction;

        public GameAreaGraphics(PictureBox gameAreaPictureBox, GameLogic gameLogic, BitmapsResources bitmapsResources)
        {
            this.bitmapsResources = bitmapsResources;

            this.gameLogic = gameLogic;
            gameLogic.OnBeginNewGame += new GameLogic.BeginNewGameHeadler(DrawNewGameAreaPictureBox);

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

        private void DrawNewGameAreaPictureBox()
        {
            areBothMouseButtonDownAction = false;
            isMouseLeftButtonDown = false;
            isMouseRightButtonDown = false;

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

                if (e.Button == MouseButtons.Left)
                {
                    if (AreBothMouseButtonsDown)
                    {
                        OnMouseUpCells?.Invoke();

                        PressCellsNearRightLeftMouseButtonsUp(currentGameAreaGraphics, gameLogic.cells[rowIndex, columnIndex]);
                        isMouseLeftButtonDown = false;

                        gameAreaPictureBox.Image = gameAreaImage;
                        return;
                    }
                    else if (areBothMouseButtonDownAction)
                    {
                        OnMouseUpCells?.Invoke();

                        areBothMouseButtonDownAction = false;
                        isMouseLeftButtonDown = false;
                        return;
                    }

                    isMouseLeftButtonDown = false;

                    if (gameLogic.cells[rowIndex, columnIndex].markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        OnMouseUpCells?.Invoke();

                        List<Cell> pressingCells = gameLogic.GetOpenCellsAfterPress(rowIndex, columnIndex);
                        DrawCellsListAfterPress(pressingCells);
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
                    }
                    else if (areBothMouseButtonDownAction)
                    {
                        areBothMouseButtonDownAction = false;
                        isMouseRightButtonDown = false;
                        return;
                    }

                    isMouseRightButtonDown = false;

                    gameAreaPictureBox.Image = gameAreaImage;
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
                        DrawCell(currentGameAreaGraphics,backFormColor, rowIndex, columnIndex, bitmapsResources.questionPressCell);
                    }
                    else
                    {
                        DrawCell(currentGameAreaGraphics, backFormColor,rowIndex, columnIndex, bitmapsResources.minesNearCount[0]);
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
                        OnMouseDownCells?.Invoke();
                        areBothMouseButtonDownAction = true;
                    }
                    else if (!cell.IsPressed && cell.markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        OnMouseDownCells?.Invoke();

                        if (isMouseRightButtonDown)
                        {
                            PressCellsNearRightLeftMouseButtonsDown(currentGameAreaGraphics, cell);
                        }
                        else
                        {
                            DrawOnBottomCellAfterMouseDown(currentGameAreaGraphics, cell);
                        }
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    isMouseRightButtonDown = true;

                    if (IsMouseLeftRightButtonDownThenPressAreaNearCell(currentGameAreaGraphics, cell))
                    {
                        areBothMouseButtonDownAction = true;
                    }
                    else if (!cell.IsPressed)
                    {
                        if (isMouseLeftButtonDown)
                        {
                            PressCellsNearRightLeftMouseButtonsDown(currentGameAreaGraphics, cell);
                        }
                        else
                        {
                            gameLogic.Mark(cell);

                            Bitmap bitmap = GetMarkCell(cell);
                            DrawCell(currentGameAreaGraphics, CellTopColor,rowIndex, columnIndex, bitmap);
                        }
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
                    DrawCell(currentGameAreaGraphics,backFormColor ,rowIndex, columnIndex, bitmapsResources.questionPressCell);
                    return;

                case Cell.MarkOnTopCell.Empty:
                    DrawCell(currentGameAreaGraphics, backFormColor, rowIndex, columnIndex, bitmapsResources.minesNearCount[0]);
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
                    DrawCell(currentGameAreaGraphics, CellTopColor, rowIndex, columnIndex, bitmapsResources.question);
                    return;

                case Cell.MarkOnTopCell.Empty:
                    DrawCell(currentGameAreaGraphics, CellTopColor, rowIndex, columnIndex, bitmapsResources.cellStart);
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

                    DrawCell(currentGameAreaGraphics,CellTopColor ,rowIndex, columnIndex, bitmap);
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

                cellsNearRightLeftMouseButtons.Clear();
            }
            else if (cellsNearRightLeftMouseButtons.Count != 0)
            {
                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    Bitmap bitmap = GetMarkCell(element);
                    DrawCell(currentGameAreaGraphics, CellTopColor,rowIndex, columnIndex, bitmap);
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
                            DrawCell(currentGameAreaGraphics,backFormColor ,i, j, bitmapsResources.questionPressCell);
                        }
                        else
                        {
                            DrawCell(currentGameAreaGraphics, backFormColor, i, j, bitmapsResources.minesNearCount[0]);
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

        private void DrawCellsListAfterPress(List<Cell> cellsList)
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

                            DrawCell(currentGameAreaGraphics,backFormColor, rowIndex, columnIndex, bitmap);
                        }
                        else
                        {
                            switch (element.markOnTop)
                            {
                                case Cell.MarkOnTopCell.Flag:
                                    Bitmap bitmap = bitmapsResources.flag;
                                    DrawCell(currentGameAreaGraphics,CellTopColor ,rowIndex, columnIndex, bitmap);
                                    break;
                            }
                        }
                    }
                    gameAreaPictureBox.Image = gameAreaImage;
                }
            }
        }

        public Color CellTopColor{get;set;}

        private void DrawCell(Graphics currentGameAreaGraphics, Color backColor, int rowIndex, int columnIndex, Bitmap bitmap)
        {
            int xRight = columnIndex * cellSideLength;
            int yTop = rowIndex * cellSideLength;

            SolidBrush backCellColor = new SolidBrush(backColor);

            currentGameAreaGraphics.FillRectangle(backCellColor, xRight, yTop, cellSideLength, cellSideLength);
            currentGameAreaGraphics.DrawImage(bitmap, xRight, yTop, cellSideLength, cellSideLength);
            backCellColor.Dispose();
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
                SolidBrush color = new SolidBrush(CellTopColor);
                currentGameAreaGraphics.FillRectangle(color, 0, 0, panelWidth, panelHeight);
                color.Dispose();

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

            Panel gamePanel = gameAreaPictureBox.Parent as Panel;
            int onePixel = 1;
            gamePanel.Height = panelHeight + gamePanel.Margin.Left + onePixel;
            gamePanel.Width = panelWidth + gamePanel.Margin.Top + onePixel;

            OnSetGamePanelWidth?.Invoke(gamePanel.Width);

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
