using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextUi
{
    class MessagesAndConstants
    {
        public const string UnknownCommandWarning = "Неизвестная команда. Для вызова справки нажмите Enter.";

        public const string FlagMark = "ф";
        public const string MineMark = "Ф";
        public const string MineErrorMine = "Ф";

        public const string NewGameCommand = "новая игра";

        public const int MinRowCount = 3;
        public const int MaxRowCount = 50;

        public const int MinColumnCount = 5;
        public const int MaxColumnCount = 50;

        public static void ShowHelpCommands()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("-= Основные команды =-");
            Console.WriteLine();
            Console.WriteLine("Нажатие на ячейку:");
            Console.WriteLine("2 3  - нажать на ячейку на пересечении 2-ой строки и 3-его стролбца.");
            Console.WriteLine();
            Console.WriteLine("  - Если ячейка 2:3 закрыта, то она откроется. Если в ней мина, игра проиграна. ");
            Console.WriteLine("  - Если ячейка 2:3 открыта, то, если рядом с ней отмечены все мины (число в ячейке равно количеству флажков вокруг),");
            Console.WriteLine("    откроется прилегающая область свободная от мин (аналог нажатия левой и правой клавиш мыши в графической версии игры).");
            Console.WriteLine();
            Console.WriteLine("Пометить/убрать флажком ячейку с миной:");
            Console.WriteLine("4 5 ф - пометить флажком ячейку в 4-ой строке 5-ом столбце. Если данная ячейка уже с флажком, то он исчезнет (ячейка будет доступна для нажатия)");
            Console.WriteLine();
            Console.WriteLine("Помеченные флажком ячейки не доступны для нажатия.");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public static void ShowHelpRules()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("-= Правила игры \"Сапёр\" =-");
            Console.WriteLine();
            Console.WriteLine("Начните с открытия одной ячейки. Напишите в командной строке через пробел номер строки и столбца.");
            Console.WriteLine("Например:");
            Console.WriteLine("5 5");
            Console.WriteLine();
            Console.WriteLine("Число в ячейке показывает, сколько мин скрыто вокруг данной ячейки.");
            Console.WriteLine("Это число поможет понять вам, где находятся безопасные ячейки, а где находятся бомбы.");
            Console.WriteLine();
            Console.WriteLine("Если рядом с открытой ячейкой есть пустая ячейка, то она откроется автоматически.");
            Console.WriteLine();
            Console.WriteLine("Если вы открыли ячейку с миной, то игра проиграна.");
            Console.WriteLine();
            Console.WriteLine("Что бы пометить ячейку, в которой находится бомба, напишите в командной сроке через пробел  номер строки, столбца и любой символ.");
            Console.WriteLine("Например:");
            Console.WriteLine("5 5 ф");
            Console.WriteLine();
            Console.WriteLine("После того, как вы отметите все мины, рядом с ячейком, можно написать через пробел ее номер строки и столбца, тогда откроются все свободные ячейки вокруг неё.");
            Console.WriteLine("Например:");
            Console.WriteLine("5 5");
            Console.WriteLine("Это аналог нажатия правой и левой клавишей мыши в графической версии игры.");
            Console.WriteLine();
            Console.WriteLine("Игра продолжается до тех пор, пока вы не откроете все незаминированные ячейки.");
            Console.WriteLine();
            Console.WriteLine("Удачной игры!");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        private static void ShowHelpStartGame()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("-= Начать игру =-");
            Console.WriteLine();
            Console.WriteLine("В игре три уровня сложности.");
            Console.WriteLine("1 - легкий уровень: поле 9х9, 10 мин.");
            Console.WriteLine("2 - средний уровень: поле 10х10, 40 мин.");
            Console.WriteLine("3 - сложный уровень: поле 10х16, 99 мин.");
            Console.WriteLine("Или можно создать свою игру:");
            Console.WriteLine("Например, запуск игры на поле 10х15 с 20 минами: ");
            Console.WriteLine("{0} 10 15 20", NewGameCommand);
            Console.WriteLine();
            Console.WriteLine("Запуск стандартной игры, например среднего уровня:");
            Console.WriteLine("{0} 2", NewGameCommand);
            Console.WriteLine();
            Console.WriteLine("Перезапуск игры:");
            Console.WriteLine("{0}", NewGameCommand);
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------");
        }
    }
}
