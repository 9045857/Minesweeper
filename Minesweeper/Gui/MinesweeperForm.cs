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
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



        }


        private void DrawStartArea()
        {
            int rowCount = 5;
            int colCount = 7;

            //  MineswepperGame.DrawStartField(panel3, rowCount, colCount);

            int mineCount = 3;

            GameField gameField = new GameField(rowCount, colCount, mineCount);

            MineswepperGame game = new MineswepperGame(gameField.cells, mineCount, panel3);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DrawStartArea();

            //System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            //System.Drawing.Graphics formGraphics;
            //formGraphics = this.CreateGraphics();
            //formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 200, 300));
            //myBrush.Dispose();
            //formGraphics.Dispose();

            //CellDraw cellDraw = new CellDraw(panel3,0,0,1,1, new BitmapsResources());
            //cellDraw.DrawCell();





        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClickGameArea();
        }

        private void OnMouseClickGameArea()
        {
            //string text=   Cursor.Position.X + ":" + Cursor.Position.Y;

            //this.Text = text;

            Point point = panel3.PointToClient(Cursor.Position);
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
    }
}
