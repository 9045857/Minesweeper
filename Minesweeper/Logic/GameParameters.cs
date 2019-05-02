using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    public class GameParameters
    {
        public enum GameTypeLevel
        {
            Beginner = 0,
            Medium = 1,
            Expert = 2,
            Custom = 3
        }

        public delegate void GameParametersChangedHeadler();
        public event GameParametersChangedHeadler OnChangeGameParameters;

        public int RowCount { get; /*private */set; }
        public int ColumnCount { get; /*private*/ set; }
        public int MinesCount { get; /*private */set; }
        public bool IsPossibleMarkQuestion { get; /*private */set; }

        public GameParameters(int rowCount, int columnCount, int minesCount, bool isPossibleMarkQuestion)
        {
            SetGameParameters(rowCount, columnCount, minesCount, isPossibleMarkQuestion);
        }

        public GameParameters()
        {
        }

        public GameParameters(GameParameters gameParameters)
        {
            int rowCount = gameParameters.RowCount;
            int columnCount = gameParameters.ColumnCount;
            int minesCount = gameParameters.MinesCount;
            bool isPossibleMarkQuestion = gameParameters.IsPossibleMarkQuestion;

            SetGameParameters(rowCount, columnCount, minesCount, isPossibleMarkQuestion);
        }

        public void SetNewGameParameters(int rowCount, int columnCount, int minesCount, bool isPossibleMarkQuestion)
        {
            SetGameParameters(rowCount, columnCount, minesCount, isPossibleMarkQuestion);

            OnChangeGameParameters?.Invoke();
        }

        public void SetNewGameParameters(GameParameters gameParameters)
        {
            int rowCount = gameParameters.RowCount;
            int columnCount = gameParameters.ColumnCount;
            int minesCount = gameParameters.MinesCount;
            bool isPossibleMarkQuestion = gameParameters.IsPossibleMarkQuestion;

            SetGameParameters(rowCount, columnCount, minesCount, isPossibleMarkQuestion);

            OnChangeGameParameters?.Invoke();
        }

        private void SetGameParameters(int rowCount, int columnCount, int minesCount, bool isPossibleMarkQuestion)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            MinesCount = minesCount;

            IsPossibleMarkQuestion = isPossibleMarkQuestion;
        }

        public GameTypeLevel GameType
        {
            get
            {
                if (RowCount == GameLogicConstants.LowLevelRowCount && ColumnCount == GameLogicConstants.LowLevelColumnCount && MinesCount == GameLogicConstants.LowLevelMinesCount)
                {
                    return GameTypeLevel.Beginner;
                }
                else if (RowCount == GameLogicConstants.MediumLevelRowCount && ColumnCount == GameLogicConstants.MediumLevelColumnCount && MinesCount == GameLogicConstants.MediumLevelMinesCount)
                {
                    return GameTypeLevel.Medium;
                }
                else if (RowCount == GameLogicConstants.HighLevelRowCount && ColumnCount == GameLogicConstants.HighLevelColumnCount && MinesCount == GameLogicConstants.HighLevelMinesCount)
                {
                    return GameTypeLevel.Expert;
                }
                else
                {
                    return GameTypeLevel.Custom;
                }
            }
        }
    }
}
