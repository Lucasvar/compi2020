using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using Compilador;

namespace at.jku.ssw.cc //Compilador//text_Box_Mio
{
    public static class Program1
    {
        //static int ii = 11;
        public static Form1 form1; 
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form1 = new Form1();
            //Application.Run(new Form1());
            Application.Run(form1);
        }
    }
}
