using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Gui;
using Minesweeper.Logic;
using Minesweeper.TextUi;

namespace Minesweeper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //   Application.EnableVisualStyles();// нужно перед созданием первой формы
            //   Application.SetCompatibleTextRenderingDefault(false);// нужно перед созданием первой формы
            //   Application.Run(new MainForm());

            System.Console.WriteLine("Игра Сапер. Текстовая версия.");
            System.Console.WriteLine();

            GameParameters gameParameters = new GameParameters(10, 15, 10, false);
            GameLogic gameLogic = new GameLogic(gameParameters);
            GameText gameText = new GameText(gameParameters, gameLogic);

            gameText.Do("");

            Console.ReadLine();

        }
    }
}
