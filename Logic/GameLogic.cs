﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Logic
{
    public class GameLogic
    {
        public delegate void WinWithHighScore(int timeHighScore);
        public event WinWithHighScore OnWinWithHighScore;

        public delegate void BeginNewGameHeadler();
        public event BeginNewGameHeadler OnBeginNewGame;

        public delegate void MarkFlagCellHeadler(int remainMarkMinesCount);
        public event MarkFlagCellHeadler OnMark;

        public delegate void FinishGameHeadler();
        public event FinishGameHeadler OnFinishAndWinGame;

        public delegate void StartGameHeadler();
        public event StartGameHeadler OnStartGame;

        public delegate void ExplodedHeadler(int remainedMinesCount);
        public event ExplodedHeadler OnExploded;

        public delegate void TimeChange(int currentTime);
        public event TimeChange OnTimeChange;

        public Cell[,] cells;//TODO как это переделать в свойство?

        public GameParameters CurrentGameParameters { get; set; }

        private GameTimer time;
        public int CurrentTime { get; private set; }
        public int TotalPlayingTime { get; private set; }

        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }
        public int MinesCount { get; private set; }
        private bool isPossibleMarkQuestion;

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

        public bool IsExploded { get; set; }
        private bool isFinishAndWinGame;

        private HighScore highScore;

        public bool IsGameContinue
        {
            get
            {
                return RowCount * ColumnCount != PressedCellsAndFoundMinesCount && !IsExploded && !isFinishAndWinGame;
            }
        }

        public GameLogic(GameParameters gameParameters)
        {
            this.CurrentGameParameters = gameParameters;
            this.CurrentGameParameters.OnChangeGameParameters += gameParameters_Changed;

            time = new GameTimer();
            time.OnTimeChange += ChangeTime;

            DeserialazeHighScore();

            SetNewGameParameters();
        }

        private void ChangeTime(int currentTime)
        {
            CurrentTime = currentTime;
            OnTimeChange?.Invoke(CurrentTime);
        }

        private void SetNewGameParameters()
        {
            RowCount = CurrentGameParameters.RowCount;
            ColumnCount = CurrentGameParameters.ColumnCount;
            MinesCount = CurrentGameParameters.MinesCount;
            isPossibleMarkQuestion = CurrentGameParameters.IsPossibleMarkQuestion;

            if (!Equals(cells, null) && (cells.GetLength(0) == RowCount && cells.GetLength(1) == ColumnCount))
            {
                RestartCurrentGame();
            }
            else
            {
                SetBeginConditions();
                cells = CreateCells(RowCount, ColumnCount);

                time.Stop();
                OnBeginNewGame?.Invoke();
            }
        }

        private void gameParameters_Changed()
        {
            SetNewGameParameters();
        }

        private void SetBeginConditions()
        {
            AreMinesSet = false;
            IsExploded = false;
            isFinishAndWinGame = false;

            MarkedMinesCount = 0;
            FoundMinesCount = 0;

            pressedCellsCount = 0;

            int currentTime = 0;
            ChangeTime(currentTime);
        }

        public void RestartCurrentGame()
        {
            SetBeginConditions();

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    cells[i, j].SetBeginConditions();
                }
            }

            time.Stop();
            OnBeginNewGame?.Invoke();
        }

        public List<Cell> GetCellsListAvailableForPress(Cell cell)//нажимают двумя клавишами
        {
            List<Cell> cellsList = new List<Cell>();

            GetAvailableIndexesNearCell(cell, out int startRowIndex, out int endRowIndex, out int startColumnIndex, out int endColumnIndex);

            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for (int j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    if (!cells[i, j].IsPressed && cells[i, j].markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        cellsList.Add(cells[i, j]);
                    }
                }
            }

            return cellsList;
        }

        public void FinishAndWinGame()
        {
            isFinishAndWinGame = true;
            time.Stop();

            if (highScore.IsHighScoreGameResult(CurrentTime, RowCount, ColumnCount, MinesCount))
            {
                OnWinWithHighScore?.Invoke(CurrentTime);
            }
            else
            {
                OnFinishAndWinGame?.Invoke();
            }
        }

        private void DeserialazeHighScore()
        {
            string startupPath = GetFileHighScoreName();

            if (File.Exists(startupPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(startupPath, FileMode.Open))
                {
                    highScore = (HighScore)formatter.Deserialize(stream);
                }
            }
            else
            {
                highScore = new HighScore();
            }
        }

        public void ClearHighScore()
        {
            highScore.Clear();
            SerialazeHighScore();
        }

        private static string GetFileHighScoreName()
        {
            string fileName = GameLogicConstants.HighScoreFileName;
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, fileName);
            return startupPath;
        }

        private void SerialazeHighScore()
        {
            string startupPath = GetFileHighScoreName();
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(startupPath, FileMode.Create))
            {
                formatter.Serialize(stream, highScore);
            }
        }

        public void AddHighScore(string userName, int time, int rowCount, int columnCount, int minesCount)
        {
            highScore.AddHighScore(userName, time, rowCount, columnCount, minesCount);
            SerialazeHighScore();
        }

        public string GetHighScore()
        {
            return highScore.ToString();
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

                        EventOnMarkCell(MarkedMinesCount);

                        break;

                    case Cell.MarkOnTopCell.Flag:

                        if (isPossibleMarkQuestion)
                        {
                            cell.markOnTop = Cell.MarkOnTopCell.Question;
                        }
                        else
                        {
                            cell.markOnTop = Cell.MarkOnTopCell.Empty;
                        }

                        MarkedMinesCount--;

                        if (cell.IsMineHere)
                        {
                            FoundMinesCount--;
                        }

                        EventOnMarkCell(MarkedMinesCount);

                        break;

                    case Cell.MarkOnTopCell.Question:
                        cell.markOnTop = Cell.MarkOnTopCell.Empty;
                        break;
                }
            }
        }

        private void EventOnMarkCell(int markedCellsCount)
        {
            int remainMarkMinesCount = MinesCount - markedCellsCount;
            OnMark?.Invoke(remainMarkMinesCount);
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

            if (IsExploded)
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
                        IsExploded = true;

                        time.Stop();
                        OnExploded?.Invoke(MinesCount - FoundMinesCount);
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

            time.Start();

            OnStartGame?.Invoke();
        }

        private void FillMineCells(int startRow, int startCol)
        {
            Random random = new Random();

            for (int i = 0; i < MinesCount; i++)
            {
                int rowIndex = random.Next(RowCount);
                int colIndex = random.Next(ColumnCount);

                while (cells[rowIndex, colIndex].IsMineInCellSet)
                {
                    rowIndex = random.Next(RowCount);
                    colIndex = random.Next(ColumnCount);
                }

                cells[rowIndex, colIndex].IsMineInCellSet = true;
                cells[rowIndex, colIndex].IsMineHere = true;
            }
        }

        private void FillEmptyCells()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (!cells[i, j].IsMineInCellSet)
                    {
                        cells[i, j].IsMineInCellSet = true;
                        cells[i, j].IsMineHere = false;
                    }
                }
            }
        }

        private int GetMinesNearCellCount(int rowIndex, int columnIndex)
        {
            GetAvailableIndexesNearCell(cells[rowIndex, columnIndex], out int startRowIndex, out int endRowIndex, out int startColumnIndex, out int endColumnIndex);

            int minesAroundCount = 0;

            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for (int j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    if (cells[i, j].IsMineHere)
                    {
                        minesAroundCount++;
                    }
                }
            }

            return minesAroundCount;
        }

        public int GetMarkedMinesNearCell(Cell cell)
        {
            GetAvailableIndexesNearCell(cell, out int starRowIndex, out int endRowIndex, out int starColumnIndex, out int endColumnIndex);

            int minesMarkedAroundCount = 0;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColumnIndex; j <= endColumnIndex; j++)
                {
                    if (cells[i, j].markOnTop == Cell.MarkOnTopCell.Flag)
                    {
                        minesMarkedAroundCount++;
                    }
                }
            }

            return minesMarkedAroundCount;
        }

        public void GetAvailableIndexesNearCell(Cell cell, out int starRowIndex, out int endRowIndex, out int starColumnIndex, out int endColumnIndex)
        {
            int rowIndex = cell.RowIndex;
            int columnIndex = cell.ColIndex;

            int indentFromInnerCell = 1;
            int borderCorrection = 1;

            starRowIndex = rowIndex - indentFromInnerCell < 0 ? 0 : rowIndex - indentFromInnerCell;
            endRowIndex = rowIndex + indentFromInnerCell == RowCount ? RowCount - borderCorrection : rowIndex + indentFromInnerCell;

            starColumnIndex = columnIndex - indentFromInnerCell < 0 ? 0 : columnIndex - indentFromInnerCell;
            endColumnIndex = columnIndex + indentFromInnerCell == ColumnCount ? ColumnCount - borderCorrection : columnIndex + indentFromInnerCell;
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
            GetAvailableIndexesNearCell(cells[startRow, startCol], out int starRowIndex, out int endRowIndex, out int starColumnIndex, out int endColumnIndex);
            List<Cell> startFreeZone = new List<Cell>();

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColumnIndex; j <= endColumnIndex; j++)
                {
                    if (i == startRow && j == startCol)
                    {
                        cells[i, j].IsMineInCellSet = true;
                        cells[i, j].IsMineHere = false;
                    }
                    else
                    {
                        startFreeZone.Add(cells[i, j]);
                    }
                }
            }

            int startCellCount = 1;
            int startFreeZoneCount = startFreeZone.Count;
            int freeCellsCount = RowCount * ColumnCount - MinesCount;
            int cellsCountFreeZoneWithoutStartCell = freeCellsCount - startCellCount;

            if (cellsCountFreeZoneWithoutStartCell >= startFreeZoneCount)
            {
                foreach (Cell element in startFreeZone)
                {
                    element.IsMineInCellSet = true;
                    element.IsMineHere = false;
                }
            }
            else if (cellsCountFreeZoneWithoutStartCell != 0)
            {
                //сформируем список случайных индексов

                List<int> randomIndexFreeCells = new List<int>();
                for (int i = 0; i < startFreeZoneCount; i++)
                {
                    randomIndexFreeCells.Add(i);
                }

                int removeRandomIndexCount = startFreeZoneCount - cellsCountFreeZoneWithoutStartCell;
                int currentRandomIndexFreeCellsCount = startFreeZoneCount;

                Random random = new Random();

                for (int i = 0; i < removeRandomIndexCount; i++)
                {
                    int removalIndex = random.Next(currentRandomIndexFreeCellsCount);
                    randomIndexFreeCells.RemoveAt(removalIndex);

                    currentRandomIndexFreeCellsCount--;
                }

                for (int i = 0; i < cellsCountFreeZoneWithoutStartCell; i++)
                {
                    int i_temp = i;
                    int rifc_temp = randomIndexFreeCells[i];

                    startFreeZone[randomIndexFreeCells[i]].IsMineInCellSet = true;
                    startFreeZone[randomIndexFreeCells[i]].IsMineHere = false;
                }
            }
        }

        private void FillQueueAndListAndPressEmptyAndNearCells(Queue<Cell> queue, List<Cell> cellsList)
        {
            Cell firstCell = queue.Dequeue();

            GetAvailableIndexesNearCell(firstCell, out int starRowIndex, out int endRowIndex, out int starColumnIndex, out int endColumnIndex);

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColumnIndex; j <= endColumnIndex; j++)
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

                EventOnMarkCell(MinesCount);

                FinishAndWinGame();
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
