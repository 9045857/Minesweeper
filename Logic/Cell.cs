namespace Logic
{
    public class Cell
    {
        public bool isMineInCellSet;

        public enum MarkOnBottomCell
        {
            MineNearCount = 0,
            Mine = 1,
            MineBombed = 2,
            MineError = 3,
            Question = 4,
            Empty = 5
        }

        public enum MarkOnTopCell
        {
            Empty = 0,
            Flag = 1,
            Question = 2
        }

        public MarkOnBottomCell markOnBottom;
        public MarkOnTopCell markOnTop;

        public int RowIndex { get; private set; }
        public int ColIndex { get; private set; }

        public Cell(int rowIndex, int colIndex)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;

            SetBeginConditions();
        }

        public void SetBeginConditions()
        {
            isMineInCellSet = false;
            IsPressed = false;
            markOnTop = MarkOnTopCell.Empty;
        }

        public bool IsPressed { get; set; }

        public bool IsMineHere { get; set; }

        public int MineNearCount { get; set; }
    }
}
