using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    public class GameParameters
    {
        public delegate void GameParametersChangedHeadler();
        public event GameParametersChangedHeadler OnChangeGameParameters;

        public int RowCount { get; set; }
        public int ColumnCount { get;  set; }
        public int MinesCount { get;  set; }
        public bool IsPossibleMarkQuestion { get;  set; }

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
    }
}
