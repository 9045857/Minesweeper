using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GameLogicConstants
    {
        public const int LowLevelRowCount = 9;
        public const int LowLevelColumnCount = 9;
        public const int LowLevelMinesCount = 10;

        public const int MediumLevelRowCount = 16;
        public const int MediumLevelColumnCount = 16;
        public const int MediumLevelMinesCount = 40;

        public const int HighLevelRowCount = 16;
        public const int HighLevelColumnCount = 30;
        public const int HighLevelMinesCount = 99;

        //   public const int MaxGameTime = 999;
        public const int PauseTime = 1000;

        public const string HighScoreFileName = "highScore";

        public const string HighScoreBeginnerCaption = "Простой";
        public const string HighScoreMediumCaption = "Средний";
        public const string HighScoreExpertCaption = "Сложный";

        public const int NumberSpaceHighScore = 3;
        public const int UserNameSpaceHighScore = 15;
        public const int UserTimeSpaceHighScore = 5;
    }
}
