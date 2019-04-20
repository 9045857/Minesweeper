using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Logic;
using System.Drawing;
using System.Windows.Input;

namespace Minesweeper.Gui
{
    class GameGraphics
    {
        private readonly BitmapsResources bitmapsResources = new BitmapsResources();

        private  int rowCount;
        private  int columnCount;
        private  int minesCount;

        private GameLogic gameLogic;

        private CellDraw[,] cells;

        private PictureBox smileButtonImage;
        private PictureBox timeImage;
        private PictureBox minesCountImage;

        private Timer gameTimer;
        int currentGameTime;
        private readonly int maxTime = 999;

        private int panelsWidth;

        private bool isMouseLeftButtonDown;
        private bool isMouseRightButtonDown;
        List<Cell> cellsNearRightLeftMouseButtons = new List<Cell>();

        public GameGraphics(int rowCount, int columnCount, int minesCount)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.minesCount = minesCount;

            currentGameTime = 0;

            gameLogic = new GameLogic(rowCount, columnCount, minesCount);
        }

        private void ClearAndDrawStartPanelGame()
        {
            Panel panel = cells[0, 0].Parent as Panel;
            panel.Controls.Clear();

            DrawStartGamePanel(panel);
        }

        private void RestartGame(int rowCount, int columnCount, int minesCount)
        {
            if (this.rowCount == rowCount && this.columnCount == columnCount && this.minesCount == minesCount)
            {
                RestartGame();
                return;
            }

            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.minesCount = minesCount;

            gameLogic.RestartGame(rowCount, columnCount, minesCount);
            RestartDislays();

            ClearAndDrawStartPanelGame();            
        }

