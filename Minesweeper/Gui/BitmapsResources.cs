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
        public Bitmap[] minesNearCount = new Bitmap[]
        {
            Properties.Resources.field0,
            Properties.Resources.field1,
            Properties.Resources.field2,
            Properties.Resources.field3,
            Properties.Resources.field4,
            Properties.Resources.field5,
            Properties.Resources.field6,
            Properties.Resources.field7,
            Properties.Resources.field8
        };

        public Bitmap[] numbers = new Bitmap[]
        {
            Properties.Resources.clock0,
            Properties.Resources.clock1,
            Properties.Resources.clock2,
            Properties.Resources.clock3,
            Properties.Resources.clock4,
            Properties.Resources.clock5,
            Properties.Resources.clock6,
            Properties.Resources.clock7,
            Properties.Resources.clock8,
            Properties.Resources.clock9
        };

        public Bitmap flag = new Bitmap(Properties.Resources.flag);
        public Bitmap question = new Bitmap(Properties.Resources.question);

        public Bitmap mine = new Bitmap(Properties.Resources.mine);
        public Bitmap mineFalse = new Bitmap(Properties.Resources.mineFalse);
        public Bitmap mineBombed = new Bitmap(Properties.Resources.mineBombed);

        public Bitmap cellStart = new Bitmap(Properties.Resources.StartButton);

        public Bitmap smile = new Bitmap(Properties.Resources.smileButton);
        public Bitmap smilePress = new Bitmap(Properties.Resources.attantionSmileButton);
        public Bitmap smileCry = new Bitmap(Properties.Resources.crySmileButton);
    }
}
