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

        private List<Cell> cellsForPress;

        public GameText(GameParameters gameParameters, GameLogic gameLogic)
        {
            this.gameParameters = gameParameters;
            this.gameLogic = gameLogic;

            rowCount = gameParameters.RowCount;
            columnCount = gameParameters.ColumnCount;
            minesCount = gameParameters.MinesCount;
        }

        private void SetGameParameters()
        {


        }

        private void WornUnknownComand()
        {
            Console.WriteLine(Messages.UnknownCommandWarning);
        }

        private void WriteGameArea()
        {
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

            if (firstSpaceIndex == 0 || lastSpaceIndex == 0 || lastSpaceIndex == firstSpaceIndex)
            {
                rowIndex = -1;
                columnIndex = -1; ;
                return false;
            }

            int nextIndex = 1;
            string rowIndexText = task.Substring(firstSpaceIndex + nextIndex, lastSpaceIndex - firstSpaceIndex - nextIndex);
            string columnIndexText = task.Substring(lastSpaceIndex + nextIndex, task.Length - lastSpaceIndex - nextIndex);

            if (Int32.TryParse(rowIndexText, out rowIndex) && Int32.TryParse(columnIndexText, out columnIndex))
            {
                return true;
            }
            else
            {
                rowIndex = -1;
                columnIndex = -1; ;
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
            Console.WriteLine("Введите вариант справки:");
        }

        public void Do(string task)
        {
            switch (task)
            {
                case "":
                    WriteCommonHelpInfo();
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

                default:
                    {
                        string firstLetter = task.Substring(0, 1);

                        int rowIndex;
                        int columnIndex;

                        switch (firstLetter)
                        {
                            case "о":
                                if (TryParseRowAndColumnIndexes(task, out rowIndex, out columnIndex))
                                {
                                    rowIndex--;
                                    columnIndex--;

                                    gameLogic.GetOpenCellsAfterPress(rowIndex, columnIndex);
                                }
                                break;

                            case "н":
                                if (TryParseRowAndColumnIndexes(task, out rowIndex, out columnIndex))
                                {
                                    rowIndex--;
                                    columnIndex--;

                                    cellsForPress = gameLogic.GetCellsListAvailableForPress(gameLogic.cells[rowIndex, columnIndex]);

                                    int cellsNearWhithoutFlags = gameLogic.GetMarkedMinesNearCell(gameLogic.cells[rowIndex, columnIndex]);

                                    if (gameLogic.cells[rowIndex, columnIndex].IsPressed && cellsNearWhithoutFlags == gameLogic.cells[rowIndex, columnIndex].MineNearCount)
                                    {
                                        foreach (Cell cell in cellsForPress)
                                        {
                                            gameLogic.GetOpenCellsAfterPress(cell.RowIndex, cell.ColIndex);
                                        }
                                    }

                                    cellsForPress.Clear();
                                }
                                break;

                            case "ф":
                                if (TryParseRowAndColumnIndexes(task, out rowIndex, out columnIndex))
                                {
                                    rowIndex--;
                                    columnIndex--;

                                    gameLogic.Mark(gameLogic.cells[rowIndex, columnIndex]);
                                }
                                break;
                            default:
                                WornUnknownComand();
                                break;
                        }

                        Console.WriteLine();
                        WriteGameArea();
                        Console.WriteLine();
                    }
                    break;

            }
        }
    }
}
