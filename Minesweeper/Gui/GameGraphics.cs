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
        public delegate void ChangeCellTopColor(Color color);
        public event ChangeCellTopColor OnChangeCellTopColor;

        private GameLogic gameLogic;
        private GameAreaGraphics gameAreaGraphics;
        private GameInfoGraphics gameInfoGraphics;

        public GameGraphics
        (
            GameParameters gameParameters,
            PictureBox gameAreaPictureBox,
            BitmapsResources bitmapsResources,
            PictureBox pictureBoxSmileButton,
            PictureBox pictureBoxMinesCount,
            PictureBox pictureBoxTime,
            Timer timerGame
        )
        {
            gameLogic = new GameLogic(gameParameters);
            gameAreaGraphics = new GameAreaGraphics(gameAreaPictureBox, gameLogic, bitmapsResources);
            gameInfoGraphics = new GameInfoGraphics(gameLogic, gameAreaGraphics, bitmapsResources, pictureBoxSmileButton, pictureBoxMinesCount, pictureBoxTime, timerGame);
        }

        public void SetCellTopColor(Color color)
        {
                gameAreaGraphics.CellTopColor = color;          
        }
    }
}