        private void DrawStartGamePanel(Panel gamePanel)
        {
            cells = new CellDraw[rowCount, columnCount];

            Bitmap cellStart = bitmapsResources.cellStart;
            int length = cellStart.Height;

            int panelHeight = rowCount * length;
            int panelWidth = columnCount * length;

            int onePixel = 1;

            int additionToPanelWidthForGoodDesign = 28;
            int additionToPanelHeightForGoodDesign = 127;

            gamePanel.Parent.Width = panelWidth + additionToPanelWidthForGoodDesign;
            gamePanel.Parent.Height = panelHeight + additionToPanelHeightForGoodDesign;

            gamePanel.Height = panelHeight + gamePanel.Margin.Left + onePixel;
            gamePanel.Width = panelWidth + gamePanel.Margin.Top + onePixel;

            this.panelsWidth = gamePanel.Width;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    int xRight = j * length;
                    int yTop = i * length;

                    CellDraw cellDraw = new CellDraw();

                    cellDraw.Parent = gamePanel;
                    cellDraw.Location = new Point(xRight, yTop);
                    cellDraw.Height = length;
                    cellDraw.Width = length;
                    cellDraw.Name = "CellPictureBox_" + i + "_" + j;// нужно ли вообще имя?
                    cellDraw.Image = cellStart;

                    cellDraw.rowIndex = i;
                    cellDraw.columnIndex = j;


                    //cellDraw.MouseMove += new MouseEventHandler(CellPictureBox_MouseMove);
                    //cellDraw.MouseClick += new MouseEventHandler(CellcellDraw_MouseClick);

                    //cellDraw.MouseLeave += new EventHandler(CellPictureBox_MouseLeave);
                    cellDraw.MouseUp += new MouseEventHandler(CellPictureBox_MouseUp);
                    cellDraw.MouseDown += new MouseEventHandler(CellPictureBox_MouseDown);

                    cells[i, j] = cellDraw;
                }
            }
        }

        private void RestartDislays()
        {
            int startTime = 0;
            timeImage.Image = GetBitmapNumericDisplay(startTime);

            minesCountImage.Image = GetBitmapNumericDisplay(minesCount);

            gameTimer.Enabled = false;
            currentGameTime = 0;
        }

        private void RestartGame()
        {
            gameLogic.RestartGame();
            RestartDislays();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    cells[i, j].Image = bitmapsResources.cellStart;
                }
            }
        }



        private void PressCellsList(List<Cell> cellsList)//TODO переименовать
        {
            if (cellsList.Count != 0)
            {
                foreach (Cell element in cellsList)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    if (element.IsPressed)
                    {
                        switch (element.markOnBottom)
                        {
                            case Cell.MarkOnBottomCell.Mine:
                                cells[rowIndex, columnIndex].Image = bitmapsResources.mine;
                                break;

                            case Cell.MarkOnBottomCell.MineBombed:
                                cells[rowIndex, columnIndex].Image = bitmapsResources.mineBombed;
                                break;

                            case Cell.MarkOnBottomCell.MineError:
                                cells[rowIndex, columnIndex].Image = bitmapsResources.mineError;
                                break;

                            case Cell.MarkOnBottomCell.MineNearCount:
                                int minesCountBitmapIndex = element.MineNearCount;
                                cells[rowIndex, columnIndex].Image = bitmapsResources.minesNearCount[minesCountBitmapIndex];
                                break;
                        }
                    }
                    else
                    {
                        switch (element.markOnTop)
                        {
                            case Cell.MarkOnTopCell.Flag:
                                cells[rowIndex, columnIndex].Image = bitmapsResources.flag;
                                break;
                        }
                    }
                }
            }
        }

        private void CellPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!gameLogic.IsGameContinue)
            {
                return;
            }

            int rowIndex = (sender as CellDraw).rowIndex;
            int columnIndex = (sender as CellDraw).columnIndex;

            if (isMouseLeftButtonDown && isMouseRightButtonDown)
            {
                SetMouseButtonsDownFalse();
                PressCellsNearRightLeftMouseButtonsUp(gameLogic.cells[rowIndex, columnIndex]);

                SetTimerFalseIfGameFinish();
                DrawSmileButtonIfCellUp();
            }
            else if (e.Button == MouseButtons.Left)
            {
                SetMouseButtonsDownFalse();

                if (gameLogic.cells[rowIndex, columnIndex].markOnTop != Cell.MarkOnTopCell.Flag)
                {
                    SetTimerTrueIfGameBegin();

                    List<Cell> pressingCells = gameLogic.GetOpenCellsAfterPress(rowIndex, columnIndex);
                    PressCellsList(pressingCells);

                    SetTimerFalseIfGameFinish();
                    SetRemainigMinesCountIfGameOver();

                    DrawSmileButtonIfCellUp();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                SetMouseButtonsDownFalse();
                SetTimerFalseIfGameFinish();
                DrawSmileButtonIfCellUp();
            }
        }

        private void SetTimerTrueIfGameBegin()
        {
            if (!gameLogic.AreMinesSet)
            {
                gameTimer.Enabled = true;
            }
        }

        private void SetTimerFalseIfGameFinish()
        {
            if (!gameLogic.IsGameContinue)
            {
                gameTimer.Enabled = false;
            }
        }

        private void SetMouseButtonsDownFalse()
        {
            isMouseLeftButtonDown = false;
            isMouseRightButtonDown = false;
        }

        private int GetMarkedMinesNearCell(Cell cell)
        {
            int rowIndex = cell.RowIndex;
            int columnIndex = cell.ColIndex;

            int indentFromInnerCell = 1;
            int borderCorrection = 1;

            int starRowIndex = rowIndex - indentFromInnerCell < 0 ? 0 : rowIndex - indentFromInnerCell;
            int endRowIndex = rowIndex + indentFromInnerCell == rowCount ? rowCount - borderCorrection : rowIndex + indentFromInnerCell;

            int starColIndex = columnIndex - indentFromInnerCell < 0 ? 0 : columnIndex - indentFromInnerCell;
            int endColumnIndex = columnIndex + indentFromInnerCell == columnCount ? columnCount - borderCorrection : columnIndex + indentFromInnerCell;

            int minesMarkedAroundCount = 0;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColumnIndex; j++)
                {
                    if (gameLogic.cells[i, j].markOnTop == Cell.MarkOnTopCell.Flag)
                    {
                        minesMarkedAroundCount++;
                    }
                }
            }

            return minesMarkedAroundCount;
        }

        private void PressCellsNearRightLeftMouseButtonsUp(Cell cell)
        {
            if (cellsNearRightLeftMouseButtons.Count != 0 && GetMarkedMinesNearCell(cell) == cell.MineNearCount)
            {
                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    List<Cell> pressingCells = gameLogic.GetOpenCellsAfterPress(rowIndex, columnIndex);

                    PressCellsList(pressingCells);
                }

                SetRemainigMinesCountIfGameOver();

                cellsNearRightLeftMouseButtons.Clear();
            }
            else if (cellsNearRightLeftMouseButtons.Count != 0)
            {
                foreach (Cell element in cellsNearRightLeftMouseButtons)
                {
                    int rowIndex = element.RowIndex;
                    int columnIndex = element.ColIndex;

                    DrawOnTopCellAfterMouseUp(cells[rowIndex, columnIndex], element);
                }

                cellsNearRightLeftMouseButtons.Clear();
            }
        }

        private void SetRemainigMinesCountIfGameOver()
        {
            if (!gameLogic.IsGameContinue && !gameLogic.isExploded)
            {
                int remaingMines = 0;
                minesCountImage.Image = GetBitmapNumericDisplay(remaingMines);
            }
        }

        private void PressCellsNearRightLeftMouseButtonsDown(Cell cell)
        {
            int rowIndex = cell.RowIndex;
            int columnIndex = cell.ColIndex;

            int indentFromInnerCell = 1;
            int borderCorrection = 1;

            int starRowIndex = rowIndex - indentFromInnerCell < 0 ? 0 : rowIndex - indentFromInnerCell;
            int endRowIndex = rowIndex + indentFromInnerCell == rowCount ? rowCount - borderCorrection : rowIndex + indentFromInnerCell;

            int starColIndex = columnIndex - indentFromInnerCell < 0 ? 0 : columnIndex - indentFromInnerCell;
            int endColIndex = columnIndex + indentFromInnerCell == columnCount ? columnCount - borderCorrection : columnIndex + indentFromInnerCell;

            for (int i = starRowIndex; i <= endRowIndex; i++)
            {
                for (int j = starColIndex; j <= endColIndex; j++)
                {
                    if (!gameLogic.cells[i, j].IsPressed && gameLogic.cells[i, j].markOnTop != Cell.MarkOnTopCell.Flag)
                    {
                        cellsNearRightLeftMouseButtons.Add(gameLogic.cells[i, j]);

                        if (gameLogic.cells[i, j].markOnTop == Cell.MarkOnTopCell.Question)
                        {
                            cells[i, j].Image = bitmapsResources.questionPressCell;
                        }
                        else
                        {
                            cells[i, j].Image = bitmapsResources.minesNearCount[0];
                        }
                    }
                }
            }
        }

        private void DrawRemainingMinesCountAfterMarkOnDispley()
        {
            int remainingMinesCountAfterMark = gameLogic.MinesCount - gameLogic.MarkedMinesCount;
            minesCountImage.Image = GetBitmapNumericDisplay(remainingMinesCountAfterMark);
        }

        private void CellPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!gameLogic.IsGameContinue)
            {
                return;
            }

            int rowIndex = (sender as CellDraw).rowIndex;
            int columnIndex = (sender as CellDraw).columnIndex;
            Cell cell = gameLogic.cells[rowIndex, columnIndex];

            if (e.Button == MouseButtons.Left)
            {
                if (cell.IsPressed && cell.MineNearCount != 0)
                {
                    isMouseLeftButtonDown = true;

                    if (isMouseRightButtonDown)
                    {
                        DrawSmileButtonIfCellDown();
                        PressCellsNearRightLeftMouseButtonsDown(cell);
                    }
                }
                else if (!cell.IsPressed && cell.markOnTop != Cell.MarkOnTopCell.Flag)
                {
                    DrawSmileButtonIfCellDown();
                    DrawOnBottomCellAfterMouseDown(sender as CellDraw, cell);
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                if (cell.IsPressed && cell.MineNearCount != 0)
                {
                    isMouseRightButtonDown = true;

                    if (isMouseLeftButtonDown)
                    {
                        DrawSmileButtonIfCellDown();
                        PressCellsNearRightLeftMouseButtonsDown(cell);
                    }
                }
                else if (!cell.IsPressed)
                {
                    gameLogic.Mark(cell);
                    DrawOnTopCellAfterMouseUp((sender as CellDraw), cell);
                    DrawRemainingMinesCountAfterMarkOnDispley();
                }
            }
        }

        private void DrawOnBottomCellAfterMouseDown(CellDraw cellDraw, Cell cell)
        {
            switch (cell.markOnTop)
            {
                case Cell.MarkOnTopCell.Question:
                    cellDraw.Image = bitmapsResources.questionPressCell;
                    break;

                case Cell.MarkOnTopCell.Empty:
                    cellDraw.Image = bitmapsResources.minesNearCount[0];
                    break;
            }
        }

        private void DrawOnTopCellAfterMouseUp(CellDraw cellDaw, Cell cell)
        {
            switch (cell.markOnTop)
            {
                case Cell.MarkOnTopCell.Flag:
                    cellDaw.Image = bitmapsResources.flag;
                    break;

                case Cell.MarkOnTopCell.Question:
                    cellDaw.Image = bitmapsResources.question;
                    break;

                case Cell.MarkOnTopCell.Empty:
                    cellDaw.Image = bitmapsResources.cellStart;
                    break;
            }
        }

        private Bitmap GetBitmapNumericDisplay(int number)
        {
            int numbersCount = 3;
            int numberWidth = bitmapsResources.numbers[0].Width;
            int numberHeight = bitmapsResources.numbers[0].Height;

            int bitmapWidth = numbersCount * numberWidth;
            int bitmapHeight = numberHeight;

            Bitmap resultBitmap = new Bitmap(bitmapWidth, bitmapHeight);

            int minNumber = -99;
            int maxNumber = 999;

            if (number < minNumber || number > maxNumber)
            {
                Bitmap minusBitmap = bitmapsResources.clockMinus;

                using (Graphics graphics = Graphics.FromImage(resultBitmap))
                {
                    graphics.DrawImage(minusBitmap, new Rectangle(0, 0, numberWidth, numberHeight));
                    graphics.DrawImage(minusBitmap, new Rectangle(numberWidth, 0, numberWidth, numberHeight));
                    graphics.DrawImage(minusBitmap, new Rectangle(numberWidth + numberWidth, 0, numberWidth, numberHeight));
                }

                return resultBitmap;
            }

            int hundred = 100;
            int ten = 10;
            int hundredRank = number / hundred;
            int tenRank = (number - hundredRank * hundred) / ten;
            int unitRank = number - hundredRank * hundred - tenRank * ten;//TODO check this on correct

            Bitmap bitmapHandredRank;
            Bitmap bitmapTenRank;
            Bitmap bitmapUnitRank;

            if (number >= 0)
            {
                bitmapHandredRank = bitmapsResources.numbers[hundredRank];
                bitmapTenRank = bitmapsResources.numbers[tenRank];
                bitmapUnitRank = bitmapsResources.numbers[unitRank];
            }
            else
            {
                bitmapHandredRank = bitmapsResources.clockMinus;
                bitmapTenRank = bitmapsResources.numbers[Math.Abs(tenRank)];
                bitmapUnitRank = bitmapsResources.numbers[Math.Abs(unitRank)];
            }

            using (Graphics graphics = Graphics.FromImage(resultBitmap))
            {
                graphics.DrawImage(bitmapHandredRank, new Rectangle(0, 0, numberWidth, numberHeight));
                graphics.DrawImage(bitmapTenRank, new Rectangle(numberWidth, 0, numberWidth, numberHeight));
                graphics.DrawImage(bitmapUnitRank, new Rectangle(numberWidth + numberWidth, 0, numberWidth, numberHeight));
            }

            return resultBitmap;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (currentGameTime >= maxTime)
            {
                gameTimer.Enabled = false;
                gameLogic.FinishGame();

                return;
            }

            currentGameTime++;
            timeImage.Image = GetBitmapNumericDisplay(currentGameTime);
        }

        private void DrawSmileButtonIfCellDown()
        {
            smileButtonImage.Image = bitmapsResources.smileButtonAttention;
        }

        private void DrawSmileButtonIfCellUp()
        {
            if (!gameLogic.isExploded)
            {
                smileButtonImage.Image = bitmapsResources.smileButton;
            }
            else
            {
                smileButtonImage.Image = bitmapsResources.smileButtonCry;
            }
        }

        private void DrawInfoPanel(Panel infoPanel, PictureBox smileButtonImage, PictureBox minesCountImage, PictureBox timeImage, Timer timer)
        {
            infoPanel.Width = panelsWidth;

            this.smileButtonImage = smileButtonImage;
            this.smileButtonImage.Image = bitmapsResources.smileButton;
            smileButtonImage.MouseUp += new MouseEventHandler(SmileButtonPictureBox_MouseUp);
            smileButtonImage.MouseDown += new MouseEventHandler(SmileButtonPictureBox_MouseDown);

            this.timeImage = timeImage;
            int startTime = 0;
            this.timeImage.Image = GetBitmapNumericDisplay(startTime);

            this.minesCountImage = minesCountImage;
            this.minesCountImage.Image = GetBitmapNumericDisplay(minesCount);

            gameTimer = timer;
            gameTimer.Interval = 1000;
            gameTimer.Enabled = false;
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
        }

        private void SmileButtonPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                smileButtonImage.Image = bitmapsResources.smileButton;
                RestartGame();
            }
        }

        private void SmileButtonPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                smileButtonImage.Image = bitmapsResources.smileButtonPressed;
            }
        }

        public void DrawStartArea(Panel gamePanel)
        {
            DrawStartGamePanel(gamePanel);
        }

        public void DrawInfoArea(Panel infoPanel, PictureBox smileButtonImage, PictureBox minesCountImage, PictureBox timeImage, Timer timer)
        {
            DrawInfoPanel(infoPanel, smileButtonImage, minesCountImage, timeImage, timer);
        }
    }
}
