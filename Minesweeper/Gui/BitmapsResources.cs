﻿using System;
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
            Properties.Resources.cell0,
            Properties.Resources.cell1,
            Properties.Resources.cell2,
            Properties.Resources.cell3,
            Properties.Resources.cell4,
            Properties.Resources.cell5,
            Properties.Resources.cell6,
            Properties.Resources.cell7,
            Properties.Resources.cell8
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
