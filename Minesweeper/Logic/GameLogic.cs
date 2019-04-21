using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper.Logic
{
    class GameLogic
    {
        public Cell[,] cells;

        public  int RowCount { get; private set; }
        public  int ColumnCount { get; private set; }

        public int MinesCount { get; private set; }

        public int MarkedMinesCount { get; private set; }
        public int FoundMinesCount { get; private set; }

        private int pressedCellsCount;

        private int PressedCellsAndFoundMinesCount
        {
            get
            {
                return pressedCellsCount + FoundMinesCount;
            }
        }

        public bool AreMinesSet { get; private set; }

        public bool isDontExploded;
        private bool isNotFinishGame;

        public bool IsGameContinue
        {
            get
            {
                return RowCount * ColumnCount != PressedCellsAndFoundMinesCount && isDontExploded&& isNotFinishGame;
            }
        }

        public GameLogic(int rowCount, int columnCount, int minesCount)
        {
            this.RowCount = rowCount;
            this.ColumnCount = columnCount;

            cells = CreateCells(rowCount, columnCount);

            this.MinesCount = minesCount;

            SetBeginConditions();
        }

        private void SetBeginConditions()
        {
            AreMinesSet = false;
            isDontExploded = true;
            isNotFinishGame = true;

            MarkedMinesCount = 0;
            FoundMinesCount = 0;

            pressedCellsCount = 0;
        }

        public void RestartGame()
        {
            SetBeginConditions();

            for (int i=0;i<RowCount;i++)
            {
                for (int j=0;j<ColumnCount;j++)
                {
                    cells[i, j].SetBeginConditions();
                }
            }
        }

        public void RestartGame(int rowCount, int columnCount, int minesCount)
        {
            SetBeginConditions();

            this.RowCount = rowCount;
            this.ColumnCount = columnCount;

            this.MinesCount = minesCount;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    cells[i, j].SetBeginConditions();
                }
            }
        }


        public void FinishGame()
        {
            isNotFinishGame = false;
        }

        private Cell[,] CreateCells(int rowCount, int columnCount)
        {
            Cell[,] cells = new Cell[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    cells[i, j] = new Cell(i, j);
                }
            }

            return cells;
        }

        public void Mark(Cell cell)
        {
            if (!IsGameContinue)
            {
                return;
            }

            if (!cell.IsPressed)
            {
                switch (cell.markOnTop)
                {
                    case Cell.MarkOnTopCell.Empty:
                        cell.markOnTop = Cell.MarkOnTopCell.Flag;

                        MarkedMinesCount++;

                        if (cell.IsMineHere)
                        {
                            FoundMinesCount++;
                        }
                        break;

                    case Cell.MarkOnTopCell.Flag:
                        cell.markOnTop = Cell.MarkOnTopCell.Question;

                        MarkedMinesCount--;

                        if (cell.IsMineHere)
                        {
                            FoundMinesCount--;
                        }
                        break;

                    case Cell.MarkOnTopCell.Question:
                        cell.markOnTop = Cell.MarkOnTopCell.Empty;
                        break;
                }
            }
        }

        private List<Cell> GetRemainingCellsAfteMinePress(int rowIndex, int columnIndex)
        {
            List<Cell> cellsList = new List<Cell>();

            cells[rowIndex, columnIndex].IsPressed = true;
            cells[rowIndex, columnIndex].markOnBottom = Cell.MarkOnBottomCell.MineBombed;
            cellsList.Add(cells[rowIndex, columnIndex]);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (!cells[i, j].IsPressed)
                    {
                        if (cells[i, j].markOnTop == Cell.MarkOnTopCell.Flag && !cells[i, j].IsMineHere)
                        {
                            cells[i, j].IsPressed = true;
                            cells[i, j].markOnBottom = Cell.MarkOnBottomCell.MineError;
                            cellsList.Add(cells[i, j]);
                        }
                        else if (cells[i, j].IsMineHere && cells[i, j].markOnTop != Cell.MarkOnTopCell.Flag)
                        {
                            cells[i, j].IsPressed = true;
                            cells[i, j].markOnBottom = Cell.MarkOnBottomCell.Mine;
                            cellsList.Add(cells[i, j]);
                        }
                    }
                }
            }

            return cellsList;
        }

        public List<Cell> GetOpenCellsAfterPress(int rowIndex, int columnIndex)
        {
            List<Cell> resultCells = new List<Cell>();

            if (!isDontExploded)
            {
                return resultCells;
            }

            if (cells[rowIndex, columnIndex].markOnTop != Cell.MarkOnTopCell.Flag && !cells[rowIndex, columnIndex].IsPressed)
            {
                if (AreMinesSet)
                {
                    if (cells[rowIndex, columnIndex].IsMineHere)
                    {
                        resultCells = GetRemainingCellsAfteMinePress(rowIndex, columnIndex);
                        isDontExploded = false;
                    }
                    else
                    {
                        resultCells = GetOpenCellsNear(rowIndex, columnIndex);
                    }
                }
                else
                {
                    AreMinesSet = true;

                    StartGame(rowIndex, columnIndex);

                    resultCells = GetOpenCellsNear(rowIndex, columnIndex);
                }
            }

            return resultCells;
        }

        private void StartGame(int startRow, int startCol)
        {
            FillStartEmptyArea(startRow, startCol);

            FillMineCells(startRow, startCol);

            FillEmptyCells();

            FillMinesCountNearCells();
        }

        private void FillMineCells(int startRow, int startCol)
        {
            Random random = new Random();

            for (int i = 0; i < MinesCount; i++)
            {
                int rowIndex = random.Next(RowCount);
                int colIndex = random.Next(ColumnCount);

                while (cells[rowIndex, colIndex].isMineInCellSet)
                {
                    rowIndex = random.Next(RowCount);
                    colIndex = random.Next(ColumnCount);
                }

                cells[rowIndex, colIndex].isMineInCellSet = true;
                cells[rowIndex, colIndex].IsMineHere = true;
            }
        }

        private void FillEmptyCells()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (!cells[i, j].isMineInCellSet)
                    {
                        cells[i, j].isMineInCellSet = true;
                        cells[i, j].IsMineHere = false;
                    }
                }
            }
        }

        private int GetMinesNearCellCount(int rowIndex, int colIndex)
        {
            int indentFromInnerCell = 1;
            int borderCorrection = 1;

            int starRowIndex = rowIndex - indentFromInnerCell < 0 ? 0 : rowIndex - indentFromInnerCell;
            int endRowIndex = rowIndex + indentFromInnerCell == RowCount ? RowCount - borderCorrection : rowIndex + indentFromInnerCell;

            int starColIndex = colIndex - indentFromInnerCell < 0 ? 0 : colIndex - indentFromInnerCell;
            int endColIndex = colIndex + indentFromInnerCell == ColumnCount ? ColumnCount - borderCorrection : colIndex + indentFromInnerCell;

            int minesAroundCount = 0;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColIndex; j++)
                {
                    if (cells[i, j].IsMineHere)
                    {
                        minesAroundCount++;
                    }
                }
            }

            return minesAroundCount;
        }

        private void FillMinesCountNearCells()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (!cells[i, j].IsMineHere)
                    {
                        cells[i, j].MineNearCount = GetMinesNearCellCount(i, j);
                    }
                }
            }
        }

        private void FillStartEmptyArea(int startRow, int startCol)
        {
            int indentFromStartCell = 1;
            int borderCorrection = 1;

            int starRowIndex = startRow - indentFromStartCell < 0 ? 0 : startRow - indentFromStartCell;
            int endRowIndex = startRow + indentFromStartCell == RowCount ? RowCount - borderCorrection : startRow + indentFromStartCell;

            int starColIndex = startCol - indentFromStartCell < 0 ? 0 : startCol - indentFromStartCell;
            int endColIndex = startCol + indentFromStartCell == ColumnCount ? ColumnCount - borderCorrection : startCol + indentFromStartCell;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColIndex; j++)
                {
                    cells[i, j].isMineInCellSet = true;
                    cells[i, j].IsMineHere = false;
                }
            }
        }

        private void FillQueueAndListAndPressEmptyAndNearCells(Queue<Cell> queue, List<Cell> cellsList)
        {
            Cell firstCell = queue.Dequeue();

            int rowIndex = firstCell.RowIndex;
            int colIndex = firstCell.ColIndex;

            int indentFromFirstCell = 1;
            int borderCorrection = 1;

            int starRowIndex = rowIndex - indentFromFirstCell < 0 ? 0 : rowIndex - indentFromFirstCell;
            int endRowIndex = rowIndex + indentFromFirstCell == RowCount ? RowCount - borderCorrection : rowIndex + indentFromFirstCell;

            int starColIndex = colIndex - indentFromFirstCell < 0 ? 0 : colIndex - indentFromFirstCell;
            int endColIndex = colIndex + indentFromFirstCell == ColumnCount ? ColumnCount - borderCorrection : colIndex + indentFromFirstCell;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColIndex; j++)
                {
                    if (!cells[i, j].IsPressed && cells[i, j].markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        cells[i, j].IsPressed = true;
                        cells[i, j].markOnBottom = Cell.MarkOnBottomCell.MineNearCount;
                        pressedCellsCount++;

                        cellsList.Add(cells[i, j]);

                        if (cells[i, j].MineNearCount == 0)
                        {
                            queue.Enqueue(cells[i, j]);
                        }
                    }
                }
            }
        }

        private List<Cell> GetPressAreaWithoutMinesNearEmptyCell(int rowIndex, int colIndex)
        {
            List<Cell> cellsList = new List<Cell>();
            Queue<Cell> queueOpeningArea = new Queue<Cell>();

            int currentRowIndex = rowIndex;
            int currentColIndex = colIndex;

            Cell currentCell = cells[currentRowIndex, currentColIndex];
            currentCell.IsPressed = true;
            currentCell.markOnBottom = Cell.MarkOnBottomCell.MineNearCount;
            pressedCellsCount++;

            queueOpeningArea.Enqueue(currentCell);
            cellsList.Add(currentCell);

            while (queueOpeningArea.Count != 0)
            {
                FillQueueAndListAndPressEmptyAndNearCells(queueOpeningArea, cellsList);
            }

            return cellsList;
        }

        private void MarkLastMinedCellsIfAllAnotherPressed(List<Cell> cellsList)
        {
            int cellsCount = RowCount * ColumnCount;
            int unpressedCellsCount = cellsCount - pressedCellsCount;

            if (MinesCount == unpressedCellsCount)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        if (!cells[i, j].IsPressed)
                        {
                            cells[i, j].markOnTop = Cell.MarkOnTopCell.Flag;
                            cellsList.Add(cells[i, j]);
                            FoundMinesCount++;
                        }
                    }
                }

                isNotFinishGame = false;
            }
        }

        private List<Cell> GetOpenCellsNear(int rowIndex, int colIndex)
        {
            if (cells[rowIndex, colIndex].MineNearCount == 0)
            {
                List<Cell> cellsList = GetPressAreaWithoutMinesNearEmptyCell(rowIndex, colIndex);

                MarkLastMinedCellsIfAllAnotherPressed(cellsList);

                return cellsList;
            }
            else
            {
                cells[rowIndex, colIndex].IsPressed = true;
                cells[rowIndex, colIndex].markOnBottom = Cell.MarkOnBottomCell.MineNearCount;
                pressedCellsCount++;

                List<Cell> cellsList = new List<Cell>();
                cellsList.Add(cells[rowIndex, colIndex]);

                MarkLastMinedCellsIfAllAnotherPressed(cellsList);

                return cellsList;
            }
        }
    }
}
