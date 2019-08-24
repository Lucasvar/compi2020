using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
//using Compilador;
using System.Threading;
using System.Diagnostics;


namespace at.jku.ssw.cc //Compilador //text_Box_Mio
{
    public partial class Form1 : Form
    {
        public text_Box_Mio.continuar instContinuar;  // 

        public RichTextBox Editor;
        public TabPage T;

        public static bool errorEnComilacion = false;
        //static int linea, col, sizeToken = 0;
        public static int iniCommLine, iniCommCol, finCommLin, finCommCol;
        static string myString;
        static string myStringNombre;
        void P(string line) { }


        public Form1()
        {
            InitializeComponent();
            instContinuar = new text_Box_Mio.continuar();
            //  inicializa();

        }

        private void button1_Click(object sender, EventArgs e) //muestra Vars Locs
        {
            Parser.muestraVarsLocales();
            Parser.MessageBoxCon3Preg();

        }
        private void button2_Click(object sender, EventArgs e)  //compilar
        {
            treeView1.Nodes.Clear();
            compilar();

            //Tab.mostrarTab();
            //richTextBox10.Text =  Tab.tabSimbString;
        }

        public void guardar()
        {
            using (System.IO.StreamWriter escritor = new System.IO.StreamWriter(myStringNombre))
            {
                myString = Editor.Text;
                escritor.Write(myString);

            }
        }
        public bool nuevo()
        {

            SaveFileDialog nuevo = new SaveFileDialog();
            nuevo.Filter = "Archivos .zs (*.zs)|*.txt";
            nuevo.Title = "Nuevo";
            if (nuevo.ShowDialog() == DialogResult.OK)
            {

                myStringNombre = nuevo.FileName;//preguntar
                myString = "";
                Editor.Text = myString;
                return true;

            }
            return false;

        }
        public bool open()
        {
            TextReader textReader;
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Archivos .zs (*.zs)|*.txt";
            openFD.Title = "Seleccione archivo .txt";
            openFD.FileName = "";
            //openFD.InitialDirectory = "d:\\";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                //string fileName = openFD.FileName;
                //label15.Text = fileName;
                myStringNombre = openFD.FileName;
                textReader = new StreamReader(myStringNombre);
                myString = textReader.ReadToEnd();

                textReader.Dispose();

                Editor.Text = myString;
                return true;
            }
            return false;

