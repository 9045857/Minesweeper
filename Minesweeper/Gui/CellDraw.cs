using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Minesweeper.Gui;
using System.Drawing.Drawing2D;
using Minesweeper.Logic;

namespace Minesweeper.Gui
{
    class CellDraw:PictureBox
    {
        //public PictureBox pictureBox;

        public int rowIndex;
        public int columnIndex;

        //public CellDraw(PictureBox pictureBox, int rowIndex, int columnIndex)
        //{
        //    this.pictureBox = pictureBox;
        //    this.rowIndex = rowIndex;
        //    this.columnIndex = columnIndex;
        //}
    }
}
