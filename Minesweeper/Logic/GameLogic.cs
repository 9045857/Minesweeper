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

        private int rowCount;
        private int columnCount;
        private int minesCount;

        public bool AreMinesSet { get; private set; }

        public bool isGameContinue;


        public GameLogic(int rowCount, int columnCount, int minesCount)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;

            cells = CreateCells(rowCount, columnCount);

            this.minesCount = minesCount;

            AreMinesSet = false;
            isGameContinue = true;
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

        public List<Cell> GetOpenCellsNearPressed(int rowIndex, int columnIndex)
        {
            List<Cell> resultCells = new List<Cell>();

            if (cells[rowIndex, columnIndex].markOnTop != Cell.MarkOnTopCell.Flag && !cells[rowIndex, columnIndex].IsPressed)
            {
                if (AreMinesSet)//попал в вопрос или мину, исходя из этого нужно статус ячейки в том числе менять
                {
                    if (cells[rowIndex, columnIndex].IsMineHere)
                    {
                        //resultCells = GetLooseGame();// раскрываем всю доску кроме правильно отмеченных
                     //   resultCells = GetAllCellsAfteMinePress(rowIndex, columnIndex);
                        isGameContinue = false;
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
            FillStartEmptyArea(startRow, startCol);//проверено

            FillMineCells(startRow, startCol);//проверено

            FillEmptyCells();

            FillMinesCountNearCells();
        }

        private void FillMineCells(int startRow, int startCol)
        {
            Random random = new Random();

            for (int i = 0; i < minesCount; i++)
            {
                int rowIndex = random.Next(rowCount);
                int colIndex = random.Next(columnCount);

                while (cells[rowIndex, colIndex].isMineInCellSet)
                {
                    rowIndex = random.Next(rowCount);
                    colIndex = random.Next(columnCount);
                }

                cells[rowIndex, colIndex].isMineInCellSet = true;
                cells[rowIndex, colIndex].IsMineHere = true;
            }
        }

        private void FillEmptyCells()
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
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
            int delta = 1;//TODO придумать нормальное название
            int borderCorrection = 1;

            int starRowIndex = rowIndex - delta < 0 ? 0 : rowIndex - delta;
            int endRowIndex = rowIndex + delta == rowCount ? rowCount - borderCorrection : rowIndex + delta;

            int starColIndex = colIndex - delta < 0 ? 0 : colIndex - delta;
            int endColIndex = colIndex + delta == columnCount ? columnCount - borderCorrection : colIndex + delta;

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
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
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
            int endRowIndex = startRow + indentFromStartCell == rowCount ? rowCount - borderCorrection : startRow + indentFromStartCell;

            int starColIndex = startCol - indentFromStartCell < 0 ? 0 : startCol - indentFromStartCell;
            int endColIndex = startCol + indentFromStartCell == columnCount ? columnCount - borderCorrection : startCol + indentFromStartCell;

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
            int endRowIndex = rowIndex + indentFromFirstCell == rowCount ? rowCount - borderCorrection : rowIndex + indentFromFirstCell;

            int starColIndex = colIndex - indentFromFirstCell < 0 ? 0 : colIndex - indentFromFirstCell;
            int endColIndex = colIndex + indentFromFirstCell == columnCount ? columnCount - borderCorrection : colIndex + indentFromFirstCell;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColIndex; j++)
                {
                    if (!cells[i, j].IsPressed && cells[i, j].markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        cells[i, j].IsPressed = true;
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

            Cell currentCell = cells[currentRowIndex, currentColIndex];//TODO ?????
            currentCell.IsPressed = true;

            queueOpeningArea.Enqueue(currentCell);
            cellsList.Add(currentCell);

            while (queueOpeningArea.Count != 0)
            {
                FillQueueAndListAndPressEmptyAndNearCells(queueOpeningArea, cellsList);
            }

            return cellsList;
        }

        private List<Cell> GetOpenCellsNear(int rowIndex, int colIndex)//TODO этот метод должен выдавать список
        {
            if (cells[rowIndex, colIndex].MineNearCount == 0)
            {
                return GetPressAreaWithoutMinesNearEmptyCell(rowIndex, colIndex);//сделано вроде нормально
            }
            else
            {
                cells[rowIndex, colIndex].IsPressed = true;

                List<Cell> cellsList = new List<Cell>();
                cellsList.Add(cells[rowIndex, colIndex]);

                return cellsList;
            }
        }
    }
}
