using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Minesweeper.Gui;
using System.Drawing.Drawing2D;

namespace Minesweeper.Gui
{
    class CellDraw
    {
        private int xRight;
        private int yTop;
        private int rowIndex;
        private int colIndex;
        private Control control;

        private int length = GraphicsConstants.CellLengthInPixels;//21 пиксел
        private BitmapsResources bitmaps;


        public CellDraw(Control control, int xRight, int yTop, int rowIndex, int colIndex, BitmapsResources bitmapsResources)
        {
            this.xRight = xRight;
            this.yTop = yTop;

            this.rowIndex = rowIndex;
            this.colIndex = colIndex;
            this.control = control;

            this.bitmaps = bitmapsResources;
        }

        public void DrawUpCellBorder()
        {
            int borderPixelsDepth = GraphicsConstants.CellBorderWidthInPixels;

            SolidBrush cellBrush = new SolidBrush(control.BackColor);

            int penPixelsWidth = 1;

            Pen penLeftTop = new Pen(Color.White, penPixelsWidth);
            Pen penRightBottom = new Pen(Color.Gray, penPixelsWidth);

            Graphics graphics;
            graphics = control.CreateGraphics();

            for (int i = 0; i < borderPixelsDepth; i++)
            {
                graphics.DrawLine(penLeftTop, xRight + i, yTop + i, xRight + i, yTop + length - i);
                graphics.DrawLine(penLeftTop, xRight + i, yTop + i, xRight + length - i, yTop + i);

                graphics.DrawLine(penRightBottom, xRight + length - i, yTop + i, xRight + length - i, yTop + length - i);
                graphics.DrawLine(penRightBottom, xRight + i, yTop + length - i, xRight + length - i, yTop + length - i);
            }

            graphics.FillRectangle(cellBrush, new Rectangle(xRight + borderPixelsDepth, yTop + borderPixelsDepth, length - 2 * borderPixelsDepth + 1, length - 2 * borderPixelsDepth + 1));

            penLeftTop.Dispose();
            penRightBottom.Dispose();
            cellBrush.Dispose();
            graphics.Dispose();
        }

        private void DrawColorCellDownBorder(SolidBrush cellBrush)
        {
            Graphics graphic = control.CreateGraphics();

            int penPixelsWidth = 1;
            Pen penBorder = new Pen(Color.Gray, penPixelsWidth);

            Rectangle cellRectangle = new Rectangle(xRight, yTop, length, length);

            graphic.FillRectangle(cellBrush, cellRectangle);

            graphic.DrawLine(penBorder, xRight, yTop, xRight + length, yTop);
            graphic.DrawLine(penBorder, xRight, yTop, xRight, yTop + length);

            penBorder.Dispose();
            graphic.Dispose();
        }

        public void DrawFlag()
        {
            DrawUpCellBorder();

            Bitmap image = bitmaps.flag;
            image.MakeTransparent();

            Graphics graphics = control.CreateGraphics();
            graphics.DrawImage(image, bitmaps.bitmapLeftTopPoint);
             
            graphics.Dispose();
        }

        public void DrawQuestion()
        {
            DrawUpCellBorder();

            Bitmap image = bitmaps.question;
            image.MakeTransparent();

            Graphics graphics = control.CreateGraphics();
            graphics.DrawImage(image, bitmaps.bitmapLeftTopPoint);
                        
            graphics.Dispose();
        }

        public void DrawMine()
        {
            SolidBrush cellBrush = new SolidBrush(control.BackColor);

            DrawColorCellDownBorder(cellBrush);

            Bitmap image = bitmaps.mine;
            image.MakeTransparent();

            Graphics graphics = control.CreateGraphics();
            graphics.DrawImage(image, bitmaps.bitmapLeftTopPoint);

            cellBrush.Dispose();
            graphics.Dispose();
        }

        public void DrawBombedMine()
        {
            SolidBrush cellBrush = new SolidBrush(Color.Red);

            DrawColorCellDownBorder(cellBrush);

            Bitmap image = bitmaps.mine;
            image.MakeTransparent();

            Graphics graphics = control.CreateGraphics();
            graphics.DrawImage(image, bitmaps.bitmapLeftTopPoint);

            cellBrush.Dispose();
            graphics.Dispose();
        }


        public void DrawMineRemoval()
        {
            SolidBrush cellBrush = new SolidBrush(control.BackColor);

            DrawColorCellDownBorder(cellBrush);

            Bitmap image = bitmaps.mineRemovel;
            image.MakeTransparent();

            Graphics graphics = control.CreateGraphics();
            graphics.DrawImage(image, bitmaps.bitmapLeftTopPoint);

            cellBrush.Dispose();
            graphics.Dispose();
        }

        private void DrawCellNumber(int number)
        {
            SolidBrush cellBrush = new SolidBrush(control.BackColor);

            DrawColorCellDownBorder(cellBrush);

            cellBrush.Dispose();

            if (number==0)
            {
                return;
            }

            int correctionCoefficient = 1;
            Bitmap image = bitmaps.bitmapsNumbers[number - correctionCoefficient];
            image.MakeTransparent(Color.White);

            Graphics graphics = control.CreateGraphics();
            graphics.DrawImage(image, bitmaps.bitmapLeftTopPoint);

            graphics.Dispose();
        }

        public void DrawCell()
        {
            // DrawMine();
            // DrawFlag();
            // DrawQuestion();
            //  DrawMineRemoval();
            DrawCellNumber(0);

            //DrawUpCellWithBorder(borderDepth);


        }

    }
}
