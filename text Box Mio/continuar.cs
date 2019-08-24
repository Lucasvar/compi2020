using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using at.jku.ssw.cc;

namespace text_Box_Mio
{
    public partial class continuar : Form
    {
        public continuar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void continuar_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            at.jku.ssw.cc.Parser.muestraProducciones = at.jku.ssw.cc.Parser.muestraCargaDeInstrs = at.jku.ssw.cc.Tab.muestraTabSimb = false;
            this.Close();
        }
    }
}
