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
    public partial class FrmContinuar : Form
    {
        public FrmContinuar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  //ver Gramaica
        {
            Parser.muestraProducciones = Parser.muestraCargaDeInstrs = true;
           // Tab.muestraTabSimb = Program1.form1.richTextBox10.Visible = false;
            Program1.form1.treeView1.ExpandAll();
            this.Close(); 
           
        }

        private void button2_Click(object sender, EventArgs e) //Ver Instrucciones
        {
            Parser.muestraProducciones = false;
            //Tab.muestraTabSimb = Program1.form1.richTextBox10.Visible = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) //Compilar
        {
            Parser.muestraProducciones = Parser.muestraCargaDeInstrs = false;
         //   Tab.muestraTabSimb = Program1.form1.richTextBox10.Visible = false; 
            this.Close();
        }
        
        //Esto aparece con doble click en el formulario
        private void FrmContinuar_Load(object sender, EventArgs e)
        {
            this.Location = new Point(200, 0);
        }

        private void button4_Click(object sender, EventArgs e) //Ver Gramat y Tabla de Simb
        {
            Parser.muestraProducciones = Parser.muestraCargaDeInstrs = true;
         //   Tab.muestraTabSimb = Program1.form1.richTextBox10.Visible = true;

            this.Close();
        }
    }
}