            //compilerForm_Load(this, new EventArgs());
        }
        public void compilar()
        {
            //Inicio Modificacion - Grupo 1 - 10/9/15
            //Oculta Labels, RichTextBox y Buttons de Maquina Virtual. Oculta Monitor de Maquina Virtual   
            Code.restaurarRichTextBox7conNegro();
            richTextBox8.Visible = false;
            richTextBox9.Visible = false;
            pictureBox2.Visible = false;
            tabControl2.Visible = true;
            richTextBox7.Visible = true;
            button3.Visible = button4.Visible = false;
            richTextBox2.Visible = richTextBox4.Visible = richTextBox5.Visible = false;
            label1.Visible = label2.Visible = label7.Visible = label8.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible = label13.Visible = false;
            //Fin Modificacion - Grupo 1 - 10/9/15
            //   
            Editor = (RichTextBox)pestania.SelectedTab.Controls[0];
            // pantalla.Visible = true; //Pantalla del monitorcito
            //pictureBox1.Visible = true;  //monitorcito
            errorEnComilacion = false;
            //para usar luego como cil[nroDeInstrCorriente].indBrFalse = brfalseVar[x.relop - Token.EQ]; //BGEenum, etc 
            //para branches condicionales
            //  public enum BrfalseENUM { BNEenum, BLTenum, BLEenum, BGTenum, BGEenum, BEQenum };
            Code.brfalseVar = new Code.BrfalseENUM[6];
            Code.brfalseVar[0] = (Code.BrfalseENUM)Code.BrfalseENUM.BNEenum;
            Code.brfalseVar[1] = (Code.BrfalseENUM)Code.BrfalseENUM.BLTenum;
            Code.brfalseVar[2] = (Code.BrfalseENUM)Code.BrfalseENUM.BLEenum;
            Code.brfalseVar[3] = (Code.BrfalseENUM)Code.BrfalseENUM.BGTenum;
            Code.brfalseVar[4] = (Code.BrfalseENUM)Code.BrfalseENUM.BGEenum;
            Code.brfalseVar[5] = (Code.BrfalseENUM)Code.BrfalseENUM.BEQenum;

            //para usar luego como cil[nroDeInstrCorriente].indBrTrue = brtrueVar[x.relop - Token.EQ]; //BGEenum, etc 
            //  public enum BrtrueENUM { BEQenum, BGEenum, BGTenum, BLEenum, BLTenum, BNEenum };
            Code.brtrueVar = new Code.BrtrueENUM[6];
            Code.brtrueVar[0] = (Code.BrtrueENUM)Code.BrtrueENUM.BEQenum;
            Code.brtrueVar[1] = (Code.BrtrueENUM)Code.BrtrueENUM.BGEenum;
            Code.brtrueVar[2] = (Code.BrtrueENUM)Code.BrtrueENUM.BGTenum;
            Code.brtrueVar[3] = (Code.BrtrueENUM)Code.BrtrueENUM.BLEenum;
            Code.brtrueVar[4] = (Code.BrtrueENUM)Code.BrtrueENUM.BLTenum;
            Code.brtrueVar[5] = (Code.BrtrueENUM)Code.BrtrueENUM.BNEenum;
            Parser.pilita.tope = -1; //inicializacion de E Stack 

            //Restaura Vars Locales
            for (int i = 0; i < Parser.maxCantVarsLocales; i++) Parser.locals[i] = 0;


            //Restaura Boton de continuar
            if (Parser.ejecuta)
            {
                Parser.muestraProducciones = false;
                Parser.muestraCargaDeInstrs = false;
            }
            else
            {
                // Parser.muestraProducciones = true;
                Parser.muestraCargaDeInstrs = true;
            }

            //pantalla.Text = ""; //Pantallita del monitor
            //pantalla.Visible = false; ////Pantallita del monitor no visible

            Program1.form1.richTextBox9.Text = ""; //salida real (con la maqu virtual real)

            Parser.cantVarLocales = 0;
            Parser.nroDeInstrCorriente = 0;

            //Restaura scroll al principio
            Editor.SelectionStart = 0; // al principio
            Editor.ScrollToCaret();

            //limpia ventanas
            richTextBox2.Text = ""; //Dejo vacio Stack
            richTextBox3.Text = ""; //Dejo vacio Instr CIL
            richTextBox4.Text = ""; //Vars Locales  
                                    //richTextBox6.Text = "";   //Arbol de derivacion

            //redirige salida a "salida.txt"
            StreamWriter sw = new StreamWriter("salida.txt");
            sw.AutoFlush = true;
            Console.SetOut(sw);  //redirige salida a sw = "salida.txt"

            //debe releer pestania.SelectedTab.Controls[0] y alimentar program con el contenido de pestania.SelectedTab.Controls[0]
            //relee pestania.SelectedTab.Controls[0]   
            pestania.SelectedTab.Controls[0].Update();
            string myString1 = Editor.Text;
            //restaura color negro en pestania.SelectedTab.Controls[0]
            Code.restaurarRichTextBox1conNegro();

            //alimentar program con el contenido de pestania.SelectedTab.Controls[0] (myString1)
            if (!Parser.ejecuta) Parser.MessageBoxCon3Preg();

            try
            {
                Parser.inicializaCil();

                /* Quitar los comentarios para que funcione el Scanner */
                if (ZZ.Program) Console.WriteLine("Main Compilador 2");
                if (ZZ.Principal)
                {
                    Console.WriteLine("ha pasado new ScannerTest()");
                    Console.WriteLine("ha pasado SCTest.CRLFLineSeparators()");
                    Console.WriteLine("ha pasado SCTest.LFLineSeparators");
                }
                if (ZZ.Program) Console.WriteLine("pasó InvalidSymbols()\n\nTERMINÓ TODO EL SCANNER");
                /////////////////////////////////////////////////////////////////////////////
                //System.Windows.Forms.MessageBox.Show("Parser.Parse(myString1)");
                Parser.Parse(myString1);
                /////////////////////////////////////////////////////////////////////////////
                if (ZZ.Program) Console.WriteLine("Tab.mostrarTab().....al final");
                if (ZZ.Program) Tab.mostrarTab();
                ZZ.Program = false;
                if (ZZ.Program) Console.WriteLine("TERMINA TODO");
                if (ZZ.Program) Console.ReadKey();
            }
            catch (ErrorMio e1)
            {
                int linea1 = e1.linea; int col1 = e1.columna; int sizeToken1 = e1.sizeToken;
                Editor.Select(Editor.GetFirstCharIndexFromLine(linea1 - 1) + col1 - 1, sizeToken1);
                Editor.SelectionColor = Color.Red;
                System.Windows.Forms.MessageBox.Show("error 3245..." + e1.msg);
                errorEnComilacion = true;
            }
            //Program1.form1.maquVirtualToolStripMenuItem.Enabled = true;
            Program1.form1.depurarToolStripMenuItem.Enabled = true;

            sw.Close();

        } //Fin compilar()


