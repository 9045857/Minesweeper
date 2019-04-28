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
        public Color CellTopColor { get; set; }

        private readonly BitmapsResources bitmapsResources;
        private readonly int cellSideLength;

        GameLogic gameLogic;
        List<Cell> cellsNearRightLeftMouseButtons = new List<Cell>();

        private int rowCount;
        private int columnCount;
        private int minesCount;

        private bool isMouseLeftButtonDown;
        private bool isMouseRightButtonDown;
        private bool isSituationBothMouseButtonDown;

        private bool IsMouseLeftButtonDown
        {
            get
            {
                if (IsMouseOnGameArea(currentRowIndex, currentColumnIndex))
                {
                    return isMouseLeftButtonDown;
                }
                else
                {
                    isMouseLeftButtonDown = false;
                    isMouseRightButtonDown = false;
                    return false;
                }
            }
        }

        private bool IsMouseRightButtonDown
        {
            get
            {
                if (IsMouseOnGameArea(currentRowIndex, currentColumnIndex))
                {
                    return isMouseRightButtonDown;
                }
                else
                {
                    isMouseLeftButtonDown = false;
                    isMouseRightButtonDown = false;
                    return false;
                }
            }
        }

        private bool AreBothMouseButtonsDown
        {
            get
            {
                if (IsMouseOnGameArea(currentRowIndex, currentColumnIndex))
                {
                    if (isMouseLeftButtonDown && isMouseRightButtonDown)
                    {
                        isSituationBothMouseButtonDown = true;
                        return isSituationBothMouseButtonDown;
                    }
                    else
                    {
                        isSituationBothMouseButtonDown = false;
                        return isSituationBothMouseButtonDown;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public GameAreaGraphics(PictureBox gameAreaPictureBox, GameLogic gameLogic, BitmapsResources bitmapsResources)
        {
            this.bitmapsResources = bitmapsResources;

            this.gameLogic = gameLogic;
            gameLogic.OnBeginNewGame += new GameLogic.BeginNewGameHeadler(DrawNewGameAreaPictureBox);

            this.gameAreaPictureBox = gameAreaPictureBox;
            gameAreaPictureBox.MouseUp += new MouseEventHandler(GameAreaPictureBox_MouseUp);
            gameAreaPictureBox.MouseDown += new MouseEventHandler(GameAreaPictureBox_MouseDown);
            gameAreaPictureBox.MouseMove += new MouseEventHandler(GameAreaPictureBox_MouseMove);

            SetRowColumnMinesCount();

            CellTopColor = gameAreaPictureBox.Parent.BackColor;
            backFormColor = gameAreaPictureBox.Parent.BackColor;
            cellSideLength = bitmapsResources.cellStart.Height;

            DrawStartGamePanel();

            forTestMouse.Show();
        }

        private void SetRowColumnMinesCount()
        {
            rowCount = gameLogic.RowCount;
            columnCount = gameLogic.ColumnCount;
            minesCount = gameLogic.MinesCount;
        }

        private void DrawNewGameAreaPictureBox()
        {
            isSituationBothMouseButtonDown = false;
            isMouseLeftButtonDown = false;
            isMouseRightButtonDown = false;

            SetRowColumnMinesCount();
            DrawStartGamePanel();
        }

        private int GetCellRowOrColumnIndex(int coordinate, int gameAreaSideLength)
        {
            if (coordinate >= gameAreaSideLength || coordinate <= 0)
            {
                return -1;
            }
            else
            {
                return coordinate / cellSideLength;
            }
        }

        private int GetMouseEventCellColumnIndex(MouseEventArgs e)
        {
            if (e.X >= gameAreaPictureBox.Width || e.X <= 0)
            {
                return -1;
            }
            else
            {
                return e.X / cellSideLength;
            }
        }

        private void GameAreaPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!gameLogic.IsGameContinue)
            {
                return;
            }

            int rowIndex = GetCellRowOrColumnIndex(e.Y, gameAreaPictureBox.Height);
            int columnIndex = GetCellRowOrColumnIndex(e.X, gameAreaPictureBox.Width);

            if (e.Button == MouseButtons.Left)
            {
                if (IsMouseOnGameArea(rowIndex, columnIndex))
                {
                    using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
                    {
                        if (isMouseLeftButtonDown && isMouseRightButtonDown)
                        {
                            OnMouseUpCells?.Invoke();

                            PressCellsNearRightLeftMouseButtonsUp(currentGameAreaGraphics, gameLogic.cells[rowIndex, columnIndex]);
                            isMouseLeftButtonDown = false;

                            gameAreaPictureBox.Image = gameAreaImage;
                            return;
                        }
                        else if (isSituationBothMouseButtonDown)
                        {
                            OnMouseUpCells?.Invoke();

                            isSituationBothMouseButtonDown = false;
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
                }
                else
                {
                    PressUpAfterMouseGoOutGameArea();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (IsMouseOnGameArea(rowIndex, columnIndex))
                {
                    using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
                    {
                        if (isMouseLeftButtonDown && isMouseRightButtonDown)
                        {
                            PressCellsNearRightLeftMouseButtonsUp(currentGameAreaGraphics, gameLogic.cells[rowIndex, columnIndex]);
                            isMouseRightButtonDown = false;

                            gameAreaPictureBox.Image = gameAreaImage;

                            return;
                        }
                        else if (isSituationBothMouseButtonDown)
                        {
                            isSituationBothMouseButtonDown = false;
                            isMouseRightButtonDown = false;
                            return;
                        }

                        isMouseRightButtonDown = false;

                        gameAreaPictureBox.Image = gameAreaImage;
                    }
                }
                else
                {
                    PressUpAfterMouseGoOutGameArea();
                }
            }
        }

        private void PressUpAfterMouseGoOutGameArea()
        {
            using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
            {
                PressCellsNearRightLeftMouseButtonsUp(currentGameAreaGraphics, gameLogic.cells[currentRowIndex, currentColumnIndex]);
                gameAreaPictureBox.Image = gameAreaImage;

                isSituationBothMouseButtonDown = false;
            }
        }

        /// <summary>
        /// Метод проверяет "нажаты ли обе клавиши мыши", заполняет массив ячеек для нажатия, отрисовывает нажатый массив.
        /// </summary>
        /// <param name="currentGameAreaGraphics"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
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
                        DrawCell(currentGameAreaGraphics, backFormColor, rowIndex, columnIndex, bitmapsResources.questionPressCell);
                    }
                    else
                    {
                        DrawCell(currentGameAreaGraphics, backFormColor, rowIndex, columnIndex, bitmapsResources.minesNearCount[0]);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Текущий индекс строки. Ему задают значение -1, если поле(параметр) определялось вне игрового поля.
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        ///  Текущий индекс. Ему задают значение -1, если поле(параметр) определялось вне игрового поля.
        /// </summary>
        private int currentColumnIndex;

        /// <summary>
        /// Свойство определяющие находятся ли текущие индексы в пределях игрового поля.
        /// </summary>
        private bool IsMouseOnGameArea(int rowIndex, int columnIndex)
        {
            return rowIndex != -1 && columnIndex != -1;
        }

        private bool IsChangedCell(int newRowIndex, int newColumnIndex)
        {
            //int rowIndex = GetCellRowOrColumnIndex(e.Y, gameAreaPictureBox.Height);
            //int columnIndex = GetCellRowOrColumnIndex(e.X, gameAreaPictureBox.Width);

            return currentColumnIndex != newColumnIndex || newRowIndex != currentRowIndex;
        }


        private int changesCellCount = 0;
        private bool isMouseMoveAndChandeCell;

        private void GameAreaPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            int rowIndexTest = GetCellRowOrColumnIndex(e.Y, (sender as PictureBox).Height);
            int columnIndexTest = GetCellRowOrColumnIndex(e.X, (sender as PictureBox).Width);

            forTestMouse.labelCell.Text = rowIndexTest + " : " + columnIndexTest;
            forTestMouse.labelCoordinate.Text = e.Y + " : " + e.X;


            forTestMouse.label2.Text = currentRowIndex + " : " + currentColumnIndex;



            if (isSituationBothMouseButtonDown && IsMouseOnGameArea(currentRowIndex, currentColumnIndex))
            {

                int rowIndex = GetCellRowOrColumnIndex(e.Y, (sender as PictureBox).Height);
                int columnIndex = GetCellRowOrColumnIndex(e.X, (sender as PictureBox).Width);

                if (IsMouseOnGameArea(rowIndex, columnIndex) && IsChangedCell(rowIndex, columnIndex))
                {
                    changesCellCount++;
                    forTestMouse.label4.Text = changesCellCount.ToString();

                    using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
                    {
                        Cell cell = gameLogic.cells[currentRowIndex, currentColumnIndex];//TODO вот тут ввести положение следующая ячейка

                        isMouseMoveAndChandeCell = true;
                        PressCellsNearRightLeftMouseButtonsUp(currentGameAreaGraphics, cell);
                        isMouseMoveAndChandeCell = false;

                        currentRowIndex = rowIndex;
                        currentColumnIndex = columnIndex;

                        cell = gameLogic.cells[currentRowIndex, currentColumnIndex];

                        IsMouseLeftRightButtonDownThenPressAreaNearCell(currentGameAreaGraphics, cell);

                        gameAreaPictureBox.Image = gameAreaImage;
                    }
                }
            }
            else if (isMouseLeftButtonDown && IsMouseOnGameArea(currentRowIndex, currentColumnIndex))//TODO сделать для нажатия левой кнопкой
            {
                int rowIndex = GetCellRowOrColumnIndex(e.Y, (sender as PictureBox).Height);
                int columnIndex = GetCellRowOrColumnIndex(e.X, (sender as PictureBox).Width);

                if (IsMouseOnGameArea(rowIndex, columnIndex) && IsChangedCell(rowIndex, columnIndex))
                {
                    using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
                    {
                        //Cell cell = gameLogic.cells[currentRowIndex, currentColumnIndex];
                        //PressCellsNearRightLeftMouseButtonsUp(currentGameAreaGraphics, cell);

                        currentRowIndex = rowIndex;
                        currentColumnIndex = columnIndex;

                        //cell = gameLogic.cells[currentRowIndex, currentColumnIndex];
                        //PressCellsNearRightLeftMouseButtonsDown(currentGameAreaGraphics, cell);

                        gameAreaPictureBox.Image = gameAreaImage;
                    }
                }
            }
        }

        private void GameAreaPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!gameLogic.IsGameContinue)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
                {
                    int rowIndex = GetCellRowOrColumnIndex(e.Y, (sender as PictureBox).Height);
                    int columnIndex = GetCellRowOrColumnIndex(e.X, (sender as PictureBox).Width);

                    currentRowIndex = rowIndex;
                    currentColumnIndex = columnIndex;

                    isMouseLeftButtonDown = true;

                    Cell cell = gameLogic.cells[rowIndex, columnIndex];

                    if (IsMouseLeftRightButtonDownThenPressAreaNearCell(currentGameAreaGraphics, cell))
                    {
                        OnMouseDownCells?.Invoke();
                        isSituationBothMouseButtonDown = true;
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
                    gameAreaPictureBox.Image = gameAreaImage;
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                using (Graphics currentGameAreaGraphics = Graphics.FromImage(gameAreaImage))
                {
                    isMouseRightButtonDown = true;

                    int rowIndex = GetCellRowOrColumnIndex(e.Y, (sender as PictureBox).Height);
                    int columnIndex = GetCellRowOrColumnIndex(e.X, (sender as PictureBox).Width);

                    Cell cell = gameLogic.cells[rowIndex, columnIndex];



                    if (IsMouseLeftRightButtonDownThenPressAreaNearCell(currentGameAreaGraphics, cell))
                    {
                        isSituationBothMouseButtonDown = true;
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
                            DrawCell(currentGameAreaGraphics, CellTopColor, rowIndex, columnIndex, bitmap);
                        }
                    }
                    gameAreaPictureBox.Image = gameAreaImage;
                }
            }
        }

        private void DrawOnBottomCellAfterMouseDown(Graphics currentGameAreaGraphics, Cell cell)
        {
            int rowIndex = cell.RowIndex;
            int columnIndex = cell.ColIndex;

            switch (cell.markOnTop)
            {
                case Cell.MarkOnTopCell.Question:
                    DrawCell(currentGameAreaGraphics, backFormColor, rowIndex, columnIndex, bitmapsResources.questionPressCell);
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

        /// <summary>
        /// Метод "возвращает" нажатые(проверенные) ячейки рядом с нажатой двумя клавившами в исходное пложение.
        /// 
        /// </summary>
        /// <param name="currentGameAreaGraphics"></param>
        /// <param name="cell"></param>
        private void PressCellsNearRightLeftMouseButtonsUp(Graphics currentGameAreaGraphics, Cell cell)
        {
            if (!cell.IsPressed)
            {
                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    Bitmap bitmap = GetMarkCell(gameLogic.cells[rowIndex, columnIndex]);

                    DrawCell(currentGameAreaGraphics, CellTopColor, rowIndex, columnIndex, bitmap);
                }
            }
            else if (isMouseMoveAndChandeCell && isSituationBothMouseButtonDown)//TODO   ввести еще одно положение ячеек - следующая ячейка и сравнивать их.
            {
                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    Bitmap bitmap = GetMarkCell(element);
                    DrawCell(currentGameAreaGraphics, CellTopColor, rowIndex, columnIndex, bitmap);
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
                    DrawCell(currentGameAreaGraphics, CellTopColor, rowIndex, columnIndex, bitmap);
                }

                cellsNearRightLeftMouseButtons.Clear();
            }
        }

        /// <summary>
        /// Метод Нажатия области доступных ячеек рядом с Нажатой двумя клавишами мышки ячейкой.
        /// Делает два действия:
        /// 1. заполняет массив доступных к нажатию ячеек
        /// 2. рисует нажатые ячеки с учетом их маркировки (вопрос или пустая) 
        /// </summary>
        /// <param name="currentGameAreaGraphics"></param>
        /// <param name="cell"></param>
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
                            DrawCell(currentGameAreaGraphics, backFormColor, i, j, bitmapsResources.questionPressCell);
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

                            DrawCell(currentGameAreaGraphics, backFormColor, rowIndex, columnIndex, bitmap);
                        }
                        else
                        {
                            switch (element.markOnTop)
                            {
                                case Cell.MarkOnTopCell.Flag:
                                    Bitmap bitmap = bitmapsResources.flag;
                                    DrawCell(currentGameAreaGraphics, CellTopColor, rowIndex, columnIndex, bitmap);
                                    break;
                            }
                        }
                    }
                    gameAreaPictureBox.Image = gameAreaImage;
                }
            }
        }

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
            int pixelsForCorrectionSize = 2;
            gamePanel.Height = panelHeight + gamePanel.Margin.Left + pixelsForCorrectionSize;
            gamePanel.Width = panelWidth + gamePanel.Margin.Top + pixelsForCorrectionSize;

            OnSetGamePanelWidth?.Invoke(gamePanel.Width);

            gameAreaPictureBox.Size = new Size(panelWidth, panelHeight);

            gameAreaPictureBox.Image = currentGameAreaBitmap;
            gameAreaImage = currentGameAreaBitmap;
        }


        private ForTestMouseMove forTestMouse = new ForTestMouseMove();



    }
}
