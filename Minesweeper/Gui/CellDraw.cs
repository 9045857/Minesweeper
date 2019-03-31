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
        private int length = 21;
        private int rowIndex;
        private int colIndex;
        private Control control;

        public CellDraw(Control control, int xRight, int yTop, int rowIndex, int colIndex)
        {
            this.xRight = xRight;
            this.yTop = yTop;

            this.rowIndex = rowIndex;
            this.colIndex = colIndex;
            this.control = control;
        }


        //private void DrawCellWithBorder()
        //{
        //    int borderPixelsDepth = 2;

        //    SolidBrush sideBrush = new SolidBrush(control.BackColor);

        //    Pen penLeftTop = new Pen(Color.White, 1);
        //    Pen penRightBottom = new Pen(Color.Gray, 1);

        //    Graphics formGraphics;
        //    formGraphics = control.CreateGraphics();

        //    for (int i = 0; i < borderPixelsDepth; i++)
        //    {
        //        formGraphics.DrawLine(penLeftTop, xRight + i, yTop + i, xRight + i, yTop + length - i);
        //        formGraphics.DrawLine(penLeftTop, xRight + i, yTop + i, xRight + length - i, yTop + i);

        //        formGraphics.DrawLine(penRightBottom, xRight + length - i, yTop + i, xRight + length - i, yTop + length - i);
        //        formGraphics.DrawLine(penRightBottom, xRight + i, yTop + length - i, xRight + length - i, yTop + length - i);
        //    }

        //    int innerRectangleLength = length - 2 * borderPixelsDepth + 1;
        //    int innerRectangleX = xRight + borderPixelsDepth;
        //    int innerRectangleY = yTop + borderPixelsDepth;

        //    formGraphics.FillRectangle(sideBrush, new Rectangle(innerRectangleX, innerRectangleY, innerRectangleLength, innerRectangleLength));
        //    // formGraphics.DrawRectangle(new Pen(Color.SlateGray), new Rectangle(innerRectangleX, innerRectangleY, innerRectangleLength-1, innerRectangleLength-1));

        //    penLeftTop.Dispose();
        //    penRightBottom.Dispose();
        //    sideBrush.Dispose();
        //    formGraphics.Dispose();
        //}

        private void DrawUpCellWithBorder()
        {
            int borderPixelsDepth = 2;

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


        public void DrawCell()
        {
            //int borderDepth = 2;

            DrawMineCell();

            //DrawUpCellWithBorder(borderDepth);

            //SolidBrush sideBrush = new SolidBrush(control.BackColor);

            //Pen penLeftTop = new Pen(Color.Gray, 1);
            //Pen penRightBottom = new Pen(Color.White, 1);

            //Pen penLeftTop = new Pen(Color.White, 1);//TODO Нужны цвета панели
            //Pen penRightBottom = new Pen(Color.Gray, 1);//TODO Нужны цвета панели

            //DrawCellWithBorder(borderDepth, sideBrush, penLeftTop, penRightBottom);


        }


        private void DrawMine()
        {
            Graphics graphics = control.CreateGraphics();

            int halfCount = 2;

            //////float cellCenterPointX = xRight + length / halfCount;
            //////float cellCenterPointY = yTop + length / halfCount;

            //////int verticesCount = 8;
            //////double angleBetweenRaysToVertices = 360.0 / verticesCount * Math.PI / 180.0;

            //////int radiusLengthProportionRatioLength = 4;

            //////int radius =  length / radiusLengthProportionRatioLength;


            //////Point[] verticesPoints = new Point[verticesCount];

            //////double startAngel = angleBetweenRaysToVertices / 2;

            //////for (int i = 0; i < verticesCount; i++)
            //////{
            //////    verticesPoints[i].X = (int)Math.Round(cellCenterPointX + radius * Math.Sin(startAngel + angleBetweenRaysToVertices * i));
            //////    verticesPoints[i].Y = (int)Math.Round(cellCenterPointY + radius * Math.Cos(startAngel + angleBetweenRaysToVertices * i));

            //////  //  MessageBox.Show(verticesPoints[i].X.ToString() + "  " + verticesPoints[i].Y.ToString());

            //////}


            //////graphics.DrawPolygon(new Pen(Color.Black, 1), verticesPoints);

            ////SolidBrush sideBrush = new SolidBrush(Color.Black);

            //////graphics.FillPolygon(sideBrush, verticesPoints);


            SolidBrush mineBrush = new SolidBrush(Color.Black);

            int mineIndentFromBorderProportionRatioLength = 5;
            int mineIndentFromBorder = length / mineIndentFromBorderProportionRatioLength;

            int mineRectangleX = xRight + mineIndentFromBorder;
            int mineRectangleY = yTop + mineIndentFromBorder;

            int indentCount = 2;
            int mineLength = length - indentCount * mineIndentFromBorder;

            Rectangle mineRectangle = new Rectangle(mineRectangleX, mineRectangleY, mineLength, mineLength);

            graphics.FillEllipse(mineBrush, mineRectangle);

            int indentFromBorderProportionRatioLength = 6;
            int indentFromBorder = length / indentFromBorderProportionRatioLength;

            int penWidth = 1;
            Pen minePen = new Pen(Color.Black, penWidth);

            graphics.DrawLine(minePen, xRight + length / halfCount, yTop + indentFromBorder, xRight + length / halfCount, yTop + length - indentFromBorder);
            graphics.DrawLine(minePen, xRight + indentFromBorder, yTop + length / halfCount, xRight + length - indentFromBorder, yTop + length / halfCount);

            int diagonalLinesIndentProportionRatioLength = 4;
            int diagonalLinesIndent = length / diagonalLinesIndentProportionRatioLength;

            graphics.DrawLine(minePen, xRight + diagonalLinesIndent, yTop + diagonalLinesIndent, xRight + length - diagonalLinesIndent, yTop + length - diagonalLinesIndent);
            graphics.DrawLine(minePen, xRight + length - diagonalLinesIndent, yTop + diagonalLinesIndent, xRight + diagonalLinesIndent, yTop + length - diagonalLinesIndent);

            SolidBrush shineBrush = new SolidBrush(Color.White); 

            int shineLengthProportionRatioLength = 10;
            int shineLength = length / shineLengthProportionRatioLength;
            int indentInPixelsFromCenter = 1;

            int rectangleX = xRight + length / halfCount - indentInPixelsFromCenter - shineLength;
            int rectangleY = yTop + length / halfCount - indentInPixelsFromCenter - shineLength;

            Rectangle shineRectangle = new Rectangle(rectangleX, rectangleY, shineLength, shineLength);

            graphics.FillRectangle(shineBrush, shineRectangle);

            graphics.Dispose();
            mineBrush.Dispose();
            shineBrush.Dispose();
        }

        private void DrawMineRemoval()
        {
            Graphics graphic = control.CreateGraphics();

            int penPixelsWidth = 2;
            Pen minePen = new Pen(Color.Red, penPixelsWidth);

            int diagonalLinesIndentProportionRatioLength = 6;
            int diagonalLinesIndent = length / diagonalLinesIndentProportionRatioLength;

            graphic.DrawLine(minePen, xRight + diagonalLinesIndent, yTop + diagonalLinesIndent, xRight + length - diagonalLinesIndent, yTop + length - diagonalLinesIndent);
            graphic.DrawLine(minePen, xRight + length - diagonalLinesIndent, yTop + diagonalLinesIndent, xRight + diagonalLinesIndent, yTop + length - diagonalLinesIndent);

            minePen.Dispose();
            graphic.Dispose();
        }


        //int penPixelsBorderWidth = 1;
        //Pen penBorder = new Pen(Color.Black, penPixelsBorderWidth);

        //SolidBrush sideBrush = new SolidBrush(Color.Black);

        //graphic.FillRectangle(sideBrush, new Rectangle(xRight, yTop, length, length));

        //  graphic.DrawRectangle(penBorder, new Rectangle(xRight, yTop, length, length));


        //graphic.DrawLine(penBorder, xRight, yTop, xRight + length, yTop);
        //graphic.DrawLine(penBorder, xRight, yTop, xRight, yTop + length);


        //int partsCount = 6;
        //int deltaCoordinate = length / partsCount;

        //int[] coordinates = int[] deltaCoordinate;


        //int deltaCoordinate = length / partsCount;



        //Point[] points = {new Point() }

        ////int conturWidth = 1;
        ////Color borderColor = Color.FromArgb(255, 0, 0, 0);
        ////Color fillColor = Color.FromArgb(150, 0, 255, 0);
        ////Pen pen = new Pen(borderColor, conturWidth);
        ////SolidBrush Brush = new SolidBrush(fillColor);
        //////e.Graphics.DrawPolygon(pen, points);


        private void DrawMineCell()
        {
          // DrawPressedCellBorder();//вроде рисует

           DrawUpCellWithBorder();//в чем разница со следующим?
                                   // DrawCellWithBorder();// на удаление

          //  DrawMine();

          

            DrawNumber("5");

            //  DrawMineRemoval();

            //DrawBombedCell();
        }


        private Image ResizeImage(Image image, int newWidth, int newHeight)
        {
            Image result = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage((Image)result))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                graphics.Dispose();
            }
            return result;
        }

        private void DrawNumber(string number)
        {
            Graphics graphic = control.CreateGraphics();
                      
            int mineBorderIndentFromCellBorder = 3;
            int indentsCount = 2;
            int minePictuteLength = length - indentsCount * mineBorderIndentFromCellBorder;

            //pictureBox.Location = new Point (xRight + mineBorderIndentFromCellBorder,yTop+ mineBorderIndentFromCellBorder);
            //pictureBox.Height = minePictuteLength;
            //pictureBox.Width = minePictuteLength;

            //pictureBox.Parent = control;

            //pictureBox.Image= ResizeImage(Properties.Resources.mine,minePictuteLength,minePictuteLength);

            //Image image= ResizeImage(Properties.Resources.mine, minePictuteLength, minePictuteLength);


            //graphic.DrawImage(image,new Point(mineBorderIndentFromCellBorder, mineBorderIndentFromCellBorder));

            //pictureBox.Image= ResizeImage(Properties.Resources.mine,minePictuteLength,minePictuteLength);

            Bitmap image = Properties.Resources.flag;//question;//mine31;

            image.MakeTransparent();
            
            graphic.DrawImage(image, new Point(2, 2));

            graphic.Dispose();
            image.Dispose();
        }


        private void DrawPressedCellBorder()
        {
            SolidBrush cellBrush = new SolidBrush(control.BackColor);

            DrawBaseCell(cellBrush);

            cellBrush.Dispose();
        }

        private void DrawBombedCell()
        {
            SolidBrush cellBrush = new SolidBrush(Color.Red);

            DrawBaseCell(cellBrush);

            cellBrush.Dispose();

            DrawMine();

        }

        private void DrawBaseCell(SolidBrush cellBrush)
        {
            Graphics graphic = control.CreateGraphics();

            int penPixelsWidth = 1;
            Pen penBorder = new Pen(Color.Gray, penPixelsWidth);

            //   SolidBrush cellBrush = new SolidBrush(Color.Red);

            Rectangle cellRectangle = new Rectangle(xRight, yTop, length, length);

            graphic.FillRectangle(cellBrush, cellRectangle);

            //  graphic.DrawRectangle(penBorder, new Rectangle(xRight, yTop, length, length));

            graphic.DrawLine(penBorder, xRight, yTop, xRight + length, yTop);
            graphic.DrawLine(penBorder, xRight, yTop, xRight, yTop + length);

            penBorder.Dispose();
            graphic.Dispose();
        }
    }
}
