using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Minesweeper.Gui
{
    class BitmapsResources
    {
        public Point bitmapLeftTopPoint = new Point(GraphicsConstants.LeftBitmapXCordinate, GraphicsConstants.TopBitmapYCordinate);

        public Bitmap[] bitmapsNumbers = new Bitmap[]
        {
            Properties.Resources.number1_2,
            Properties.Resources.number2_2,
            Properties.Resources.number3_2,
            Properties.Resources.number4_2,
            Properties.Resources.number5_2,
            Properties.Resources.number6_2,
            Properties.Resources.number7_2,
            Properties.Resources.number8_2,
        };

        public Bitmap flag = new Bitmap(Properties.Resources.flag);

        public Bitmap question = new Bitmap(Properties.Resources.question);

        public Bitmap mine = new Bitmap(Properties.Resources.mine3);

        public Bitmap mineRemovel = new Bitmap(Properties.Resources.mine3Removel);
    }
}
