using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
//using Minesweeper.Logic;

namespace Minesweeper.Gui
{
    class ButtonCell :Button
    {



        public ButtonCell()
        {
            ButtonCellCreate();
        }

        private void ButtonCellCreate()
        {
            this.SetStyle(ControlStyles.Selectable, false);
            // this.SetStyle(ControlStyles.);
          //  this.OnMouseMove+=new ;

        }


    }
}
