using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JRCinemas
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (JRCinemaForm form = new JRCinemaForm())
            {
                Application.Run(form);
            }
           
        }
    }
}
