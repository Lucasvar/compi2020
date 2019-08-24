using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Drawing;

namespace at.jku.ssw.cc
{
    /// The code generator for the Z# compiler.
    public class AlP2
    {
        public static int iii;
        static void f() { iii = 9; }
    } ;
    public class Code
    {
        public static int segmAnteriorGram = 0;
        static LocalBuilder ultimaVbleLocalDin;
        static bool primeraVez = true;
        public int ii;// = 100;
        //----- shortcuts to the relevant IL instruction codes of System.Reflection.Emit.OpCodes
        public static readonly OpCode
            // loading method arguments
            LDARG0 = OpCodes.Ldarg_0,
            LDARG1 = OpCodes.Ldarg_1,
            LDARG2 = OpCodes.Ldarg_2,
            LDARG3 = OpCodes.Ldarg_3,
            LDARG = OpCodes.Ldarg_S,
            // storing in method argument slots
            STARG = OpCodes.Starg_S,
            // loading local variables
            LDLOC0 = OpCodes.Ldloc_0,
            LDLOC1 = OpCodes.Ldloc_1,
            LDLOC2 = OpCodes.Ldloc_2,
            LDLOC3 = OpCodes.Ldloc_3,
            LDLOC = OpCodes.Ldloc_S,
            // storing local variables
            STLOC0 = OpCodes.Stloc_0,
            STLOC1 = OpCodes.Stloc_1,
            STLOC2 = OpCodes.Stloc_2,
            STLOC3 = OpCodes.Stloc_3,
            STLOC = OpCodes.Stloc_S,
            // loading constant values
            LDNULL = OpCodes.Ldnull,
            LDCM1 = OpCodes.Ldc_I4_M1,
            LDC0 = OpCodes.Ldc_I4_0,
            LDC1 = OpCodes.Ldc_I4_1,
            LDC2 = OpCodes.Ldc_I4_2,
            LDC3 = OpCodes.Ldc_I4_3,
            LDC4 = OpCodes.Ldc_I4_4,
            LDC5 = OpCodes.Ldc_I4_5,
            LDC6 = OpCodes.Ldc_I4_6,
            LDC7 = OpCodes.Ldc_I4_7,
            LDC8 = OpCodes.Ldc_I4_8,
            LDC = OpCodes.Ldc_I4,
            // stack manipulation
            DUP = OpCodes.Dup,
            POP = OpCodes.Pop,
            // method invocation
            CALL = OpCodes.Call,
            RET = OpCodes.Ret,
            // branching
            BR = OpCodes.Br,
            BEQ = OpCodes.Beq,
            BGE = OpCodes.Bge,
            BGT = OpCodes.Bgt,
            BLE = OpCodes.Ble,
            BLT = OpCodes.Blt,
            BNE = OpCodes.Bne_Un,
            // arithmetics
            ADD = OpCodes.Add,
            SUB = OpCodes.Sub,
            MUL = OpCodes.Mul,
            DIV = OpCodes.Div,
            REM = OpCodes.Rem,
            NEG = OpCodes.Neg,
            // field access
            LDFLD = OpCodes.Ldfld,
            STFLD = OpCodes.Stfld,
            LDSFLD = OpCodes.Ldsfld,
            STSFLD = OpCodes.Stsfld,
            // object creation
            NEWOBJ = OpCodes.Newobj,
            NEWARR = OpCodes.Newarr,
            // array handling 
            LDLEN = OpCodes.Ldlen,
            LDELEMCHR = OpCodes.Ldelem_U2,
            LDELEMINT = OpCodes.Ldelem_I4,
            LDELEMREF = OpCodes.Ldelem_Ref,
            STELEMCHR = OpCodes.Stelem_I2,
            STELEMINT = OpCodes.Stelem_I4,
            STELEMREF = OpCodes.Stelem_Ref,

            // exception handling
            THROW = OpCodes.Throw;

