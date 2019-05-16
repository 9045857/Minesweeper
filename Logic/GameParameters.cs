namespace Logic
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

        private int rowCount;
        public int RowCount
        {
            get
            {
                return rowCount;
            }
            set
            {
                rowCount = GetRowCountAfterCheck(value);
            }
        }

        private int columnCount;
        public int ColumnCount
        {
            get
            {
                return columnCount;
            }
            set
            {
                columnCount = GetColumnCountAfterCheck(value);
            }
        }

        private int minesCount;
        public int MinesCount
        {
            get
            {
                return minesCount;
            }
            set
            {
                minesCount = GetMinesCountAfterCheck(value, RowCount, ColumnCount);
            }
        }

        public bool IsPossibleMarkQuestion { get; set; }

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

        private int GetRowCountAfterCheck(int rowCount)
        {
            if (rowCount < GameLogicConstants.CustomLevelRowCountMin)
            {
                return GameLogicConstants.CustomLevelRowCountMin;
            }
            else if (rowCount > GameLogicConstants.CustomLevelRowCountMax)
            {
                return GameLogicConstants.CustomLevelRowCountMax;
            }

            return rowCount;
        }

        private int GetColumnCountAfterCheck(int columnCount)
        {
            if (columnCount < GameLogicConstants.CustomLevelColumnCountMin)
            {
                return GameLogicConstants.CustomLevelColumnCountMin;
            }
            else if (columnCount > GameLogicConstants.CustomLevelColumnCountMax)
            {
                return GameLogicConstants.CustomLevelColumnCountMax;
            }

            return columnCount;
        }

        private int GetMinesCountAfterCheck(int minesCount, int rowCount, int columnCount)
        {
            int minMinesCount = GameLogicConstants.CustomLevelMinesCountMin;
            int maxMinesCount = rowCount * columnCount - 1;

            if (minesCount < minMinesCount)
            {
                return minMinesCount;
            }
            else if (minesCount > maxMinesCount)
            {
                return maxMinesCount;
            }

            return minesCount;
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
