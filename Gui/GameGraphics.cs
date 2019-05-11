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
            gameLogic.OnWinWithHighScore += ShowHighScoreDialog;

            gameAreaGraphics = new GameAreaGraphics(gameAreaPictureBox, gameLogic, bitmapsResources);
            gameInfoGraphics = new GameInfoGraphics(gameLogic, gameAreaGraphics, bitmapsResources, pictureBoxSmileButton, pictureBoxMinesCount, pictureBoxTime);

            this.highScoreDialog = highScoreDialog;
            this.highScoreDialog.OnSetUserName += AddHighScore;
        }

        public void SetCellTopColor(Color color)
        {
            gameAreaGraphics.CellTopColor = color;
        }

        private void ShowHighScoreDialog(int gameTime)
        {
            this.highScoreDialog.GameTime = gameTime;
            this.highScoreDialog.ShowDialog();
        }

        private void AddHighScore()//TODO нужно переделать. передавать то, что написано в диалоге без изменний, но возвращать нормальные
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
