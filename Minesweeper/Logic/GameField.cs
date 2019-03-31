using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    class GameField
    {
        public Cell[,] cells;

        private int rowCount;

        private int colCount;

        private int mineCount;


        public GameField(int rowCount, int colCount, int mineCount)
        {
            cells = new Cell[rowCount, colCount];

            this.rowCount = rowCount;
            this.colCount = colCount;

            this.mineCount = mineCount;
        }

        private void FillMineCells(int startRow, int startCol)
        {
            Random random = new Random();

            for (int i = 0; i < mineCount; i++)
            {
                int rowIndex = random.Next(rowCount);
                int colIndex = random.Next(colCount);

                while (!Equals(cells[rowIndex, colIndex], null))
                {
                    rowIndex = random.Next(rowCount);
                    colIndex = random.Next(colCount);
                }

                cells[startRow, startCol] = new Cell(rowIndex, colIndex);
                cells[startRow, startCol].IsMineHere = true;
            }
        }

        private void FillEmptyCells()
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (Equals(cells[i, j], null))
                    {
                        cells[i, j] = new Cell(i, j);
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
            int endColIndex = colIndex + delta == colCount ? colCount - borderCorrection : colIndex + delta;

            int minesAroundCount = 0;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColIndex; j++)
                {
                    if (!cells[i, j].IsMineHere)
                    {
                        minesAroundCount++;
                    }
                }
            }

            int ownInfluenceCompensation = 1;

            return minesAroundCount - ownInfluenceCompensation;
        }

        private void FillMinesCountNearMineCells()
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
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
            int endColIndex = startCol + indentFromStartCell == colCount ? colCount - borderCorrection : startCol + indentFromStartCell;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColIndex; j++)
                {
                    cells[i, j] = new Cell(i, j);
                    cells[i, j].IsMineHere = false;
                }
            }
        }

        public void StartGame(int startRow, int startCol)
        {
            FillStartEmptyArea(startRow, startCol);

            FillMineCells(startRow, startCol);

            FillEmptyCells();

            FillMinesCountNearMineCells();

            PressOnCell(startRow, startCol);
        }

        public void FinishGame()
        {
        }



        private void FillQueueAndPressEmptyCells(Queue<Cell> queue)
        {
            Cell firstCell = queue.Dequeue();
            firstCell.IsPressed = true;

            int rowIndex = firstCell.RowIndex;
            int colIndex = firstCell.ColIndex;

            int indentFromFirstCell = 1;
            int borderCorrection = 1;

            int starRowIndex = rowIndex - indentFromFirstCell < 0 ? 0 : rowIndex - indentFromFirstCell;
            int endRowIndex = rowIndex + indentFromFirstCell == rowCount ? rowCount - borderCorrection : rowIndex + indentFromFirstCell;

            int starColIndex = colIndex - indentFromFirstCell < 0 ? 0 : colIndex - indentFromFirstCell;
            int endColIndex = colIndex + indentFromFirstCell == colCount ? colCount - borderCorrection : colIndex + indentFromFirstCell;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColIndex; j++)
                {
                    if (!cells[i, j].IsPressed)
                    {
                        cells[i, j].IsPressed = true;

                        if (cells[i, j].MineNearCount == 0)
                        {
                            queue.Enqueue(cells[i, j]);
                        }
                    }
                }
            }
        }

        private void PressAreaWithoutMinesNearEmptyCell(int rowIndex, int colIndex)
        {
            Queue<Cell> queueOpeningArea = new Queue<Cell>();

            int currentRowIndex = rowIndex;
            int currentColIndex = colIndex;

            Cell currentCell = cells[currentRowIndex, currentColIndex];

            queueOpeningArea.Enqueue(currentCell);

            while (!Equals(queueOpeningArea.Peek(), null))
            {
                FillQueueAndPressEmptyCells(queueOpeningArea);
            }
        }

        public void PressOnCell(int rowIndex, int colIndex)
        {
            if (cells[rowIndex, colIndex].IsMineHere)
            {
                FinishGame();

                return;
            }

            if (cells[rowIndex, colIndex].MineNearCount == 0)
            {
                PressAreaWithoutMinesNearEmptyCell(rowIndex, colIndex);
            }
            else
            {
                cells[rowIndex, colIndex].IsPressed = true;
            }
        }

        public void MarkFlag(int rowIndex, int colIndex)
        {
            cells[rowIndex, colIndex].IsFlag=true;            
        }


        public void MarkMine(int rowIndex, int colIndex)
        {
            cells[rowIndex, colIndex].IsMineHere = true;
        }


    }
}
