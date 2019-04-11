using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    class Cell
    {
        public bool isMineInCellSet;

        public enum MarkOnTopCell
        {
            Empty = 0,
            Flag = 1,
            Question = 2
        }

        public MarkOnTopCell markOnTop;

        public int RowIndex { get; private set; }
        public int ColIndex { get; private set; }

        public Cell(int rowIndex, int colIndex)
        {
            isMineInCellSet = false;

            RowIndex = rowIndex;
            ColIndex = colIndex;

            IsPressed = false;

            markOnTop = MarkOnTopCell.Empty;

            //IsFlag = false;
            //IsQuestion = false;
        }

        public bool IsPressed { get; set; }// много используется в логике

        public bool IsMineHere { get; set; }

        public int MineNearCount { get; set; }
        // public int MineNearCheckedCount { get; set; }// в чем отличие?

        public void Mark()
        {
            if (!IsPressed)
            {
                switch (markOnTop)
                {
                    case MarkOnTopCell.Empty:
                        markOnTop = MarkOnTopCell.Flag;
                        break;

                    case MarkOnTopCell.Flag:
                        markOnTop = MarkOnTopCell.Question;
                        break;

                    case MarkOnTopCell.Question:
                        markOnTop = MarkOnTopCell.Empty;
                        break;
                }
            }
        }

        //public bool IsFlag { get; set; }

        //public bool IsQuestion { get; set; }
    }
}