        const FieldAttributes GLOBALATTR = FieldAttributes.Assembly | FieldAttributes.Static;
        const FieldAttributes FIELDATTR = FieldAttributes.Assembly;
        const MethodAttributes METHATTR = MethodAttributes.Assembly | MethodAttributes.Static;
        const TypeAttributes INNERATTR = TypeAttributes.Class | TypeAttributes.NotPublic;
        const TypeAttributes PROGATTR = TypeAttributes.Class | TypeAttributes.Public;

        /* quick access to conditional branch instructions */
        static readonly OpCode[] brtrue = { BEQ, BGE, BGT, BLE, BLT, BNE };
        static string[] brtrueString = { "beq", "bge", "bgt", "ble", "blt", "bne" };

        public enum BrtrueENUM { BEQenum, BGEenum, BGTenum, BLEenum, BLTenum, BNEenum };

        static readonly OpCode[] brfalse = { BNE, BLT, BLE, BGT, BGE, BEQ };
        static string[] brfalseString = { "bne", "blt", "ble", "bgt", "bge", "beq" };
        public enum BrfalseENUM { BNEenum, BLTenum, BLEenum, BGTenum, BGEenum, BEQenum };

        public static BrtrueENUM vsrEnum = BrtrueENUM.BEQenum;

        public static BrfalseENUM[] brfalseVar;
        public static BrtrueENUM[] brtrueVar;

        /* No-arg contructor of class System.Object. */
        static readonly ConstructorInfo objCtor = typeof(object).GetConstructor(new Type[0]);

        /* No-arg constructor of class System.ExecutionEngineException, for functions without return */
        internal static readonly ConstructorInfo eeexCtor =
                typeof(System.ExecutionEngineException).GetConstructor(new Type[0]);

        //----- System.Reflection.Emit objects for metadata management	

        public static AssemblyBuilder assembly;  // metadata builder for the program assembly
        static ModuleBuilder module;      // metadata builder for the program module
        public static TypeBuilder program;       // metadata builder for the main class P
        static TypeBuilder inner;         // metadata builder for the currently compiled inner class
        internal static MethodBuilder     // builders for the basic I/O operations provided by the Z# keywords read and write
            readChar, readInt, writeChar, writeInt;
        //MethodBuilder writeStrMthd1;
        internal static ILGenerator il;   // IL stream of currently compiled method

        //----- metadata generation

        /* Creates the required metadata builder objects for the given Symbol.
         * Call this after you inserted you Symbol into the symbol table.
         */

        public static void restaurarRichTextBox1conNegro()
        {
            string myString1 = Program1.form1.Editor.Text;
            //restaura color negro en Editor
            int linea = 0; int col = 0; int sizeToken = myString1.Length;
            Program1.form1.Editor.Select(Program1.form1.Editor.GetFirstCharIndexFromLine(linea)
                                                                       + col,
                                sizeToken);
            Program1.form1.Editor.SelectionColor = System.Drawing.Color.Black;
            Program1.form1.Editor.SelectionFont =
                 new Font(Program1.form1.Editor.Font.FontFamily,
                        Program1.form1.Editor.Font.Size, FontStyle.Regular);
        }

        public static void restaurarRichTextBox3conNegro()
        {
            string myString1 = Program1.form1.richTextBox3.Text;
            //restaura color negro en richTextBox3
            int linea = 0; int col = 0; int sizeToken = myString1.Length;
            Program1.form1.richTextBox3.Select(Program1.form1.richTextBox3.GetFirstCharIndexFromLine(linea)
                                                                       + col,
                                               sizeToken);
            Program1.form1.richTextBox3.SelectionColor = System.Drawing.Color.Black;
            Program1.form1.richTextBox3.SelectionFont =
                     new Font(Program1.form1.richTextBox3.Font.FontFamily,
                              Program1.form1.richTextBox3.Font.Size, FontStyle.Regular);
        }

