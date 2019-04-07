using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Logic;
using System.Drawing;

namespace Minesweeper.Gui
{
    class MineswepperGame
    {
        private readonly int cellLength = GraphicsConstants.CellLengthInPixels;

        private readonly int mineCount;
        private readonly Control control;

        private int rowCount;
        private int colCount;

        private readonly Cell[,] cells;
        private CellDraw[,] cellDraws;
        private readonly BitmapsResources bitmaps = new BitmapsResources();

        private int RestMinesCount { get; set; }
        private int CheckedMinesCount { get; set; }

        public MineswepperGame(Cell[,] cells, int mineCount, Control control)
        {
            this.rowCount = cells.GetLength(0);
            this.colCount = cells.GetLength(1);

            this.cells = cells;

            this.mineCount = mineCount;
            this.control = control;

            RestMinesCount = mineCount;
            CheckedMinesCount = 0;

            DrawStartField(control, rowCount, colCount);


            //   FillCellDraws();//TODO нужен другой метод заполнения массива клеток
        }


        public static void DrawStartField(Control control, int rowCount, int colCount)
        {
            int cellLength = GraphicsConstants.CellLengthInPixels;
            int nextPixelForStartPoint = 1;

            int controlWidth = colCount * (cellLength + nextPixelForStartPoint) - nextPixelForStartPoint + control.Margin.Right + control.Margin.Left;
            int controlHeight = rowCount * (cellLength + nextPixelForStartPoint) - nextPixelForStartPoint + control.Margin.Bottom + control.Margin.Top;

            control.Width = controlWidth;
            control.Height = controlHeight;
                        
            BitmapsResources bitmaps = new BitmapsResources();
            Bitmap bitmap = bitmaps.cellStart;

            Bitmap tempBmp = new Bitmap(controlWidth, controlHeight);

            Graphics grph = Graphics.FromImage(tempBmp);
            


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    int yTop = i * (cellLength + nextPixelForStartPoint);
                    int xRight = j * (cellLength + nextPixelForStartPoint);
                    grph.DrawImage(bitmap, xRight, yTop);
                }
            }

            control.CreateGraphics().DrawImage(tempBmp, 0, 0);
            bitmap.Dispose();
            tempBmp.Dispose();
        }






    }
}
