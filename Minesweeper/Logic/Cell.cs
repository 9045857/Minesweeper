using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    class Cell
    {
        private enum Status// TODO завязка на енум неправильная. Переделать.
        {
            Unpressed = 0,
            Pressed = 1,
            Flag = 2,
            Question = 3
        }

        private Status status;

        //public Cell()
        //{
        //    status = Status.Unpressed;
        //}

        public Cell(int rowIndex,int colIndex)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;

            //status = Status.Unpressed;

            IsPressed = false;
        }

        //public bool isUnpressed
        //{
        //    get
        //    {
        //        return status == Status.Unpressed;
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            status = Status.Unpressed;
        //        }
        //    }
        //}

        public bool IsPressed { get; set; }

        //public bool isPressed
        //{
        //    get
        //    {
        //        return status == Status.Pressed;
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            status = Status.Pressed;
        //        }
        //    }
        //}

        //public bool isFlag
        //{
        //    get
        //    {
        //        return status == Status.Flag;
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            status = Status.Flag;
        //        }
        //    }
        //}

        public bool isQuestion
        {
            get
            {
                return status == Status.Question;
            }
            set
            {
                if (value)
                {
                    status = Status.Question;
                }
            }
        }

        public bool IsMineHere { get; set; }

        public bool IsFlag { get; set; }

        public int MineNearCount { get; set; }

        public int MineNearCheckedCount { get; set; }

        public int RowIndex { get; private set; }

        public int ColIndex { get; private set; }
    }
}