        public static void restaurarRichTextBox7conNegro()
        {
            string myString1 = Program1.form1.richTextBox7.Text;
            //restaura color negro en richTextBox7
            int linea = 0; int col = 0; int sizeToken = myString1.Length;
            Program1.form1.richTextBox7.Select(Program1.form1.richTextBox7.GetFirstCharIndexFromLine(linea)
                                                                       + col,
                                               sizeToken);
            Program1.form1.richTextBox7.SelectionColor = System.Drawing.Color.Black;
            Program1.form1.richTextBox7.SelectionFont =
                     new Font(Program1.form1.richTextBox7.Font.FontFamily,
                              Program1.form1.richTextBox7.Font.Size, FontStyle.Regular);
        }

        //Inicio Modificacion - Grupo 1 - 10/9/15 - Se cambio tipo del parametro del metodo
        /// <summary>
        /// "token" : Pinta Token
        /// "latoken" : Pinta LaToken
        /// </summary>
        /// <param name="colorearToken"></param>

        public static void Colorear(string pintar)
        {
            int linea1 = -1; int col1 = -1;
            int sizeToken1 = -1; ;
            if (pintar == "token")
            {
                if (Parser.yaPintada)
                {
                    Parser.yaPintada = false; //para los tokens que siguen
                    return;
                }
                else
                {
                    linea1 = Parser.token.line; col1 = Parser.token.col;
                    if (Parser.token.str == null) sizeToken1 = 1;
                    else sizeToken1 = Parser.token.str.Length;
                }
            }
            else
            {   //Pintar = latoken
                linea1 = Parser.laToken.line; col1 = Parser.laToken.col;

                if (Parser.laToken.str == null) sizeToken1 = 1;
                else sizeToken1 = Parser.laToken.str.Length;
                Parser.yaPintada = true;
            }

            restaurarRichTextBox1conNegro();
            Program1.form1.Editor.Select(Program1.form1.Editor.GetFirstCharIndexFromLine(linea1 - 1)+ col1 - 1,sizeToken1);
            //Inicio Modificación Grupo 1 - 31/09/15 - Token = Pinta Rojo ; LaToken = Pinta Verde
            if (pintar == "token")
            {
                Program1.form1.Editor.SelectionColor = System.Drawing.Color.Red;
            }
            else
            {
                Program1.form1.Editor.SelectionColor = System.Drawing.Color.LimeGreen;
            }
            //Fin Modificacion Grupo 1 - 31/09/15
            Program1.form1.Editor.SelectionFont =
                      new Font(Program1.form1.Editor.Font.FontFamily,
                               Program1.form1.Editor.Font.Size, FontStyle.Bold);

            if (linea1 % 14 == 0 && linea1 != 0)
            {
                Program1.form1.Editor.SelectionStart =
                      Program1.form1.richTextBox3.GetFirstCharIndexFromLine((linea1 / 13) * 13);
                Program1.form1.Editor.ScrollToCaret();
            }
            if (Parser.muestraProducciones) Parser.MessageBoxCon3Preg();
        }

        //Fin Modificacion - Grupo 1 - 10/9/15

        public static void cargaInstr(string instr)
        {

            string instrConNroLinea;
            instrConNroLinea = Parser.nroDeInstrCorriente.ToString() + ":" + instr;
            if (Parser.nroDeInstrCorriente < 10) instrConNroLinea = " " + instrConNroLinea;
            string stack = Program1.form1.richTextBox3.Text;
            if (stack == "") Program1.form1.richTextBox3.Text = instrConNroLinea;
            else Program1.form1.richTextBox3.Text = stack + "\n" + instrConNroLinea;
            //Grupo 2 INICIO 23/10
            //Empieza a buscar el ultimo nodo con hijos para colgar la instruccion cil
            System.Windows.Forms.TreeNode previo = Program1.form1.treeView1.Nodes[Program1.form1.treeView1.Nodes.Count - 1].LastNode;//Accedemos al ultimo nodo del primer nivel
            System.Windows.Forms.TreeNode actual = previo;//actual lo usaremos para avanzar en la lista y en previo guardaremos el ultimo nodo que hayamos expandido
            while (actual != null && actual.Nodes.Count > 0)//empieza a buscar la lista   COunt es la cantidad de hijos
            {
                previo = actual;
                actual = actual.Nodes[actual.Nodes.Count - 1].LastNode;
            }
            previo.Nodes.Add("( " + instrConNroLinea + " )");//previo quedo con el ultimo nodo expandido
            //Grupo 2 FIN 301cargaInstr
            string texto = Program1.form1.richTextBox3.Text;
            Program1.form1.richTextBox3.SelectionStart = texto.Length;
            Program1.form1.richTextBox3.ScrollToCaret();
            if (Parser.muestraCargaDeInstrs)
                Program1.form1.instContinuar.ShowDialog();

            //Para ser utilizada despues
            Parser.cil[Parser.nroDeInstrCorriente].instrString = instrConNroLinea;
        }

