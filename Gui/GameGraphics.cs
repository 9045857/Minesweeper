using Logic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Gui
{
    public class GameGraphics
    {
        private GameLogic gameLogic;
        private GameAreaGraphics gameAreaGraphics;
        private GameInfoGraphics gameInfoGraphics;
        private HighScoreDialog highScoreDialog;
        private PictureBox dynamicImage;
        private BitmapsResources bitmapsResources;

        private bool isWinWithHighScore;
        private int gameTime;

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
            //  gameLogic.OnFinishAndWinGame += DrawDynamicWinFinal;

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
        }

        private void DrawDynamicWinFinal()
        {
            DoDynamicWin();

            if (isWinWithHighScore)
            {
                this.highScoreDialog.GameTime = gameTime;
                this.highScoreDialog.ShowDialog();
                isWinWithHighScore = false;
            }
            else
            {
                string congratulation = string.Concat(GameOptionsConstants.WinMessage, gameLogic.CurrentTime, " сек.");
                MessageBox.Show(congratulation, GameOptionsConstants.WinMessageCaption);
            }

            dynamicImage.Visible = false;
        }

        private void DrawDynamicLossFinal()
        {
            dynamicImage.Image = bitmapsResources.dynamicLoss;
            dynamicImage.Visible = true;

            MessageBox.Show(GameOptionsConstants.LossMessage, GameOptionsConstants.LossMessageCaption);

            dynamicImage.Visible = false;
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

        //private void ShowHighScoreDialog()
        //{
        //    DoDynamicWin();



        //    dynamicImage.Visible = false;
        //}

        private void DoDynamicWin()
        {
            dynamicImage.Image = bitmapsResources.dynamicWin;
            dynamicImage.Visible = true;

            //TODO сделать задерку 0,5-1 сек для анимации.
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
