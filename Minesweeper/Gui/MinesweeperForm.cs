using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

            Graphics graphics = panel1.CreateGraphics();
            graphics.DrawRectangle(new Pen(Color.Red), 10, 10, 11, 19);
            graphics.Dispose();

            Button button = new  ButtonCell();
            button.Location =  new Point(30,30);
            button.Size = new Size(15,15);
            button.Parent = panel1;

         //   button.Dispose();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 200, 300));
            myBrush.Dispose();
            formGraphics.Dispose();

            CellDraw cellDraw = new CellDraw(panel3,0,0,1,1, new BitmapsResources());
            cellDraw.DrawCell();





        }
    }
}