        public static void cargaProgDeLaGram(string prod)
        {
            if (Parser.muestraProducciones)
            {
                //string texto = Program1.form1.richTextBox6.Text;
                //if (texto == "") Program1.form1.richTextBox6.Text = prod;
                //else Program1.form1.richTextBox6.Text = texto + "\n" + prod;

                //Program1.form1.richTextBox6.SelectionStart = texto.Length;
                //Program1.form1.richTextBox6.ScrollToCaret();
                //Parser.MessageBoxCon3Preg();
            }
        }

        public static void seleccLaProdEnLaGram(int lineaEnLaGramatica)
        {
            int tamDelSegm = 15;  //21

            // G3 PERU Se le suma 9 porque en statement hay 9 lineas en una sola regla.
            // Si se agrega una regla antes de Statement, revisar.
            if (lineaEnLaGramatica > 18)
            {
                lineaEnLaGramatica += 9;
            }
            if (Parser.muestraProducciones)
            {
                Code.restaurarRichTextBox7conNegro();//Gramatica
                string texto = Program1.form1.richTextBox7.Text;
                int sizeToken1 = 2;
                int segActual = lineaEnLaGramatica / tamDelSegm; //De 0 a tamDelSegm-1 (21 -1 = 20) => 0, De tamDelSegm (21) a .. => 1, ....
                if (segActual != segmAnteriorGram)
                {
                    Program1.form1.richTextBox7.SelectionStart =
                          Program1.form1.richTextBox7.GetFirstCharIndexFromLine(segActual * tamDelSegm);


                    Program1.form1.richTextBox7.ScrollToCaret();
                }
                segmAnteriorGram = segActual;
                Program1.form1.richTextBox7.Select(Program1.form1.richTextBox7.GetFirstCharIndexFromLine(lineaEnLaGramatica) + 0, sizeToken1);
                Program1.form1.richTextBox7.SelectionColor = System.Drawing.Color.Red;
                Program1.form1.richTextBox7.SelectionFont = new Font(Program1.form1.richTextBox7.Font.FontFamily, Program1.form1.richTextBox7.Font.Size, FontStyle.Bold);
            }
        }