        //private void button3_Click(object sender, EventArgs e)  //inicializar
        private void inicializa()  //inicializar
        {
            Parser.cil = new Parser.Instruccion[Parser.maxnroDeInstrCorriente];
            /*   
               //lee fuente en Editor
               try
               {
                   // Create an instance of StreamReader to read from a file.
                   // The using statement also closes the StreamReader.



                           string file = openFileDialog1.FileName; //obtengo direccio del archivo
                           using (StreamReader myFile = new StreamReader(file)) //lee fuente en Editor
                           {                            
                               myString = myFile.ReadToEnd();
                               Editor.Text = myString;
                           }



               }
               catch (Exception e1)
               {
                   // Let the user know what went wrong.
                   Console.WriteLine("The file could not be read:");
                   Console.WriteLine(e1.Message); Console.ReadKey();
               };
   */
            //lee gramatica en richTextBox7
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                //Lee: OK
                using (StreamReader myFile = new StreamReader("z_gramatica.txt")) //
                {
                    myString = myFile.ReadToEnd();
                    richTextBox7.Text = myString;
                }
            }
            catch (Exception e1)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file (gramatica.txt) could not be read:");
                Console.WriteLine(e1.Message); Console.ReadKey();
            };

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void button4_Click(object sender, EventArgs e)
        {
            Parser.muestraCil();
        }

        private void button3_Click(object sender, EventArgs e)    //ejec maqu virtual                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        {
            maquVirtual();
        }

        public void maquVirtual()    //ejec maqu virtual                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        {
            richTextBox8.Visible = true;

            pictureBox2.Visible = true;
            // richTextBox10.Visible = false;
            //label15.Visible = false;


            //System.Windows.Forms.MessageBox.Show("en m virt");


            richTextBox8.Visible = true;
            Program1.form1.richTextBox8.Text = ""; //pantallita
            Program1.form1.richTextBox9.Text = ""; //salida real (con la maqu virtual real)
            Program1.form1.richTextBox4.Text = ""; //Vars Locales
            Parser.pilita.tope = -1; //
            Parser.pilita.inicializa();

            //Restaura Vars Locales
            for (int i = 0; i < Parser.maxCantVarsLocales; i++) Parser.locals[i] = 0;

            string todasLasInstr;
            todasLasInstr = richTextBox3.Text;

            Program1.form1.richTextBox3.SelectionStart = 0; // al principio
            Program1.form1.richTextBox3.ScrollToCaret();

            //Selecciona locales
            int cantDeRojoQuePinta = 3;  //" 0:"
            richTextBox3.Select(richTextBox3.GetFirstCharIndexFromLine(0) + 0, cantDeRojoQuePinta); //23);
            //System.Windows.Forms.MessageBox.Show(col.ToString());
            richTextBox3.SelectionColor = Color.Red;
            Program1.form1.richTextBox3.SelectionFont =
                     new Font(Program1.form1.richTextBox3.Font.FontFamily,
                              Program1.form1.richTextBox3.Font.Size, FontStyle.Bold);

            Parser.muestraVarsLocales();
            if (Parser.ejecuta)
            {
                Parser.muestraProducciones = Parser.muestraCargaDeInstrs = false;
            }
            else
                Parser.MessageBoxCon2PregMaqVirtual();

            //Restaura locales de Bold a Normal
            richTextBox3.Select(richTextBox3.GetFirstCharIndexFromLine(0) + 0, cantDeRojoQuePinta);//23);
            //System.Windows.Forms.MessageBox.Show(col.ToString());
            richTextBox3.SelectionColor = Color.Black;
            Program1.form1.richTextBox3.SelectionFont =
                     new Font(Program1.form1.richTextBox3.Font.FontFamily,
                              Program1.form1.richTextBox3.Font.Size, FontStyle.Regular);

            int nroInstr = 1;

            int segmAnterior = 0;

            bool seguir = true;
            while (seguir) //(nroInstr <= Parser.nroDeInstrCorriente)
            {
                int segActual = nroInstr / 33; //De 0 a 32 => 0, De 33 a 63 => 1, ....
                if (segActual != segmAnterior)
                {
                    Program1.form1.richTextBox3.SelectionStart =
                          Program1.form1.richTextBox3.GetFirstCharIndexFromLine(segActual * 33 + 2);//error raro
                                                                                                    // + 2 debido a .locals consume 3 lineas en vez de 1

                    Program1.form1.richTextBox3.ScrollToCaret();
                }
                segmAnterior = segActual;
                todasLasInstr = richTextBox3.Text;

                //richTextBox3.Select(richTextBox3.GetFirstCharIndexFromLine(Parser.cantVarLocales - 1) + 0, cantDeRojoQuePinta);
                int linea = nroInstr + Parser.cantVarLocales - 1; //qqqqq
                int ver = richTextBox3.GetFirstCharIndexFromLine(linea);
                richTextBox3.Select(richTextBox3.GetFirstCharIndexFromLine(linea) + 0, cantDeRojoQuePinta);//??????
                richTextBox3.SelectionColor = Color.Red;

                Program1.form1.richTextBox3.SelectionFont =
                 new Font(Program1.form1.richTextBox3.Font.FontFamily,
                          Program1.form1.richTextBox3.Font.Size, FontStyle.Bold);


                //Parser.MessageBoxCon3Preg();

                switch (Parser.cil[nroInstr].accionInstr)
                {

                    case Parser.AccionInstr.add:
                        int primerElem = ((ElemPilita)Parser.pilita.pop()).entero;
                        int segElem = ((ElemPilita)Parser.pilita.pop()).entero;
                        Parser.pilita.push(
                            new ElemPilita(primerElem + segElem, "", ElemPilita.ElemDePila.esEntero));

                        //Parser.pilita.push(primerElem + segElem, "", ElemPilita.ElemDePila.esEntero);
                        break;

                    case Parser.AccionInstr.sub:
                        primerElem = ((ElemPilita)Parser.pilita.pop()).entero;
                        segElem = ((ElemPilita)Parser.pilita.pop()).entero;
                        Parser.pilita.push(
                            new ElemPilita(segElem - primerElem, "", ElemPilita.ElemDePila.esEntero));
                        break;

                    case Parser.AccionInstr.mul:
                        primerElem = ((ElemPilita)Parser.pilita.pop()).entero;
                        segElem = ((ElemPilita)Parser.pilita.pop()).entero;
                        Parser.pilita.push(
                            new ElemPilita(primerElem * segElem, "", ElemPilita.ElemDePila.esEntero));
                        break;

                    case Parser.AccionInstr.div:
                        primerElem = ((ElemPilita)Parser.pilita.pop()).entero;
                        segElem = ((ElemPilita)Parser.pilita.pop()).entero;
                        Parser.pilita.push(
                            new ElemPilita(segElem / primerElem, "", ElemPilita.ElemDePila.esEntero));
                        break;

                    case Parser.AccionInstr.loadConst:
                        Parser.pilita.push(
                            new ElemPilita(Parser.cil[nroInstr].nro, "", ElemPilita.ElemDePila.esEntero));
                        break;
                    case Parser.AccionInstr.ldstr:
                        Parser.pilita.push(
                             new ElemPilita(-1, Parser.cil[nroInstr].argDelWriteLine, ElemPilita.ElemDePila.esEstring));
                        break;

                    case Parser.AccionInstr.loadLocal:
                        Parser.pilita.push(
                             new ElemPilita(Parser.locals[Parser.cil[nroInstr].nro], "", ElemPilita.ElemDePila.esEntero));
                        break;

                    case Parser.AccionInstr.nop:
                        //System.Windows.Forms.MessageBox.Show("no hace nada");
                        break;
                    case Parser.AccionInstr.pop:
                        //(ElemPilita)Parser.pilita.pop()
                        Parser.pilita.pop();   // lo tira

                        break;
                    case Parser.AccionInstr.storeLocal:
                        Parser.locals[Parser.cil[nroInstr].nro] = ((ElemPilita)Parser.pilita.pop()).entero;
                        Parser.muestraVarsLocales();
                        break;

                    case Parser.AccionInstr.write:
                        int ancho = ((ElemPilita)Parser.pilita.pop()).entero;
                        int nuevoValor = ((ElemPilita)Parser.pilita.pop()).entero;
                        string nuevoValorStr = Code.blancos(ancho - nuevoValor.ToString().Length) + nuevoValor.ToString();
                        string textBoxAnterior = richTextBox8.Text;


                        if (richTextBox8.Text == "") richTextBox8.Text = nuevoValorStr;
                        else richTextBox8.Text = textBoxAnterior + nuevoValorStr;
                        string nuevo = richTextBox8.Text;

                        //Letras en Blanco
                        Program1.form1.richTextBox8.Select(
                                     Program1.form1.richTextBox8.GetFirstCharIndexFromLine(0)
                                                           + 0, nuevo.Length);
                        Program1.form1.richTextBox8.SelectionColor = System.Drawing.Color.White;
                        richTextBox8.Visible = true;
                        break;

                    case Parser.AccionInstr.writeln:
                        string argStringDelWriteLine = "??";

                        ElemPilita elemPilita = (ElemPilita)Parser.pilita.pop();
                        if (elemPilita.elemDePila == ElemPilita.ElemDePila.esEstring)
                            argStringDelWriteLine = elemPilita.estring;
                        else System.Windows.Forms.MessageBox.Show("Error 9876543 asew");

                        textBoxAnterior = richTextBox8.Text;
                        if (richTextBox8.Text == "")
                            richTextBox8.Text = argStringDelWriteLine + "\n";
                        else richTextBox8.Text = textBoxAnterior + argStringDelWriteLine + "\n";
                        //          + Parser.cil[nroInstr].argDelWriteLine + "\n";

                        //Letras en Blanco
                        nuevo = richTextBox8.Text;
                        Program1.form1.richTextBox8.Select(
                                     Program1.form1.richTextBox8.GetFirstCharIndexFromLine(0)
                                                           + 0, nuevo.Length);
                        Program1.form1.richTextBox8.SelectionColor = System.Drawing.Color.White;
                        richTextBox8.Visible = true;
                        break;
                    case Parser.AccionInstr.branchInc:
                        nroInstr = Parser.cil[nroInstr].nroLinea - 1; //Porque al final del switch lo incr en 1
                        break;
                    case Parser.AccionInstr.fJump:
                        switch (Parser.cil[nroInstr].indBrFalse)
                        {
                            case Code.BrfalseENUM.BNEenum:
                                //int tope1 = Parser.pilita.pop();
                                //int segundo = Parser.pilita.pop();
                                //if (tope1 != segundo)
                                if (((ElemPilita)Parser.pilita.pop()).entero != ((ElemPilita)Parser.pilita.pop()).entero)
                                    nroInstr = Parser.cil[nroInstr].nroLinea - 1;
                                break;
                            case Code.BrfalseENUM.BLTenum:
                                int tope = ((ElemPilita)Parser.pilita.pop()).entero;
                                if (((ElemPilita)Parser.pilita.pop()).entero < tope)
                                    nroInstr = Parser.cil[nroInstr].nroLinea - 1;
                                break;
                            case Code.BrfalseENUM.BLEenum:
                                tope = ((ElemPilita)Parser.pilita.pop()).entero;
                                if (((ElemPilita)Parser.pilita.pop()).entero <= tope)
                                    nroInstr = Parser.cil[nroInstr].nroLinea - 1;
                                break;
                            case Code.BrfalseENUM.BGTenum:
                                tope = ((ElemPilita)Parser.pilita.pop()).entero;
                                if (((ElemPilita)Parser.pilita.pop()).entero > tope)
                                    nroInstr = Parser.cil[nroInstr].nroLinea - 1;
                                break;

                            case Code.BrfalseENUM.BGEenum:
                                tope = ((ElemPilita)Parser.pilita.pop()).entero;
                                if (((ElemPilita)Parser.pilita.pop()).entero >= tope)
                                    nroInstr = Parser.cil[nroInstr].nroLinea - 1;
                                break;

                            case Code.BrfalseENUM.BEQenum:
                                if (((ElemPilita)Parser.pilita.pop()).entero == ((ElemPilita)Parser.pilita.pop()).entero)
                                    nroInstr = Parser.cil[nroInstr].nroLinea - 1;
                                break;
                            default:
                                System.Windows.Forms.MessageBox.Show("NO deberia entrar aqui ddwdw4242");
                                break;
                        }
                        break;
                    case Parser.AccionInstr.ret:
                        seguir = false;
                        break;

                    default:
                        System.Windows.Forms.MessageBox.Show("no deberia entrar aqui 4354dd56");
                        break;
                }//Fin Switch
                Parser.pilita.mostrarPilita();
                Code.restaurarRichTextBox3conNegro();
                nroInstr++;
            }//Fin While
            //Parser.pilita.mostrarPilita();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox6_TextChanged(object sender, EventArgs e) // Gramatica
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter("salida.txt");

            //la ejecucion que dejó en "salida.txt" la coloca en richTextBox2 

            //if (errores) .........

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader myFile2 = new StreamReader("salida.txt"))
                //ya puede leer este 

                // StreamReader myFile2 = new StreamReader("programa.txt");  //este: OK
                {
                    string myString2 = myFile2.ReadToEnd();


                    //Program.Main1(myString);
                    richTextBox9.Text = myString2;
                    richTextBox9.Select(richTextBox9.GetFirstCharIndexFromLine(0)
                                                           + 0,
                    richTextBox9.Text.Length);
                    richTextBox9.SelectionColor = Color.Blue;


                    myFile2.Close();
                }
                //myString = myString1;
            }
            catch (Exception e1)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e1.Message); Console.ReadKey();
            };
            /////////////////////////////////////////////////////

            //sw.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            TextReader textReader;
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Archivos .zs (*.zs)|*.txt";
            openFD.Title = "Seleccione archivo .txt";
            openFD.FileName = "";
            //openFD.InitialDirectory = "d:\\";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFD.FileName;
                //label15.Text = fileName;

                textReader = new StreamReader(fileName);
                string fileText = textReader.ReadToEnd();
                textReader.Dispose();

                Editor.Text = fileText;
            }

            //compilerForm_Load(this, new EventArgs());

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Tab.muestraTabSimb = true;
            //richTextBox10.Visible = true;
            //label15.Visible = true;

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (open())
           {
               generarToolStripMenuItem.Enabled = true;
               depurarToolStripMenuItem.Enabled = true;
               guardarToolStripMenuItem.Enabled = true;
               guardarComoToolStripMenuItem.Enabled = true;
               button2.Enabled = true;
               button3.Enabled = true;
               inicializa();*/

            TextReader textReader;
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Archivos|*.txt";
            openFD.Title = "Seleccione archivo";
            openFD.FileName = "";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                guardarToolStripMenuItem.Enabled = true;
                guardarComoToolStripMenuItem.Enabled = true;

                button2.Enabled = true;
                button3.Enabled = true;
                // button5.Enabled = true;
                string fileName = openFD.FileName;
                //MessageBox.Show(fileName);//Muestra el file abierto con todo el path incluido
                textReader = new StreamReader(fileName);
                string fileText = textReader.ReadToEnd();
                textReader.Dispose();
                //lo que yo hago--
                FileInfo a = new FileInfo(openFD.FileName);
                if (pestania.TabPages[a.Name] == null)
                {
                    T = new TabPage();
                    T.Name = a.Name; //este el el nombre (interno) del TabPage
                    T.Text = a.Name;  //Este es el texto que muestra en la pestanha
                    //MessageBox.Show(a.Name);  //Muestra el file abierto sin el path (por ej:  "if Anidado.txt" )
                    Editor = new RichTextBox();
                    //Editor.Name = a.Name + "Ritch";  //no es necesario
                    //Editor.Size = new Size(376, 554);
                    Editor.Size = new Size(428, 303);
                    Editor.Text = fileText;

                    // Podria haberse hecho del sig. modo:
                    //Editor.LoadFile(openFD.FileName, RichTextBoxStreamType.PlainText);


                    T.Controls.Add(Editor);
                    pestania.TabPages.Add(T);
                    Editor.ContextMenuStrip = contextMenuStrip1;
                    pestania.SelectTab(a.Name);

                    Editor.KeyPress += new KeyPressEventHandler(Editor_PressKey);
                    Editor.KeyUp += new KeyEventHandler(Editor_KeyUp);
                    Editor.MouseClick += new MouseEventHandler(Editor_MouseClick);
                    //termino lo que yo hago
                }
                else pestania.SelectTab(a.Name);
            }
            //}
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nuevo())
            {
                //generarToolStripMenuItem.Enabled = true;
                depurarToolStripMenuItem.Enabled = true;
                guardarToolStripMenuItem.Enabled = true;
                guardarComoToolStripMenuItem.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;

                inicializa();
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void ejecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Parser.ejecuta = true;
            //compilar();

            //if (!errorEnComilacion) maquVirtual();
            //Parser.ejecuta = false;
            //tabControl1.Width -= 100;
            label1.Visible = label2.Visible = label8.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible = label13.Visible = true;
            richTextBox2.Visible = richTextBox4.Visible = true;
            tabControl1.SelectedIndex = 2;
            tabControl2.Visible = false;
            button3.Visible = true;
            richTextBox8.Visible = richTextBox9.Visible = true;
            maquVirtual();

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("ok1");
            SaveFileDialog nuevo = new SaveFileDialog();
            nuevo.Filter = "Archivos |*.txt";
            if (nuevo.ShowDialog() == DialogResult.OK)
            {

                //myStringNombre = nuevo.FileName; //tiene el path incluido

                //using (System.IO.StreamWriter escritor = new System.IO.StreamWriter(myStringNombre))
                //{
                //    myString = Editor.Text;
                //    escritor.Write(myString);

                //}

                //MessageBox.Show("ok2");
                //hubiera sido lo mismo: 
                Editor.SaveFile(nuevo.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void salidaRealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter("salida.txt");

            //la ejecucion que dejó en "salida.txt" la coloca en richTextBox2 

            //if (errores) .........

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader myFile2 = new StreamReader("salida.txt"))
                //ya puede leer este 

                // StreamReader myFile2 = new StreamReader("programa.txt");  //este: OK
                {
                    string myString2 = myFile2.ReadToEnd();


                    //Program.Main1(myString);
                    richTextBox9.Text = myString2;
                    richTextBox9.Select(richTextBox9.GetFirstCharIndexFromLine(0)
                                                           + 0,
                    richTextBox9.Text.Length);
                    richTextBox9.SelectionColor = Color.Blue;


                    myFile2.Close();
                }
                //myString = myString1;
            }
            catch (Exception e1)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e1.Message); Console.ReadKey();
            };
            /////////////////////////////////////////////////////

            //sw.Close();

        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Editor.CanUndo)
            { Editor.Undo(); }
        }

        private void rehacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Editor.CanRedo)
            { Editor.Redo(); }
        }

        private void ediciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Editor.CanUndo)
            { deshacerToolStripMenuItem.Enabled = true; }
            else
            { deshacerToolStripMenuItem.Enabled = false; }

            if (Editor.CanRedo)
            { rehacerToolStripMenuItem.Enabled = true; }
            else
            { rehacerToolStripMenuItem.Enabled = false; }

            if (Editor.SelectedText.Length != 0)
            {
                copiarToolStripMenuItem.Enabled = true;
                cortarToolStripMenuItem.Enabled = true;
            }
            else
            {
                copiarToolStripMenuItem.Enabled = false;
                cortarToolStripMenuItem.Enabled = false;
            }
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Editor.Cut();

        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Paste();
        }

        private void cortarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Editor.Copy();
        }

        private void seleccionarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.SelectAll();
        }

        private void richTextBox9_TextChanged(object sender, EventArgs e)
        {

        }
        public void Tabulation(char c)
        {

            int contCR = 0;//contador de llaves
            if (c == '\r' || c == '\n')// si cambio de linea empieso a calcular la tabulacion
            {
                contCR = 0;
                string cadena = Editor.Text.Substring(0, Editor.SelectionStart); ;//corto el texto total hasta donde me encuentro posicionado

                for (int i = 0; i < cadena.Length; i++)// si encuentro una llave de apertura sumo si es de cierre resto
                {
                    if (cadena[i] == '{')
                        contCR++;

                    if (cadena[i] == '}')
                        contCR--;
                }

                for (int i = 0; i < contCR; i++)
                {

                    Editor.SelectedText = "     ";//por cada llave sin cerrar agrego el espacio de tabulacion
                }


            }
            else// ahora si voy a agregar una llave de cierre retorno el tabulador en una posicion paar que quede alineado
                if (c == '}' && Editor.Text[Editor.SelectionStart - 1] == ' ' && Editor.Text[Editor.SelectionStart - 2] == ' ' && Editor.Text[Editor.SelectionStart - 3] == ' ' && Editor.Text[Editor.SelectionStart - 4] == ' ' && Editor.Text[Editor.SelectionStart - 5] == ' ')
            {
                int aux;
                aux = Editor.SelectionStart;

                Editor.Text = Editor.Text.Remove(Editor.SelectionStart - 4, 4);//remuevo la tabulacion
                Editor.SelectionStart = aux - 5;// posiciono el cursor
                                                // Editor.Select(Editor.SelectionStart, 1);

            }
        }




        int auxx1 = 0;//variable para almacenar la posicion de la ultima llave/parentesis de apertura para asi desmarcarla
        int auxx2 = 0;//variable para almacenar la posicion de la ultima llave/parentesis de cierre para asi desmarcarla

        public void marca_llaves_parentesis()
        {


            int aux = Editor.SelectionStart;// aux tiene la posicion de la llave/parentesis que selecciono
            Editor.Select(auxx1, 1);// me posiciono en la apertura anterior para limpiarlo
            Editor.SelectionBackColor = Color.White;//desselecciono

            Editor.Select(auxx2, 1);// me posiciono en la apertura anterior para limpiarlo
            Editor.SelectionBackColor = Color.White;//desselecciono

            Editor.Select(aux, 0);// vuelvo a posicionarme en el original
            Editor.SelectionStart = aux;//selecciono
            //
            if (aux < Editor.Text.Length && Editor.Text[aux] == '{')
            {



                int bandera1 = 0;//bandera contador que va a identificar el '}'correspondiente
                int bandera2 = 0;//bandera para terminar el bucle while 
                int i = aux + 1;// posicion siguiente a la actual


                while (bandera2 == 0 && i < Editor.Text.Length)
                {

                    if (Editor.Text[i] == '}' && bandera1 == 0)
                    {
                        bandera2 = 1;//termino el while
                        auxx1 = i;//guardo para desp descolorear
                        Editor.Select(i, 1);// me posiciono en } para colorerar
                        //Editor.SelectionStart = i;
                        //Editor.SelectionLength = 1;
                        Editor.SelectionBackColor = Color.LightGray;
                        auxx2 = aux;//guardo para desp descolorear
                        Editor.SelectionStart = aux;// me posiciono en { para colorear
                        Editor.SelectionBackColor = Color.LightGray;
                        Editor.Select(aux, 0);// dejo el cursor para seguir escribiendo

                    }
                    else
                        if (Editor.Text[i] == '{')
                    {
                        bandera1++;
                    }
                    else
                            if (Editor.Text[i] == '}' && bandera1 != 0)
                    {
                        bandera1--;
                    }
                    i++;
                }
            }
            else // PARENTESIS!!!!!!!!!!!
                if (aux < Editor.Text.Length && Editor.Text[aux] == '(')
            {



                int bandera1 = 0;//bandera contador que va a identificar el ')'correspondiente
                int bandera2 = 0;//bandera para terminar el bucle while 
                int i = aux + 1;// posicion siguiente a la actual


                while (bandera2 == 0 && i < Editor.Text.Length)
                {

                    if (Editor.Text[i] == ')' && bandera1 == 0)
                    {
                        bandera2 = 1;
                        auxx1 = i;
                        Editor.Select(i, 1);
                        //Editor.SelectionStart = i;
                        //Editor.SelectionLength = 1;
                        Editor.SelectionBackColor = Color.LightGray;
                        auxx2 = aux;
                        Editor.SelectionStart = aux;
                        Editor.SelectionBackColor = Color.LightGray;
                        Editor.Select(aux, 0);

                    }
                    else
                        if (Editor.Text[i] == '(')
                    {
                        bandera1++;
                    }
                    else
                            if (Editor.Text[i] == ')' && bandera1 != 0)
                    {
                        bandera1--;
                    }
                    i++;
                }
            }
            else // CIERRE DE LAS LLAVES!!!!!!!!!!!
                    if (aux < Editor.Text.Length && Editor.Text[aux] == '}')
            {



                int bandera1 = 0;
                int bandera2 = 0;
                int i = aux - 1;


                while (bandera2 == 0 && i > 0)
                {

                    if (Editor.Text[i] == '{' && bandera1 == 0)
                    {
                        bandera2 = 1;
                        auxx1 = i;
                        Editor.Select(i, 1);
                        //Editor.SelectionStart = i;
                        //Editor.SelectionLength = 1;
                        Editor.SelectionBackColor = Color.LightGray;
                        auxx2 = aux;
                        Editor.SelectionStart = aux;
                        Editor.SelectionBackColor = Color.LightGray;
                        Editor.Select(aux, 0);

                    }
                    else
                        if (Editor.Text[i] == '}')
                    {
                        bandera1++;
                    }
                    else
                            if (Editor.Text[i] == '{' && bandera1 != 0)
                    {
                        bandera1--;
                    }
                    i--;
                }
            }
            else // FIN PARENTESSIS!!!!!!!!!!!!!!11
                        if (aux < Editor.Text.Length && Editor.Text[aux] == ')')
            {



                int bandera1 = 0;
                int bandera2 = 0;
                int i = aux - 1;


                while (bandera2 == 0 && i > 0)
                {

                    if (Editor.Text[i] == '(' && bandera1 == 0)
                    {
                        bandera2 = 1;
                        auxx1 = i;
                        Editor.Select(i, 1);
                        //Editor.SelectionStart = i;
                        //Editor.SelectionLength = 1;
                        Editor.SelectionBackColor = Color.LightGray;
                        auxx2 = aux;
                        Editor.SelectionStart = aux;
                        Editor.SelectionBackColor = Color.LightGray;
                        Editor.Select(aux, 0);

                    }
                    else
                        if (Editor.Text[i] == ')')
                    {
                        bandera1++;
                    }
                    else
                            if (Editor.Text[i] == '(' && bandera1 != 0)
                    {
                        bandera1--;
                    }
                    i--;
                }
            }

        }

        void Editor_PressKey(object sender, KeyPressEventArgs e)
        {
            Editor = (RichTextBox)pestania.SelectedTab.Controls[0];

            Tabulation(e.KeyChar);

        }

        void Editor_MouseClick(object sender, MouseEventArgs e)
        {
            Editor = (RichTextBox)pestania.SelectedTab.Controls[0];
            marca_llaves_parentesis();
        }

        void Editor_KeyUp(object sender, KeyEventArgs e)
        {
            Editor = (RichTextBox)pestania.SelectedTab.Controls[0];
            marca_llaves_parentesis();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            //RichTextBox r1 = new RichTextBox();
            //r1.Text = "xxx";

            //SaveFileDialog instDeSaveFileDialog = new SaveFileDialog();
            //instDeSaveFileDialog.Filter = "Archivos |*.txt";

            //if (instDeSaveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    r1.SaveFile(instDeSaveFileDialog.FileName, RichTextBoxStreamType.PlainText);
            //}





            TextReader textReader;
            //OpenFileDialog openFD = new OpenFileDialog();
            //openFD.Filter = "Archivos .zs (*.zs)|*.txt";
            //openFD.Title = "Seleccione archivo .txt";
            //openFD.FileName = "";

            button2.Enabled = true;
            button3.Enabled = true;
            guardarToolStripMenuItem.Enabled = true;
            guardarComoToolStripMenuItem.Enabled = true;

            // button5.Enabled = true;
            string fileName = "programa.txt";
            textReader = new StreamReader(fileName);
            string fileText = textReader.ReadToEnd();

            textReader.Dispose();
            //lo que yo hago--
            FileInfo a = new FileInfo(fileName);
            if (pestania.TabPages[a.Name] == null)
            {
                T = new TabPage();
                T.Name = a.Name;
                T.Text = a.Name;
                Editor = new RichTextBox();
                Editor.Name = a.Name + "Ritch";
                //Editor.Size = new Size(376, 554);
                Editor.Size = new Size(428, 303);
                Editor.Text = fileText;
                //myStringNombre = fileText;  //Agregado para que funcione guardar sin haber usado nuevo o abrir

                T.Controls.Add(Editor);
                pestania.TabPages.Add(T);
                Editor.ContextMenuStrip = contextMenuStrip1;
                pestania.SelectTab(a.Name);

                Editor.KeyPress += new KeyPressEventHandler(Editor_PressKey);
                Editor.KeyUp += new KeyEventHandler(Editor_KeyUp);
                Editor.MouseClick += new MouseEventHandler(Editor_MouseClick);
                //termino lo que yo hago
            }
            else pestania.SelectTab(a.Name);
        }


        private void árbolDeDerivaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parser.muestraProducciones = true;
            Parser.muestraCargaDeInstrs = Tab.muestraTabSimb = false;
            tabControl1.SelectedIndex = 0;
            treeView1.Nodes.Clear();
            inicializa();
            compilar();
            //Parser.muestraProducciones =  true;


            //Pars5er.muestraCargaDeInstrs = false;
            //Parser.ejecuta = false;
            //  Tab.muestraTabSimb = Program1.form1.richTextBox10.Visible = false;
            Program1.form1.treeView1.ExpandAll();
        }

        private void tablaDeSímbolosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;

            inicializa();
            Parser.muestraProducciones = Parser.muestraCargaDeInstrs = false;
            Tab.muestraTabSimb = true;
            compilar();
            // this.Close();
        }

        private void instruccionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parser.muestraProducciones = Tab.muestraTabSimb = false;
            Parser.muestraCargaDeInstrs = true;
            tabControl1.SelectedIndex = 2;
            inicializa();

            compilar();
        }

        private void maquVirtualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Width -= 100;
            label1.Visible = label2.Visible = label8.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible = true;
            richTextBox2.Visible = richTextBox4.Visible = true;
            tabControl1.SelectedIndex = 2;
            tabControl2.Visible = false;
            richTextBox8.Visible = richTextBox9.Visible = true;
            maquVirtual();


        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void miniGenerDeCodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"Compiladorcito\Compiladorcito\bin\Debug\";
            // Aqui se introduce el nombre del archivo
            pr.StartInfo.FileName = "Compiladorcito.exe";

            pr.Start();
        }

        private void overviewppsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "01.Overview.ppsx";
            pr.Start();
        }

        private void scanningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "02.Scanning.ppsx";
            pr.Start();

        }
        private void parsingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "03.Parsing.ppsx";
            pr.Start();

        }

        private void semanticProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "04.SemanticProcessing.ppsx";
            pr.Start();
        }

        private void symbolTableParaImprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "05.SymbolTable para Imprimir.ppsx";
            pr.Start();
        }

        private void codeGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "06.CodeGeneration - Fiambala.ppsx";
            pr.Start();
        }

        private void bUParsingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "07.BU-Parsing.ppsx";
            pr.Start();
        }

        private void compilerGeneratorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "08.CompilerGenerators.ppsx";
            pr.Start();
        }

        private void depurarToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void pestania_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        
        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "Autores.ppsx";
            pr.Start();


            //TextReader textReader;

            ////string fileName = "autores.txt";
            //string fileName = "autores.txt";
            //textReader = new StreamReader(fileName);
            //string fileText = textReader.ReadToEnd();
            //textReader.Dispose();
            //string arreglado = "";
            //for (int i = 0; i < fileText.Length; i++)
            //{
            //    int entero = (int)fileText[i];


            //    //MessageBox.Show(fileText[i] + "");
            //    //MessageBox.Show(Convert.ToString(entero));

            //    if (entero == 65533) arreglado = arreglado + (char)241; // "ñ"
            //        else arreglado = arreglado + fileText[i];

            //    //if (entero == 65533) MessageBox.Show("......65533......");;
            //    //else arreglado = arreglado + fileText[i];

            //}
            //richTextBox1.Text = arreglado; 

            //richTextBox1.Visible = true;



            //MessageBox.Show("Continuar");



        }

        private void pPTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void introducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "Introduccion.ppsx";
            pr.Start();

        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"slides\";
            pr.StartInfo.FileName = "Ayuda.ppsx";
            pr.Start();
        }

        private void gramaticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            
            pr.StartInfo.FileName = "z_gramatica.txt";
            pr.Start();
        }

        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            acerca_de f = new acerca_de();
            f.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void inspeccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
    public class ErrorMio : System.Exception
    {
        public int linea, columna, sizeToken; public string msg;
        public ErrorMio(int linea, int columna, int sizeToken, string msg)
        {
            this.linea = linea; this.columna = columna;
            this.sizeToken = sizeToken; this.msg = msg;
        }
    };



}

