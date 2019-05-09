﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;


namespace Gui
{
    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();// нужно перед созданием первой формы
            Application.SetCompatibleTextRenderingDefault(false);// нужно перед созданием первой формы
            Application.Run(new MainForm());





        }
    }
}