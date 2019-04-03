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
    class CellDraw
    {
        private int xRight;
        private int yTop;

        private Control control;

        private readonly Cell cell;

        private readonly int length = GraphicsConstants.CellLengthInPixels;//35 пиксел
        private BitmapsResources bitmaps;

        public CellDraw(Cell cell, Control control, BitmapsResources bitmapsResources)
        {
            this.xRight = GetTopLeftCoordinate(cell.ColIndex, length);
            this.yTop = GetTopLeftCoordinate(cell.RowIndex, length);

            this.control = control;

            this.cell = cell;

            this.bitmaps = bitmapsResources;

            //  DrawUpCellBorder();
        }

        private static int GetTopLeftCoordinate(int index, int length)
        {
            int nextPixelAfterPreviewCell = 1;
            return index * (length + nextPixelAfterPreviewCell);
        }

        public static void DrawStartCell(Control control, int rowIndex, int colIndex, int length, BitmapsResources bitmaps)
        {
            int yTop = GetTopLeftCoordinate(rowIndex, length);
            int xRight = GetTopLeftCoordinate(colIndex, length);

            Graphics graphics;
            graphics = control.CreateGraphics();

            Bitmap image = bitmaps.cellStart;
            graphics.DrawImage(image, new Point(xRight, yTop));

            image.Dispose();
            graphics.Dispose();
        }

        private void DrawCellImage(Bitmap bitmap)
        {
            Graphics graphics = control.CreateGraphics();
            graphics.DrawImage(bitmap, new Point(xRight, yTop));
            graphics.Dispose();
        }
        
        public void DrawFlag()
        {
            DrawCellImage(bitmaps.flag);
        }

        public void DrawQuestion()
        {
            DrawCellImage(bitmaps.question);
        }

        public void DrawMine()
        {
            DrawCellImage(bitmaps.mine);
        }

        public void DrawBombedMine()
        {
            DrawCellImage(bitmaps.mineBombed);
        }
        
        public void DrawMineFalse()
        {
            DrawCellImage(bitmaps.mineFalse);
        }

        public void DrawCellNearMinesCount(int number)
        {
            DrawCellImage(bitmaps.minesNearCount[number]);
        }

        public void DrawCell()
        {
            // DrawMine();
            // DrawFlag();
            // DrawQuestion();
            //  DrawMineRemoval();
          //  DrawCellNumber(0);

            //DrawUpCellWithBorder(borderDepth);
        }

    }
}
