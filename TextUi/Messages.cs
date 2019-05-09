using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextUi
{
    class Messages
    {
        public const string UnknownCommandWarning = "Неизвестная команда. Для вызова справки нажмите Enter.";

        public const string FlagMark = "ф";
        public const string MineMark = "Ф";
        public const string MineErrorMine = "Ф";

        public static void ShowHelpCommands()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("-= Команды =-");
            Console.WriteLine();
            Console.WriteLine("Структура команды: <действие> пробел  <номер строки> пробел <номер столбца>");
            Console.WriteLine("Действие пишется русскими буквами: о, ф, н.");
            Console.WriteLine();
            Console.WriteLine("о 2 3  - открыть ячейку на пересечении 2-ой строки и 3-его стролбца.");
            Console.WriteLine("ф 4 5  - пометить флажком ячейку в 4-ой строке 5-ом столбце.");
            Console.WriteLine("н 2 6  - нажать на открытую ячейку во 2-ой строк 6-ом столбце ");
            Console.WriteLine("         (аналог нажатия двумя клавишами мыши в графической версии игры).");
            Console.WriteLine("         Игра откроет свободные ячейки, если нажатие на ячейку с цифрой, ");
            Console.WriteLine("         рядом с которой отмечено соответствующее количество мин. ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public static void ShowHelpRules()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("-= Правила игры =-");
            Console.WriteLine();
            Console.WriteLine("Играть в \"сапёр\" очень просто. ");
            Console.WriteLine();
            Console.WriteLine("Начните с открытия одной ячейки. Напишите в командной строке: ");
            Console.WriteLine("\"о\", номер ее строки и столбца.");
            Console.WriteLine();
            Console.WriteLine("Число в ячейке показывает, сколько мин скрыто вокруг данной ячейки.");
            Console.WriteLine("Это число поможет понять вам, где находятся безопасные ячейки, ");
            Console.WriteLine("а где находятся бомбы.");
            Console.WriteLine();
            Console.WriteLine("Если рядом с открытой ячейкой есть пустая ячейка, ");
            Console.WriteLine("то она откроется автоматически.");
            Console.WriteLine();
            Console.WriteLine("Если вы открыли ячейку с миной, то игра проиграна.");
            Console.WriteLine("Что бы пометить ячейку, в которой находится бомба, ");
            Console.WriteLine("напишите в командной сроке \"ф\", номер строки и номер столбца.");
            Console.WriteLine();
            Console.WriteLine("После того, как вы отметите все мины, рядом с ячейком,");
            Console.WriteLine("можно написать \"н\", номер строки и столбца данной ячейки.");
            Console.WriteLine("Тогда откроются все свободные ячейки вокруг неё");
            Console.WriteLine();
            Console.WriteLine("Если в ячейке указано число, оно показывает, сколько мин скрыто в ");
            Console.WriteLine("восьми ячейках вокруг данной. Это число помогает понять, ");
            Console.WriteLine("где находятся безопасные ячейки.");
            Console.WriteLine();
            Console.WriteLine("Игра продолжается до тех пор, пока вы не откроете все ");
            Console.WriteLine("не заминированные ячейки.");
            Console.WriteLine();
            Console.WriteLine("Удачной игры!");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        private static void ShowHelpStartGame()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("-= Начало игры =-");
            Console.WriteLine();
            Console.WriteLine("Игроку предоставляются на выбор три уровня сложности.");
            Console.WriteLine("1 - легкий уровень: поле 9х9, 10 мин.");
            Console.WriteLine("2 - средний уровень: поле 10х10, 40 мин.");
            Console.WriteLine("3 - сложный уровень: поле 10х16, 99 мин.");
            Console.WriteLine();
            Console.WriteLine("Начать игру, например среднего уровня, нужно следующим образом:");
            Console.WriteLine("играть 2");
            Console.WriteLine();
            Console.WriteLine("Перезапуск игры:");
            Console.WriteLine("играть");
            Console.WriteLine();
            Console.WriteLine("Запуск игры со своими параметрами:");
            Console.WriteLine("Например, запуск игры на поле 10х15 с 20 минами: ");
            Console.WriteLine("играть 10 15 20");
            Console.WriteLine();
            Console.WriteLine("Начать игру с новыми параметрами или перезапустить с текущими можно в любой момент.");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        private static void ShowHelpSymbols()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Внешний вид игрового поля");
            Console.WriteLine();

            Console.Write("Мин: 10");
            Console.Write("             ");
            Console.WriteLine("Время: 0");

            Console.WriteLine();
            Console.WriteLine("   | 1  2  3  4  5  6  7  8  9 10 11 12 13");
            Console.WriteLine("---+---------------------------------------");
            Console.WriteLine(" 1 | o  o  o  o  o  o  o  o  o  o  o  o  o");
            Console.WriteLine(" 2 | o  o  o  o  o  o  o  o  o  o  o  o  o");
            Console.WriteLine(" 3 | o  o  o  o  o  o  o  o  o  o  o  o  o");
            Console.WriteLine(" 4 | o  o  o  o  o  o  o  o  o  o  o  o  o");
            Console.WriteLine(" 5 | o  o  o  o  o  o  o  o  o  o  o  o  o");
            Console.WriteLine(" 6 | o  o  o  o  o  o  o  o  o  o  o  o  o");
            Console.WriteLine(" 7 | o  o  o  o  o  o  o  o  o  o  o  o  o");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Символы использующиеся в игре.");
            Console.WriteLine();

            Console.WriteLine("Цифры указывают на количесто мин рядом с ячейкой.");

            Console.ForegroundColor = ConsoleColor.Blue; // 1 
            Console.Write("1  ");

            Console.ForegroundColor = ConsoleColor.Green; // 2
            Console.Write("2  ");

            Console.ForegroundColor = ConsoleColor.Red; //3
            Console.Write("3  ");

            Console.ForegroundColor = ConsoleColor.DarkCyan; // 4
            Console.Write("4  ");

            Console.ForegroundColor = ConsoleColor.DarkYellow; //5
            Console.Write("5  ");

            Console.ForegroundColor = ConsoleColor.Cyan; // 6
            Console.Write("6  ");

            Console.ForegroundColor = ConsoleColor.Yellow; // 7
            Console.Write("7  ");

            Console.ForegroundColor = ConsoleColor.DarkGray; // 8
            Console.WriteLine("8  ");
            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkRed; // флаг
            Console.Write("  ф");
            Console.ResetColor();
            Console.WriteLine(" - флажок для отметки мины. Ставиться игроком, с помощью команды, например: ф 5 9.");

            Console.WriteLine();
            Console.Write("  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("Ф");
            Console.ResetColor();
            Console.WriteLine(" - взорвавшаяся мина.");

            Console.WriteLine();
            Console.Write("  ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("Ф");
            Console.ResetColor();
            Console.WriteLine(" - ненайденная мина.");

            Console.WriteLine();
            Console.WriteLine("  X - ошибочно отмеченная мина.");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }
    }
}
