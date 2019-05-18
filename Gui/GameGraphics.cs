using Logic;
using System.Drawing;
using System.Windows.Forms;

namespace Gui
{
    public class GameGraphics
    {
        private GameLogic gameLogic;
        private GameAreaGraphics gameAreaGraphics;
        private GameInfoGraphics gameInfoGraphics;
        private HighScoreDialog highScoreDialog;

        private FinalDynamicTimer dynamicTimer;
        private delegate void CallTimer();

        private PictureBox dynamicImage;
        private BitmapsResources bitmapsResources;

        private bool isWinWithHighScore;
        private int gameTime;
        private bool isWin;
        private bool isLoss;

        public GameGraphics
        (
            GameParameters gameParameters,
            PictureBox gameAreaPictureBox,
            BitmapsResources bitmapsResources,
            PictureBox pictureBoxSmileButton,
            PictureBox pictureBoxMinesCount,
            PictureBox pictureBoxTime,
            HighScoreDialog highScoreDialog
        )
        {
            gameLogic = new GameLogic(gameParameters);
            gameLogic.OnWinWithHighScore += OnWinWithHighScore;

            gameAreaGraphics = new GameAreaGraphics(gameAreaPictureBox, gameLogic, bitmapsResources);
            gameAreaGraphics.OnDrawExplodedGameArea += DrawDynamicLossFinal;
            gameAreaGraphics.OnDrawWinGameArea += DrawDynamicWinFinal;

            gameInfoGraphics = new GameInfoGraphics(gameLogic, gameAreaGraphics, bitmapsResources, pictureBoxSmileButton, pictureBoxMinesCount, pictureBoxTime);

            this.highScoreDialog = highScoreDialog;
            this.highScoreDialog.OnSetUserName += AddHighScore;

            dynamicImage = new PictureBox();
            dynamicImage.Parent = gameAreaPictureBox;
            dynamicImage.Dock = DockStyle.Fill;
            dynamicImage.SizeMode = PictureBoxSizeMode.Zoom;
            dynamicImage.BackColor = Color.Transparent;
            dynamicImage.Visible = false;

            this.bitmapsResources = bitmapsResources;

            dynamicTimer = new FinalDynamicTimer();
            dynamicTimer.OnStartAnimation += DoDynamic;
            dynamicTimer.OnStopAnimation += StopDynamic;
        }

        private void DoDynamic()
        {
            if (dynamicImage.InvokeRequired)
            {
                var d = new CallTimer(DoDynamic);
                dynamicImage.Invoke(d);
            }
            else
            {
                if (isWin)
                {
                    dynamicImage.Image = bitmapsResources.dynamicWin;
                    isWin = false;
                }
                else if (isLoss)
                {
                    dynamicImage.Image = bitmapsResources.dynamicLoss;
                    isLoss = false;
                }

                dynamicImage.Visible = true;
            }
        }

        private void StopDynamic()
        {
            if (dynamicImage.InvokeRequired)
            {
                var d = new CallTimer(StopDynamic);
                dynamicImage.Invoke(d);
            }
            else
            {
                if (isWinWithHighScore)
                {
                    this.highScoreDialog.GameTime = gameTime;
                    this.highScoreDialog.ShowDialog();
                    isWinWithHighScore = false;
                }

                dynamicImage.Visible = false;
            }
        }

        private void DrawDynamicWinFinal()
        {
            isWin = true;

            int beforAnimationTime = 200;
            int animationTime = 3000;

            dynamicTimer.Start(beforAnimationTime, animationTime);
        }

        private void DrawDynamicLossFinal()
        {
            isLoss = true;

            int beforAnimationTime = 200;
            int animationTime = 2300;

            dynamicTimer.Start(beforAnimationTime, animationTime);
        }

        public void SetCellTopColor(Color color)
        {
            gameAreaGraphics.CellTopColor = color;
        }

        private void OnWinWithHighScore(int gameTime)
        {
            isWinWithHighScore = true;
            this.gameTime = gameTime;
        }

        private void AddHighScore()
        {
            string userName = highScoreDialog.UserName;
            int time = highScoreDialog.GameTime;
            int rowCount = gameLogic.RowCount;
            int columnCount = gameLogic.ColumnCount;
            int minesCount = gameLogic.MinesCount;

            gameLogic.AddHighScore(userName, time, rowCount, columnCount, minesCount);
        }

        public string GetHighScore()
        {
            return gameLogic.GetHighScore();
        }

        public void ClearHighScore()
        {
            gameLogic.ClearHighScore();
        }
    }
}
