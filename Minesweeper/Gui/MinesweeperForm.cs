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
    public partial class mainForm : Form
    {
        private BitmapsResources bitmaps = new BitmapsResources();

        private int currentRowIndex;
        private int currentColIndex;



        public mainForm()
        {
            InitializeComponent();
        }


       // private void DrawStartArea()
       // {
       //     int rowCount = 5;
       //     int colCount = 7;

       //     //  MineswepperGame.DrawStartField(panel3, rowCount, colCount);

       //     int mineCount = 3;

       //     GameLogic gameField = new GameLogic(rowCount, colCount, mineCount);

       ////ERROr     MineswepperGame game = new MineswepperGame(gameField.cells, mineCount, gameAreaPanel);
       // }


        //private void panel3_MouseClick(object sender, MouseEventArgs e)
        //{
        //    OnMouseClickGameArea();
        //}

        //private void OnMouseClickGameArea()
        //{
        //    //string text=   Cursor.Position.X + ":" + Cursor.Position.Y;

        //    //this.Text = text;

        //    //Point point = gameAreaPanel.PointToClient(Cursor.Position);
        //    //Text = point.ToString();

        //}

        //private void panel3_Move(object sender, EventArgs e)
        //{
        //}

        //private void panel3_MouseMove(object sender, MouseEventArgs e)
        //{


        //}

        private GameGraphics mineswepperGame;

        private void MainForm_Load(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            mineswepperGame = new GameGraphics(10, 15, 16);//начинающий
            mineswepperGame.DrawStartArea(panelGame, panelInfo, pictureBoxSmileButton, pictureBoxMinesCount, pictureBoxTime);
        }

        private void pictureBoxSmileButton_Click(object sender, EventArgs e)
        {           
            NewMethod();
        }

        //private void pictureBox1_Click(object sender, EventArgs e)
        //{




        //}





        //private void DrawStartPanelWithCells(Panel panel, int colCount, int rowCount)
        //{
        //    BitmapsResources bitmaps = new BitmapsResources();

        //    int cellLength = GraphicsConstants.CellLengthInPixels;
        //    int nextPixelForStartPoint = 1;

        //    int controlWidth = colCount * (cellLength + nextPixelForStartPoint) - nextPixelForStartPoint + panel.Margin.Right + panel.Margin.Left;
        //    int controlHeight = rowCount * (cellLength + nextPixelForStartPoint) - nextPixelForStartPoint + panel.Margin.Bottom + panel.Margin.Top;

        //    panel.Width = controlWidth;
        //    panel.Height = controlHeight;

        //    Bitmap bitmap = bitmaps.cellStart;

        //    Graphics graphics = panel.CreateGraphics();

        //    for (int i = 0; i < rowCount; i++)
        //    {
        //        for (int j = 0; j < colCount; j++)
        //        {
        //            int yTop = i * (cellLength + nextPixelForStartPoint);
        //            int xRight = j * (cellLength + nextPixelForStartPoint);

        //            graphics.DrawImage(bitmap, xRight, yTop);
        //        }
        //    }

        //    graphics.Dispose();
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{

        //}

        //private void gameAreaPanel_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{

        //}
    }
}
