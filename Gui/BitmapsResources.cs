using System.Drawing;

namespace Gui
{
    public class BitmapsResources
    {
        public Bitmap[] minesNearCount = new Bitmap[]
        {
            //Properties.Resources.cell0,
            Properties.Resources.cell01,

            Properties.Resources.cell1,
            Properties.Resources.cell2,
            Properties.Resources.cell3,
            Properties.Resources.cell4,
            Properties.Resources.cell5,
            Properties.Resources.cell6,
            Properties.Resources.cell7,
            Properties.Resources.cell8
        };

        public Bitmap clockMinus = new Bitmap(Properties.Resources.clockMinus);

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
        public Bitmap questionPressCell = new Bitmap(Properties.Resources.questionPressCell);

        public Bitmap mine = new Bitmap(Properties.Resources.mine);
        public Bitmap mineError = new Bitmap(Properties.Resources.mineError);
        public Bitmap mineBombed = new Bitmap(Properties.Resources.mineBombed);

        public Bitmap cellStart = new Bitmap(Properties.Resources.cellStart1);
        //public Bitmap cellStart =new Bitmap(Properties.Resources.cellStart);

        //public Bitmap smileButton = new Bitmap(Properties.Resources.smileButton);
        //public Bitmap smileButtonCry = new Bitmap(Properties.Resources.smileButtonCry);
        //public Bitmap smileButtonAttention = new Bitmap(Properties.Resources.smileButtonAttention);
        //public Bitmap smileButtonPressed = new Bitmap(Properties.Resources.smileButtonPressed);

        public Bitmap smileButtonPressed = new Bitmap(Properties.Resources.smileButtonPressed1);
        public Bitmap smileButtonAttention = new Bitmap(Properties.Resources.smileButtonAttention1);
        public Bitmap smileButton = new Bitmap(Properties.Resources.smileButton1);
        public Bitmap smileButtonCry = new Bitmap(Properties.Resources.smileButtonCry1);
    }
}
