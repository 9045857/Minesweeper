using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ReDraw();

            pictureBox4.Image = WindowsFormsApp1.Properties.Resources.smileButton3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NewMethod()
        {
            int rowCount = Convert.ToInt16(textBox1.Text);
            int colCount = Convert.ToInt16(textBox2.Text);

            Bitmap image = WindowsFormsApp1.Properties.Resources.field12;
            int length = image.Height;
            int nextPixel = 0;

            //            MessageBox.Show(length.ToString());

            int panelHeight = rowCount * length;
            int panelWidth = colCount * length;

            panel1.Height = panelHeight + panel1.Margin.Right;
            panel1.Width = panelWidth + panel1.Margin.Bottom;

            Graphics graphics = panel1.CreateGraphics();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    int xRight = j * (length + nextPixel);
                    int yTop = i * (length + nextPixel);

                    graphics.DrawImage(image, xRight, yTop, length, length);
                }
            }

            graphics.Dispose();
        }

        private PictureBox[,] cellsPictures = new PictureBox[10, 10];

        private void button2_Click(object sender, EventArgs e)
        {
            ReDraw();
        }

        private void ReDraw()
        {
            int rowCount = Convert.ToInt16(textBox1.Text);
            int colCount = Convert.ToInt16(textBox2.Text);

            Bitmap image = WindowsFormsApp1.Properties.Resources.StartButton3;
            int length = image.Height;

            int panelHeight = rowCount * length;
            int panelWidth = colCount * length;

            panel2.Height = panelHeight + panel2.Margin.Left + 1;
            panel2.Width = panelWidth + panel2.Margin.Top + 1;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (cellsPictures[i, j] == null)
                    {
                        int xRight = j * length;
                        int yTop = i * length;

                        PictureBox pictureBox = new PictureBox();

                        pictureBox.Parent = panel2;
                        pictureBox.Location = new Point(xRight, yTop);
                        pictureBox.Height = length;
                        pictureBox.Width = length;
                        pictureBox.Name = "cellPictureBox" + i + "_" + j;
                        //  image.MakeTransparent(Color.Black);
                        pictureBox.Image = image;

                        pictureBox.MouseMove += new MouseEventHandler(CellPictureBox_MouseMove);
                        pictureBox.MouseClick += new MouseEventHandler(CellPictureBox_MouseClick);
                        pictureBox.MouseLeave += new EventHandler(CellPictureBox_MouseLeave);

                        cellsPictures[i, j] = pictureBox;
                    }
                    else
                    {
                        cellsPictures[i, j].Image = image;
                    }
                }
            }
        }

        private int cellLength = WindowsFormsApp1.Properties.Resources.field12.Width;
        private int xCoordinate;
        private int yCoordinate;

        private int cellIndex(int coordinate)
        {
            return coordinate / cellLength;
        }

        private void CellPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            xCoordinate = (Cursor.Position.X - Location.X - panel2.Location.X - 10);
            yCoordinate = (Cursor.Position.Y - Location.Y - panel2.Location.Y - 33);

            label4.Text = xCoordinate + " : " + yCoordinate;

            label3.Text = e.X + " : " + e.Y;

            (sender as PictureBox).Image = WindowsFormsApp1.Properties.Resources.field03;

            pictureBox2.Image= WindowsFormsApp1.Properties.Resources.attantionSmileButton31;

        }

        private void CellPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            int rowIndex = cellIndex(yCoordinate);
            int colIndex = cellIndex(xCoordinate);

            label5.Text = colIndex + " : " + rowIndex;
            cellsPictures[rowIndex, colIndex].Image = WindowsFormsApp1.Properties.Resources.field13;
        }

        private void CellPictureBox_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = WindowsFormsApp1.Properties.Resources.StartButton3;
            pictureBox2.Image = WindowsFormsApp1.Properties.Resources.smileButton31;

        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
             //   pictureBox4.Image = WindowsFormsApp1.Properties.Resources.smileButton3;


        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
           (sender as PictureBox).Image = WindowsFormsApp1.Properties.Resources.pressedSmileButton31;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as PictureBox).Image = WindowsFormsApp1.Properties.Resources.crySmileButton31;
        }
    }
}
