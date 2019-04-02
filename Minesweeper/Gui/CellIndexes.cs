using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Minesweeper.Gui
{
    class CellIndexes
    {
        public int RowIndex { get; private set; }
        public int ColIndex { get; private set; }

        public CellIndexes(Point point, int cellLength)
        {
            RowIndex = point.X / cellLength;
            ColIndex = point.Y / cellLength;
        }

        public override string ToString()
        {
            return RowIndex + "; " + ColIndex;
        }
    }
}
