using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace TextUi
{
    class GameText
    {
        GameParameters gameParameters;
        GameLogic gameLogic;

        private int rowCount;
        private int columnCount;
        private int minesCount;
        private int remainedMines;

        private List<Cell> cellsForPress;

        public GameText(GameParameters gameParameters, GameLogic gameLogic)
        {
            this.gameParameters = gameParameters;
            this.gameParameters.OnChangeGameParameters += SetRowColumnMinesCount;

            this.gameLogic = gameLogic;
            gameLogic.OnWinWithHighScore += AddHighScore;
            gameLogic.OnMark += ChangeRemainMines;
            gameLogic.OnExploded+= ChangeRemainMines;

            SetRowColumnMinesCount();
            remainedMines = minesCount;
        }

        private void SetRowColumnMinesCount()
        {
            rowCount = gameParameters.RowCount;
            columnCount = gameParameters.ColumnCount;
            minesCount = gameParameters.MinesCount;
        }

        private void ChangeRemainMines(int remainedMines)
        {
            this.remainedMines = remainedMines;
        }

        private void SetGameParameters(int gameType, out int rowCount, out int columnCount, out int minesCount)
        {
            switch (gameType)
            {
                case 1:
                    rowCount = GameLogicConstants.LowLevelRowCount;
                    columnCount = GameLogicConstants.LowLevelColumnCount;
                    minesCount = GameLogicConstants.LowLevelMinesCount;
                    break;

                case 2:
                    rowCount = GameLogicConstants.MediumLevelRowCount;
                    columnCount = GameLogicConstants.MediumLevelColumnCount;
                    minesCount = GameLogicConstants.MediumLevelMinesCount;
                    break;

                case 3:
                    rowCount = GameLogicConstants.HighLevelRowCount;
                    columnCount = GameLogicConstants.HighLevelColumnCount;
                    minesCount = GameLogicConstants.HighLevelMinesCount;
                    break;

                default:
                    rowCount = -1;
                    columnCount = -1;
                    minesCount = -1;
                    break;
            }
        }

        private void WornUnknownComand()
        {
            Console.WriteLine(Messages.UnknownCommandWarning);
        }

        public void WriteGameArea()
        {
            int timeCaption = gameLogic.CurrentTime;
                       
            Console.WriteLine();
            Console.WriteLine("   Мины: {0}       (*_*)       Время: {1}", remainedMines, timeCaption);
            Console.WriteLine();

            int maxWroteColumnIndex = columnCount + 1;
            int columnIndexLength = maxWroteColumnIndex.ToString().Length;
            int columnWithSpaceLength = columnIndexLength + 1;

            int maxWroteRowIndex = rowCount + 1;
            int rowIndexLength = maxWroteRowIndex.ToString().Length;

            string space = "   ";

            //формируем заголовок
            Console.Write(space);
            Console.Write("".PadRight(rowIndexLength, ' '));
            Console.Write(" | ");

            for (int i = 0; i < columnCount; i++)
            {
                Console.Write((i + 1).ToString().PadRight(columnWithSpaceLength, ' '));
            }
            Console.WriteLine();

            Console.Write(space);
            Console.Write("".PadRight(rowIndexLength, '-'));
            Console.Write("-+");

            for (int i = 0; i < columnCount; i++)
            {
                Console.Write("".PadRight(columnWithSpaceLength, '-'));
            }
            Console.WriteLine();

            for (int i = 0; i < rowCount; i++)
            {
                Console.Write(space);
                Console.Write((i + 1).ToString().PadRight(rowIndexLength, ' '));
                Console.Write(" | ");

                for (int j = 0; j < columnCount; j++)
                {
                    Cell cell = gameLogic.cells[i, j];

                    if (cell.IsPressed)
                    {
                        WritePressedCell(cell);
                    }
                    else
                    {
                        WriteUnpressedCell(cell);
                    }

                    Console.Write("".PadLeft(columnIndexLength, ' '));
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void WriteUnpressedCell(Cell cell)
        {
            switch (cell.markOnTop)
            {
                case Cell.MarkOnTopCell.Empty:
                    Console.ForegroundColor = ConsoleColor.DarkGray; // начальная ячейка
                    Console.Write("о");
                    break;

                case Cell.MarkOnTopCell.Flag:
                    Console.ForegroundColor = ConsoleColor.DarkRed; // флаг
                    Console.Write("ф");
                    break;

                default:
                    Console.Write("E");//ERROR
                    break;
            }

            Console.ResetColor();
        }

        private void WritePressedCell(Cell cell)
        {
            switch (cell.markOnBottom)
            {
                case Cell.MarkOnBottomCell.Empty:
                    Console.Write(" ");
                    break;

                case Cell.MarkOnBottomCell.Mine:
                    //Console.ForegroundColor = ConsoleColor.DarkRed;
                    //Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Ф");
                    break;

                case Cell.MarkOnBottomCell.MineBombed:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("Ф");
                    break;

                case Cell.MarkOnBottomCell.MineError:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("X");
                    break;

                case Cell.MarkOnBottomCell.MineNearCount:
                    switch (cell.MineNearCount)
                    {
                        case 0:
                            Console.Write(" ");
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Blue; // 1 
                            Console.Write("1");
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Green; // 2
                            Console.Write("2");
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Red; //3
                            Console.Write("3");
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.DarkCyan; // 4
                            Console.Write("4");
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.DarkYellow; //5
                            Console.Write("5");
                            break;
                        case 6:
                            Console.ForegroundColor = ConsoleColor.Cyan; // 6
                            Console.Write("6");
                            break;
                        case 7:
                            Console.ForegroundColor = ConsoleColor.Yellow; // 7
                            Console.Write("7");
                            break;
                        case 8:
                            Console.ForegroundColor = ConsoleColor.DarkGray; // 8
                            Console.Write("8");
                            break;
                        default:
                            Console.Write("?");
                            break;
                    }
                    break;
                default:
                    Console.Write("E");//ERROR
                    break;
            }

            Console.ResetColor();
        }



        private bool TryParseRowAndColumnIndexes(string task, out int rowIndex, out int columnIndex)
        {
            int firstSpaceIndex = task.IndexOf(' ');
            int lastSpaceIndex = task.LastIndexOf(' ');

            if (firstSpaceIndex == 0 || lastSpaceIndex != firstSpaceIndex)
            {
                rowIndex = -1;
                columnIndex = -1;
                return false;
            }

            int nextIndex = 1;
            string rowIndexText = task.Substring(0, firstSpaceIndex);
            string columnIndexText = task.Substring(firstSpaceIndex + nextIndex, task.Length - firstSpaceIndex - nextIndex);

            if (Int32.TryParse(rowIndexText, out rowIndex) && Int32.TryParse(columnIndexText, out columnIndex))
            {
                //Correct DisplayIndexes to Listindexes
                rowIndex--;
                columnIndex--;

                if ((rowIndex >= 0 && rowIndex <= rowCount) && (columnIndex >= 0 && columnIndex <= columnCount))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Ошибка ввода данных: выход за границы индексов.");
                    return false;
                }
            }
            else
            {
                rowIndex = -1;
                columnIndex = -1;
                return false;
            }
        }

        private bool TryParseBeginNewGame(string task, out int gameType, out int rowCount, out int columnCount, out int minesCount)
        {
            string mainCommand = "новая игра";
            string сommand = task.Substring(0, mainCommand.Length);

            if (!mainCommand.Equals(сommand))
            {
                return GetFalse(out gameType, out rowCount, out columnCount, out minesCount);
            }

            string gameParameters = task.Substring(mainCommand.Length, task.Length - mainCommand.Length);
            string[] parameters = gameParameters.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parameters.Length == 1)
            {
                if (Int32.TryParse(parameters[0], out gameType))
                {
                    if (gameType == 1 || gameType == 2 || gameType == 3)
                    {
                        rowCount = -1;
                        columnCount = -1;
                        minesCount = -1;

                        return true;
                    }
                    else
                    {
                        return GetFalse(out gameType, out rowCount, out columnCount, out minesCount);
                    }
                }
                else
                {
                    return GetFalse(out gameType, out rowCount, out columnCount, out minesCount);
                }
            }
            else if (parameters.Length == 3)
            {
                if (Int32.TryParse(parameters[0], out rowCount) && Int32.TryParse(parameters[1], out columnCount) && Int32.TryParse(parameters[2], out minesCount))
                {
                    gameType = -1;
                    return true;
                }
                else
                {
                    return GetFalse(out gameType, out rowCount, out columnCount, out minesCount);
                }
            }
            else
            {
                return GetFalse(out gameType, out rowCount, out columnCount, out minesCount);
            }
        }

        private static bool GetFalse(out int gameType, out int rowCount, out int columnCount, out int minesCount)
        {
            gameType = -1;
            rowCount = -1;
            columnCount = -1;
            minesCount = -1;

            return false;
        }

        private bool TryParseRowAndColumnIndexesFlagMark(string task, out int rowIndex, out int columnIndex)
        {
            int firstSpaceIndex = task.IndexOf(' ');
            int lastSpaceIndex = task.LastIndexOf(' ');

            if (firstSpaceIndex == 0 || lastSpaceIndex == 0 || lastSpaceIndex == firstSpaceIndex)
            {
                rowIndex = -1;
                columnIndex = -1;
                return false;
            }

            int nextIndex = 1;
            string rowIndexText = task.Substring(0, firstSpaceIndex);
            string columnIndexText = task.Substring(firstSpaceIndex + nextIndex, lastSpaceIndex - firstSpaceIndex - nextIndex);

            if (Int32.TryParse(rowIndexText, out rowIndex) && Int32.TryParse(columnIndexText, out columnIndex))
            {
                //Correct DisplayIndexes to Listindexes
                rowIndex--;
                columnIndex--;

                if ((rowIndex >= 0 && rowIndex <= rowCount) && (columnIndex >= 0 && columnIndex <= columnCount))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Ошибка ввода данных: выход за границы индексов.");
                    return false;
                }
            }
            else
            {
                rowIndex = -1;
                columnIndex = -1;
                return false;
            }
        }

        private void WriteCommonHelpInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Информация. Варианты:");
            Console.WriteLine("1 - игровые команды");
            Console.WriteLine("2 - правила игры");
            Console.WriteLine("3 - таблица рекордов");
            Console.WriteLine();
            Console.WriteLine("4 - выход из игры");
            Console.WriteLine();
        }

        private void AddHighScore(int time)
        {
            Console.WriteLine("Вы выиграли с отличным результатом {0} сек.", time);
            Console.WriteLine("Добавить ваш результат в таблицу рекордов? (\"1\" - да)");

            string yesNo = Console.ReadLine();

            if (yesNo == "1")
            {
                Console.Write("Введите свое имя: ");
                string userName = Console.ReadLine();

                if (userName == "")
                {
                    userName = "игрок";
                }

                gameLogic.AddHighScore(userName, time, rowCount, columnCount, minesCount);
            }
        }

        public void Do(string task)
        {
            switch (task)
            {
                case "":
                    WriteCommonHelpInfo();
                    break;

                case "0":
                    WriteGameArea();
                    break;

                case "1":
                    Messages.ShowHelpCommands();
                    break;

                case "2":
                    Messages.ShowHelpRules();
                    break;

                case "3":
                    Console.WriteLine(gameLogic.GetHighScore());
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                case "новая игра":
                    gameLogic.RestartCurrentGame();
                    WriteGameArea();
                    break;

                default:

                    int rowIndex;
                    int columnIndex;

                    if (TryParseRowAndColumnIndexes(task, out rowIndex, out columnIndex))
                    {
                        if (!gameLogic.cells[rowIndex, columnIndex].IsPressed && gameLogic.cells[rowIndex, columnIndex].markOnTop != Cell.MarkOnTopCell.Flag)
                        {
                            gameLogic.GetOpenCellsAfterPress(rowIndex, columnIndex);
                        }
                        else if (gameLogic.cells[rowIndex, columnIndex].IsPressed && gameLogic.cells[rowIndex, columnIndex].MineNearCount > 0)
                        {

                            cellsForPress = gameLogic.GetCellsListAvailableForPress(gameLogic.cells[rowIndex, columnIndex]);

                            int cellsNearWhithoutFlags = gameLogic.GetMarkedMinesNearCell(gameLogic.cells[rowIndex, columnIndex]);

                            if (cellsNearWhithoutFlags == gameLogic.cells[rowIndex, columnIndex].MineNearCount)
                            {
                                foreach (Cell cell in cellsForPress)
                                {
                                    gameLogic.GetOpenCellsAfterPress(cell.RowIndex, cell.ColIndex);
                                }
                            }

                            cellsForPress.Clear();
                        }
                    }
                    else if (TryParseRowAndColumnIndexesFlagMark(task, out rowIndex, out columnIndex))
                    {
                        gameLogic.Mark(gameLogic.cells[rowIndex, columnIndex]);
                    }
                    else if (TryParseBeginNewGame(task, out int gameType, out int rowCount, out int columnCount, out int minesCount))
                    {
                        if (gameType != -1)
                        {
                            SetGameParameters(gameType, out rowCount, out columnCount, out minesCount);
                        }

                        gameParameters.SetNewGameParameters(rowCount, columnCount, minesCount, false);
                    }
                    else
                    {
                        WornUnknownComand();
                    }

                    WriteGameArea();

                    break;
            }
        }
    }
}
