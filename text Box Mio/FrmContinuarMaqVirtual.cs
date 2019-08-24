using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace at.jku.ssw.cc 
{
    public partial class FrmContinuarMaqVirtual : Form
    {
        public FrmContinuarMaqVirtual()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parser.muestraProducciones = Parser.muestraCargaDeInstrs = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parser.muestraProducciones = Parser.muestraCargaDeInstrs = false;
            this.Close();
        }
        private void FrmContinuarMaqVirtual_Load(object sender, EventArgs e)
        {
        }

        private void FrmContinuarMaqVirtual_Load_1(object sender, EventArgs e)
        {
        }
    }
}
