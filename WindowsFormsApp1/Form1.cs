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
            // ReDraw();

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

        private PictureBox[,] cellsPictures;

        private void button2_Click(object sender, EventArgs e)
        {
            int rowCount = Convert.ToInt16(textBox1.Text);
            int colCount = Convert.ToInt16(textBox2.Text);

            DrawStartArea(panel2, cellsPictures,rowCount,colCount);
        }

        private void DrawStartArea(Panel gamePanel, PictureBox[,] cellsPictures,int rowCount,int colCount)
        {
            cellsPictures = new PictureBox[rowCount, colCount];

            Bitmap image = WindowsFormsApp1.Properties.Resources.StartButton3;
            int length = image.Height;

            int panelHeight = rowCount * length;
            int panelWidth = colCount * length;

            //TODO долго рисует новое поле. Нужен алгоритм "мновенной" отрисовки поля
            // Попыка использовать промежутоную картинку-ширму не помогла или я не правильно ее использовал

            //Bitmap shieldImage = new Bitmap(panelWidth, panelHeight);

            //using (Graphics graphics = Graphics.FromImage(shieldImage))
            //{
            //    for (int i = 0; i < rowCount; i++)
            //    {
            //        for (int j = 0; j < colCount; j++)
            //        {
            //            int xRight = j * length;
            //            int yTop = i * length;

            //            graphics.DrawImage(image, xRight, yTop, length, length);
            //        }
            //    }
            //}
            
            //using (Graphics graphics = gamePanel.CreateGraphics())
            //{
                gamePanel.Height = panelHeight + gamePanel.Margin.Left + 1;
                gamePanel.Width = panelWidth + gamePanel.Margin.Top + 1;

            //    graphics.DrawImage(shieldImage, 0, 0, panelWidth, panelHeight);
            //}
            
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    int xRight = j * length;
                    int yTop = i * length;

                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Parent = gamePanel;
                    pictureBox.Location = new Point(xRight, yTop);
                    pictureBox.Height = length;
                    pictureBox.Width = length;
                    pictureBox.Name = "cellPictureBox" + i + "_" + j;
                    pictureBox.Image = image;

                    pictureBox.MouseMove += new MouseEventHandler(CellPictureBox_MouseMove);
                    pictureBox.MouseClick += new MouseEventHandler(CellPictureBox_MouseClick);
                    pictureBox.MouseLeave += new EventHandler(CellPictureBox_MouseLeave);

                    cellsPictures[i, j] = pictureBox;
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

            pictureBox2.Image = WindowsFormsApp1.Properties.Resources.attantionSmileButton31;

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

        private Bitmap[] clockNumbers = /*new Bitmap[10]*/
        {
            WindowsFormsApp1.Properties.Resources.clock0,
            WindowsFormsApp1.Properties.Resources.clock1,
            WindowsFormsApp1.Properties.Resources.clock2,
            WindowsFormsApp1.Properties.Resources.clock3,
            WindowsFormsApp1.Properties.Resources.clock4,
            WindowsFormsApp1.Properties.Resources.clock5,
            WindowsFormsApp1.Properties.Resources.clock6,
            WindowsFormsApp1.Properties.Resources.clock7,
            WindowsFormsApp1.Properties.Resources.clock8,
            WindowsFormsApp1.Properties.Resources.clock9
        };

        private Bitmap GetBitmapNumericDisplay(int number)//число не должно превышать 999
        {
            int hundred = 100;
            int ten = 10;
            int hundredRank = number / hundred;
            int tenRank = (number - hundredRank * hundred) / ten;
            int unitRank = number - hundredRank * hundred - tenRank * ten;

            int numbersCount = 3;
            int numberWidth = clockNumbers[0].Width;
            int numberHeigth = clockNumbers[0].Height;

            int bitmapWidth = numbersCount * numberWidth;
            int bitmapHeigth = numberHeigth;

            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeigth);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Bitmap bitmapHandredRank = new Bitmap(clockNumbers[hundredRank]);
                Bitmap bitmapTenRank = new Bitmap(clockNumbers[tenRank]);
                Bitmap bitmapUnitRank = new Bitmap(clockNumbers[unitRank]);

                // graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0,0,bitmapWidth,bitmapHeigth));

                graphics.DrawImage(bitmapHandredRank, new Rectangle(0, 0, numberWidth, numberHeigth));
                graphics.DrawImage(bitmapTenRank, new Rectangle(numberWidth, 0, numberWidth, numberHeigth));
                graphics.DrawImage(bitmapUnitRank, new Rectangle(numberWidth + numberWidth, 0, numberWidth, numberHeigth));
            }

            return bitmap;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap clock = GetBitmapNumericDisplay(501);
            pictureBox5.ClientSize = clock.Size;
            pictureBox5.Image = clock;

            pictureBox6.Size = clock.Size;
            pictureBox6.Image = GetBitmapNumericDisplay(0);

            int minTime = Convert.ToInt16(textBox3.Text);
            int maxTime = Convert.ToInt16(textBox4.Text);

            FillStartTimeData(minTime, maxTime);

            timer1.Enabled = false;
            timer1.Enabled = true;
        }

        private int currentTime;
        private int minTime;
        private int maxTime;

        private void FillStartTimeData(int minTime, int maxTime)
        {
            this.minTime = minTime;
            this.maxTime = maxTime;

            currentTime = minTime;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentTime >= maxTime)
            {
                timer1.Enabled = false;
                return;
            }

            currentTime++;

            pictureBox6.Image = GetBitmapNumericDisplay(currentTime);
        }
    }
}