        internal static void CreateMetadata(Symbol sym)
        {
            switch (sym.kind)
            {
                case Symbol.Kinds.Global:
                    {
                        if (sym.type != Tab.noType)
                            sym.fld = program.DefineField(sym.name, sym.type.sysType, GLOBALATTR);
                        break;
                    }
                case Symbol.Kinds.Field:
                    {
                        if (sym.type != Tab.noType)
                            sym.fld = inner.DefineField(sym.name, sym.type.sysType, FIELDATTR);
                        break;
                    }
                case Symbol.Kinds.Local:
                    {
                        LocalBuilder vbleLocalDin = il.DeclareLocal(sym.type.sysType);
                        if (primeraVez)
                        {
                            ultimaVbleLocalDin = vbleLocalDin;
                            primeraVez = false;
                        }
                        break;
                    }
                case Symbol.Kinds.Type:
                    inner = module.DefineType(sym.name, INNERATTR);
                    sym.type.sysType = inner;
                    // define default contructor (calls base constructor)
                    sym.ctor = inner.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[0]);
                    il = sym.ctor.GetILGenerator();
                    il.Emit(LDARG0);
                    il.Emit(CALL, typeof(object).GetConstructor(new Type[0]));
                    il.Emit(RET);
                    break;
                case Symbol.Kinds.Meth:
                    {
                        sym.meth = program.DefineMethod(sym.name, MethodAttributes.Public, typeof(void), null);
                        il = sym.meth.GetILGenerator();
                        if (sym.name == "Main")
                        {
                            assembly.SetEntryPoint(sym.meth);
                        }
                        break;
                    }

                case Symbol.Kinds.Prog:
                    {
                        AssemblyName assemblyName = new AssemblyName();
                        assemblyName.Name = sym.name;
                        assembly =
                            AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
                        module = assembly.DefineDynamicModule(sym.name + "Module", sym.name + ".exe");//".exe"
                        program = module.DefineType(sym.name, PROGATTR);  //clase din para el program
                        Type objType = Type.GetType("System.Object");
                        ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);
                        ConstructorBuilder pointCtor = program.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[0]);
                        ILGenerator ctorIL = pointCtor.GetILGenerator();
                        // First, you build the constructor.
                        ctorIL.Emit(OpCodes.Ldarg_0);
                        ctorIL.Emit(OpCodes.Call, objCtor);
                        ctorIL.Emit(OpCodes.Ret);
                        inner = null;
                        // build methods for I/O keywords (read, write)
                        BuildReadChar();
                        BuildReadInt();
                        BuildWriteChar();
                        BuildWriteInt();
                        break;
                    }
            }
        }

        /* Completes the system type of an inner class.
         * Call this at end of class declaration. */
        internal static void CompleteClass()
        {
            inner.CreateType();
            inner = null;
        }

        // ---------- instruction generation
        /* Load the operand x onto the expression stack. */
        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
        internal static void Load(Item x)
        {
            switch (x.kind)
            {
                case Item.Kinds.Const:
                    if (x.type == Tab.nullType) il.Emit(LDNULL);
                    else LoadConst(x.val);
                    break;
                case Item.Kinds.Arg:
                    {
                        switch (x.adr)
                        {
                            case 0: il.Emit(LDARG0); break;
                            case 1: il.Emit(LDARG1); break;
                            case 2: il.Emit(LDARG2); break;
                            case 3: il.Emit(LDARG3); break;
                            default: il.Emit(LDARG, x.adr); break;
                        }
                        break;
                    }
                case Item.Kinds.Local:
                    {
                        Parser.nroDeInstrCorriente++;
                        Parser.cil[Parser.nroDeInstrCorriente].accionInstr = Parser.AccionInstr.loadLocal;
                        Parser.cil[Parser.nroDeInstrCorriente].nro = x.adr;
                        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                        switch (x.adr)
                        {
                            case 0: il.Emit(LDLOC0); cargaInstr("ldloc.0   ");
                                break;
                            case 1: il.Emit(LDLOC1); cargaInstr("ldloc.1   ");
                                break;
                            case 2: il.Emit(LDLOC2); cargaInstr("ldloc.2   ");
                                break;
                            case 3: il.Emit(LDLOC3); cargaInstr("ldloc.3   ");
                                break;
                            default:
                                string instr = "ldloc." + x.adr.ToString();
                                il.Emit(LDLOC, x.adr); cargaInstr(instr);
                                break;
                        }
                        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                        break;
                    }
                case Item.Kinds.Static:
                    {
                        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                        if (x.sym.fld != null)
                        {
                            il.Emit(LDSFLD, x.sym.fld);
                            cargaInstr(".field static assembly int32 " + x.sym.name);
                        }
                        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                        break;
                    }
                case Item.Kinds.Stack:
                    break; // nothing to do (already loaded)
                case Item.Kinds.Field:  // assert: object base address is on stack
                    {
                        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                        if (x.sym.fld != null)
                        {
                            il.Emit(LDFLD, x.sym.fld);
                            cargaInstr(".field static assembly int32 " + x.sym.name);
                        };
                        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                        break;
                    }
                case Item.Kinds.Elem: // assert: array base address and index are on stack
                    {
                        if (x.type == Tab.charType) il.Emit(LDELEMCHR);
                        else if (x.type == Tab.intType) il.Emit(LDELEMINT);
                        else if (x.type.kind == Struct.Kinds.Class) il.Emit(LDELEMREF);
                        else Parser.Errors.Error("invalid array element type");
                        break;
                    }
                default:
                    {
                        Parser.Errors.Error("cannot load this value");
                        break;
                    }
            }
            x.kind = Item.Kinds.Stack;
        }//Fin internal static void Load(Item x)

        /* Load an integer constant onto the expression stack. */
        public static string blancos(int cant)
        {
            string bl = "";
            for (int i = 0; i < cant; i++)
                bl = bl + " ";
            return bl;
        }
        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------                    
        internal static void LoadConst(int n)
        {
            Parser.nroDeInstrCorriente++;
            Parser.cil[Parser.nroDeInstrCorriente].accionInstr = Parser.AccionInstr.loadConst;
            Parser.cil[Parser.nroDeInstrCorriente].nro = n;  //aqui

            switch (n)
            {
                case -1: il.Emit(LDCM1); cargaInstr("ldc.i4 -1"); break;
                case 0: il.Emit(LDC0); cargaInstr("ldc.i4 0  "); break;
                case 1: il.Emit(LDC1); cargaInstr("ldc.i4 1  "); break;
                case 2: il.Emit(LDC2); cargaInstr("ldc.i4 2  "); break;
                case 3: il.Emit(LDC3); cargaInstr("ldc.i4 3  "); break;
                default: il.Emit(LDC, n); cargaInstr("ldc.i4 " + n.ToString() + blancos(3 - n.ToString().Length)); break;
            }
        }
        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
        /* Generate an assignment x = y. */
        internal static void Assign(Item izq, Item der, System.Windows.Forms.TreeNode padrecito)
        {
            switch (izq.kind)
            {
                case Item.Kinds.Stack:  //Para el caso de x = 17. izq tendrá kind Stack
                    {
                        switch (izq.sym.kind)
                        {
                            case Symbol.Kinds.Arg:
                                {
                                    //Debo saber que nro de arg
                                    il.Emit(STARG, izq.adr);
                                    break;
                                }
                            case Symbol.Kinds.Local:
                                {
                                    //Debo saber que nro de local
                                    int nroDeArg = izq.adr;
                                    Parser.nroDeInstrCorriente++;
                                    Parser.cil[Parser.nroDeInstrCorriente].accionInstr = Parser.AccionInstr.storeLocal;
                                    Parser.cil[Parser.nroDeInstrCorriente].nro = nroDeArg; //cuando haya args hay restarle la cant de args
                                    //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                                    switch (nroDeArg)
                                    {
                                        case 0: il.Emit(STLOC0); cargaInstr("stloc.0   "); break;
                                        case 1: il.Emit(STLOC1); cargaInstr("stloc.1   "); break;
                                        case 2: il.Emit(STLOC2); cargaInstr("stloc.2   "); break;
                                        case 3: il.Emit(STLOC3); cargaInstr("stloc.3   "); break;
                                        default: il.Emit(STLOC, nroDeArg);
                                            cargaInstr("stloc." + nroDeArg.ToString());
                                            break;
                                    }
                                    //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                                    il.Emit(POP);
                                    //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                                    Parser.nroDeInstrCorriente++; cargaInstr("pop      ");
                                    Parser.cil[Parser.nroDeInstrCorriente].accionInstr = Parser.AccionInstr.pop;
                                    //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                                    break;
                                }
                            case Symbol.Kinds.Global:
                                {
                                    if (izq.sym == null)
                                    {
                                        Console.Write("Error 3032928)"); if (ZZ.readKey) Console.ReadKey();
                                    }
                                    //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                                    il.Emit(STSFLD, izq.sym.fld); cargaInstr(".field static assembly int32 " + izq.sym.name);
                                    il.Emit(POP); Parser.nroDeInstrCorriente++; cargaInstr("pop");
                                    //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                                    break;
                                }
                            default: Parser.Errors.Error("izq.kind=Stack, yo contemplè solo 3 casos: Arg, Local y Global"); break;
                        }//Fin switch (izq.sym.kind)
                        break;
                    }
                case Item.Kinds.Field:
                    {
                        il.Emit(STFLD, izq.sym.fld); cargaInstr(".field static assembly int32 " + izq.sym.name);
                        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
                        break;
                    }
                case Item.Kinds.Elem:
                    {
                        if (izq.type == Tab.intType) il.Emit(STELEMINT);
                        else if (izq.type == Tab.charType) il.Emit(STELEMCHR);
                        else il.Emit(STELEMREF);
                        break;
                    }
                default:
                    {
                        Parser.Errors.Error("caso no contemplado para izq.kind");
                        break;
                    }
            }//Fin switch (izq.kind)
        }//Fin internal static void Assign(Item izq, Item der)

        /* Generate an increment instruction that increments x by n. */
        internal static void Inc(Item x, int n)
        {
            //NADA
        }

        /* Unconditional jump. */
        internal static void Jump(Label lab)
        {
            il.Emit(BR, lab);
        }

        /* True Jump. Generates conditional branch instruction. */
        internal static void TJump(Item x)
        {
            il.Emit(brtrue[x.relop - Token.EQ], x.tLabel);
        }

        /* False Jump. Generates conditional branch instruction. */
        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
        internal static void FJump(Item x)
        {
            il.Emit(brfalse[x.relop - Token.EQ], x.fLabel);

            Parser.nroDeInstrCorriente++;

            Parser.cil[Parser.nroDeInstrCorriente].accionInstr = Parser.AccionInstr.fJump;
            Parser.cil[Parser.nroDeInstrCorriente].nroLinea = -1;  //el Br cond está indefinido
            Parser.cil[Parser.nroDeInstrCorriente].indBrFalse = brfalseVar[x.relop - Token.EQ]; //bge, etc (enum)

            cargaInstr(brfalseString[x.relop - Token.EQ] + "  " + Parser.cil[Parser.nroDeInstrCorriente].nroLinea.ToString());
        }
        //////----------------------------------------------------------------Grupo 2 22/10/2015------------------------------------------------------
        /* Generate an executable .NET-PE-File. */

        public static void WritePEFile()
        {
            program.CreateType();
            if (inner != null) inner.CreateType();
            assembly.Save(assembly.GetName().Name + ".exe");
        }

        static void BuildReadChar()
        {
            readChar = program.DefineMethod("readc", MethodAttributes.Static, typeof(char), new Type[0]);
            il = readChar.GetILGenerator();
            il.EmitCall(CALL, typeof(Console).GetMethod("Read", new Type[0]), null);
            il.Emit(OpCodes.Conv_U2);
            il.Emit(RET);
        }

        static void BuildReadInt()
        {
            readInt = program.DefineMethod("readi", MethodAttributes.Static, typeof(int), new Type[0]);
            il = readInt.GetILGenerator();
            LocalBuilder neg = il.DeclareLocal(typeof(bool));
            il.Emit(LDC0);
            il.Emit(STLOC0);
            LocalBuilder x = il.DeclareLocal(typeof(int));
            il.Emit(LDC0);
            il.Emit(STLOC1);
            LocalBuilder ch = il.DeclareLocal(typeof(char));
            il.EmitCall(CALL, readChar, null);
            il.Emit(STLOC2);
            Label ifEnd = il.DefineLabel();
            il.Emit(LDLOC2);
            il.Emit(LDC, (int)'-');
            il.Emit(BNE, ifEnd);
            il.Emit(LDC1);
            il.Emit(STLOC0);
            il.EmitCall(CALL, readChar, null);
            il.Emit(STLOC2);
            il.MarkLabel(ifEnd);
            Label whileStart = il.DefineLabel();
            Label whileEnd = il.DefineLabel();
            il.MarkLabel(whileStart);
            il.Emit(LDC, (int)'0');
            il.Emit(LDLOC2);
            il.Emit(BGT, whileEnd);
            il.Emit(LDLOC2);
            il.Emit(LDC, (int)'9');
            il.Emit(BGT, whileEnd);
            il.Emit(LDC, 10);
            il.Emit(LDLOC1);
            il.Emit(MUL);
            il.Emit(LDLOC2);
            il.Emit(LDC, (int)'0');
            il.Emit(SUB);
            il.Emit(ADD);
            il.Emit(STLOC1);
            il.EmitCall(CALL, readChar, null);
            il.Emit(STLOC2);
            il.Emit(BR, whileStart);
            il.MarkLabel(whileEnd);
            ifEnd = il.DefineLabel();
            Label elseBranch = il.DefineLabel();
            il.Emit(LDLOC0);
            il.Emit(OpCodes.Brfalse, elseBranch);
            il.Emit(LDLOC1);
            il.Emit(NEG);
            il.Emit(BR, ifEnd);
            il.MarkLabel(elseBranch);
            il.Emit(LDLOC1);
            il.MarkLabel(ifEnd);
            il.Emit(RET);
        }

        static void BuildWriteChar()
        {
            writeChar = program.DefineMethod("write", MethodAttributes.Static, typeof(void), new Type[] { typeof(char), typeof(int) });
            il = writeChar.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "{{0,{0}}}");
            il.Emit(LDARG1);
            il.Emit(OpCodes.Box, typeof(int));
            il.EmitCall(CALL, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object) }), null);
            il.Emit(LDARG0);
            il.Emit(OpCodes.Box, typeof(char));
            il.EmitCall(CALL, typeof(Console).GetMethod("Write", new Type[] { typeof(string), typeof(object) }), null);
            il.Emit(RET);
        }

        static void BuildWriteInt()
        {
            writeInt = program.DefineMethod("write", MethodAttributes.Static, typeof(void), new Type[] { typeof(int), typeof(int) });
            il = writeInt.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "{{0,{0}}}");
            il.Emit(LDARG1);
            il.Emit(OpCodes.Box, typeof(int));
            il.EmitCall(CALL, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object) }), null);
            il.Emit(LDARG0);
            il.Emit(OpCodes.Box, typeof(int));
            il.EmitCall(CALL, typeof(Console).GetMethod("Write", new Type[] { typeof(string), typeof(object) }), null);
            il.Emit(RET);
        }
    }

    /* Z# Code Item.
     * An item stores the attributes of an operand during code generation.
     */
    class Item
    {
        public enum Kinds { Const, Arg, Local, Static, Field, Stack, Elem, Meth, Cond }

        public Kinds kind;            // Const, Local, Static, Stack, Field, Elem, Method, Cond
        public Struct type;           // item type
        public int val;               // Const: value
        public int adr;               // Arg, Local: offset
        public int relop;             // Cond: token code of relational operator
        public Symbol sym;            // Field, Meth: node from symbol table
        public Label tLabel, fLabel;  // Cond: true jumps, false jumps

        public Item(Symbol sym)
        {
            type = sym.type;
            this.sym = sym;
            switch (sym.kind)
            {
                case Symbol.Kinds.Const: kind = Kinds.Const; val = sym.val; break;
                case Symbol.Kinds.Arg: kind = Kinds.Arg; adr = sym.adr; break;
                case Symbol.Kinds.Local: kind = Kinds.Local; adr = sym.adr; break;
                case Symbol.Kinds.Global: kind = Kinds.Static; break;
                case Symbol.Kinds.Field: kind = Kinds.Field; break;
                case Symbol.Kinds.Meth: kind = Kinds.Meth; break;
                default: Parser.Errors.Error(ErrorStrings.CREATE_ITEM); break;
            }
        }

        // special constructor for Const Items
        public Item(int val) { kind = Kinds.Const; type = Tab.intType; this.val = val; }

        // special constructor for Cond Items
        public Item(int relop, Struct type)
        {
            this.kind = Kinds.Cond;
            this.type = type;
            this.relop = relop;
            tLabel = Code.il.DefineLabel();
            fLabel = Code.il.DefineLabel();
        }

        // special constructor for Stack Items
        internal Item(Struct type)
        {
            kind = Kinds.Stack;
            this.type = type;
        }
    }

}
