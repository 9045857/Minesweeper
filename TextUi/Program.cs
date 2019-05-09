using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace TextUi
{
    class Program
    {
        //private GameText gameText;
        //private GameLogic gameLogic;
        //private GameParameters gameParameters;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine();


            GameParameters gameParameters = new GameParameters(9, 9, 10, false);
            GameLogic  gameLogic = new GameLogic(gameParameters);

            GameText gameText = new GameText(gameParameters, gameLogic);

            while (true)
            {
                string command = Console.ReadLine();
                gameText.Do(command);

            }            
        }


    }
}
