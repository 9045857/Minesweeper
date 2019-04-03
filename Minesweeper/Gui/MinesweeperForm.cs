using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Logic;


namespace Minesweeper.Gui
{
    public partial class MainForm : Form
    {
        private BitmapsResources bitmaps = new BitmapsResources();

        private int currentRowIndex;
        private int currentColIndex;



        public MainForm()
        {
            InitializeComponent();
        }
            

        private void DrawStartArea()
        {
            int rowCount = 5;
            int colCount = 7;

            //  MineswepperGame.DrawStartField(panel3, rowCount, colCount);

            int mineCount = 3;

            GameField gameField = new GameField(rowCount, colCount, mineCount);

            MineswepperGame game = new MineswepperGame(gameField.cells, mineCount, gameAreaPanel);
        }

      
        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClickGameArea();
        }

        private void OnMouseClickGameArea()
        {
            //string text=   Cursor.Position.X + ":" + Cursor.Position.Y;

            //this.Text = text;

            Point point = gameAreaPanel.PointToClient(Cursor.Position);
            Text = point.ToString();

        }

        private void panel3_Move(object sender, EventArgs e)
        {
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            //label1.Text = e.Location.X + ":" + e.Location.Y;

            int rowIndex = e.Location.Y / GraphicsConstants.CellLengthInPixels;
            int colIndex = e.Location.X / GraphicsConstants.CellLengthInPixels;

            label1.Text = rowIndex + " " + colIndex;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            tableLayoutPanel1.RowStyles[0].Height = 70;


            DrawStartArea();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BitmapsResources bitmaps = new BitmapsResources();

            int cellLength = GraphicsConstants.CellLengthInPixels;
            int nextPixelForStartPoint = 1;

            int colCount = 3;
            int rowCount = 5;

            int controlWidth = colCount * (cellLength + nextPixelForStartPoint)- nextPixelForStartPoint + panel1.Margin.Right + panel1.Margin.Left;
            int controlHeight = rowCount * (cellLength +nextPixelForStartPoint)- nextPixelForStartPoint + panel1.Margin.Bottom + panel1.Margin.Top;
                      
            panel1.Width = controlWidth;
            panel1.Height = controlHeight;

            Bitmap bitmap = bitmaps.cellStart;

            Graphics graphics=panel1.CreateGraphics();
            
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    int yTop= i * (cellLength + nextPixelForStartPoint);
                    int xRight = j * (cellLength + nextPixelForStartPoint);

                    graphics.DrawImage(bitmap,xRight,yTop);
                }
            }

            graphics.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
