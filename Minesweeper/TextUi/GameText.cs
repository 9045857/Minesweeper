using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Logic;

namespace Minesweeper.TextUi
{
    class GameText
    {
        GameParameters gameParameters;
        GameLogic gameLogic;

        private int rowCount;
        private int columnCount;
        private int minesCount;

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
            

            //Пробелы для выравнивания ячеек. Если число колонок двузначное, то два пробела, в противном случае - один пробел.
          //  string spaces = (maxWroteIndex < 10) ? " " : "  ";

            int maxWroteColumnIndex = columnCount + 1;
            int columnIndexLength = maxWroteColumnIndex.ToString().Length;
            int columnWithSpaceLength = columnIndexLength + 1;

            int maxWroteRowIndex = rowCount + 1;
            int rowIndexLength = maxWroteRowIndex.ToString().Length;

            //формируем заголовок
            Console.Write("".PadRight(rowIndexLength,' '));
            Console.Write(" | ");

            for (int i = 0; i < columnCount; i++)
            {
                Console.Write((i + 1).ToString().PadRight(columnWithSpaceLength, ' '));
            }
            Console.WriteLine();

            Console.Write("".PadRight(rowIndexLength, '-'));
            Console.Write("-+");

            for (int i = 0; i < columnCount; i++)
            {
                Console.Write("".PadRight(columnWithSpaceLength, '-'));
            }
            Console.WriteLine();

            for (int i = 0; i < rowCount; i++)
            {
                Console.Write((i + 1).ToString().PadRight(rowIndexLength,' '));
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
                    Console.Write("о");
                    break;

                case Cell.MarkOnTopCell.Flag:
                    Console.ForegroundColor = ConsoleColor.DarkRed; // флаг
                    Console.Write("  ф");
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
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("Ф");
                    break;

                case Cell.MarkOnBottomCell.MineBombed:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("Ф");
                    break;

                case Cell.MarkOnBottomCell.MineError:
                    Console.WriteLine("X");
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
                            Console.WriteLine("8");
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

        public void Do(string task)
        {
            WriteGameArea();
        }

    }
}
