using Logic;
using System;

namespace TextUi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Игра \"Сапер\".");
            Console.WriteLine();

            // на старте загружаем параметры начинающего
            int rowCount = Logic.GameLogicConstants.LowLevelRowCount;
            int columnCount = Logic.GameLogicConstants.LowLevelColumnCount;
            int minesCount = Logic.GameLogicConstants.LowLevelMinesCount;

            bool canQestionMark = false; //в текстовой версии будем играть без возможности маркировать вопросами. 
                                         //Технически такая возможностьесь, но из-за сложности игры в текстовом формате, лучше без нее.

            GameParameters gameParameters = new GameParameters(rowCount, columnCount, minesCount, canQestionMark);
            GameLogic gameLogic = new GameLogic(gameParameters);
            GameText gameText = new GameText(gameParameters, gameLogic);

            gameText.WriteGameArea();

            while (true)
            {
                string command = Console.ReadLine();
                gameText.Do(command);
            }
        }
    }
}
