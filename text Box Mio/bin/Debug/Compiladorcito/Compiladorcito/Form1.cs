using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Diagnostics;

namespace Compiladorcito
{
    public partial class Form1 : Form
    {
        bool band = true;
        public Form1()
        {
            InitializeComponent();
        }


        private void boton_compilar_Click(object sender, EventArgs e)
        {
            
            //GENERAMOS CODIGO PARA EL PRIMER EJEMPLO: "INT"
            if (tabControl1.SelectedTab.Text=="Ejemplo1")
            { 
                contador = 0;
                Pila_C.Text = "";
                Pila.Text = "";

                MethodBuilder meth;
                AssemblyBuilder assembly;  // metadata builder for the program assembly
                ModuleBuilder module;      // metadata builder for the program module
                TypeBuilder program;       // metadata builder for the main class P

                MethodBuilder writeInt;
                //MethodBuilder writeStrMthd1;
                ILGenerator il,ilw;

                AssemblyName assemblyName = new AssemblyName();
                assemblyName.Name = "ProgPpal1";

                assembly =
                    AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,   //originalm:Save
                                           AssemblyBuilderAccess.RunAndSave);
                module = assembly.DefineDynamicModule(assemblyName + "Module", assemblyName + ".exe");//".exe"
                program = module.DefineType("ProgPpal1", TypeAttributes.Class | TypeAttributes.Public);  //clase din para el program
                
                meth = program.DefineMethod("Main", MethodAttributes.Public, typeof(void), null);
                il = meth.GetILGenerator();
                assembly.SetEntryPoint(meth);
                
                Type objType = Type.GetType("System.Object");
                ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);
                ConstructorBuilder pointCtor =
                      program.DefineConstructor(MethodAttributes.Public,
                                        CallingConventions.Standard, new Type[0]);
                ILGenerator ctorIL = pointCtor.GetILGenerator();

                // First, you build the constructor.
                ctorIL.Emit(OpCodes.Ldarg_0);
                //ctorIL.EmitWriteLine("...dentro del constr de class P..."); //funca bien
                ctorIL.Emit(OpCodes.Call, objCtor);
                ctorIL.Emit(OpCodes.Ret);

                writeInt = program.DefineMethod("write", MethodAttributes.Static,
                                               typeof(void),
                                               new Type[] { typeof(int), typeof(int) });
                ilw = writeInt.GetILGenerator();

                // System.Console.Write(System.String.Format("{{0,{0}}}", width), x)
                ilw.Emit(OpCodes.Ldstr, "{{0,{0}}}");
                ilw.Emit(OpCodes.Ldarg_1);
                ilw.Emit(OpCodes.Box, typeof(int));
                ilw.EmitCall(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object) }), null);

                ilw.Emit(OpCodes.Ldarg_0);
                ilw.Emit(OpCodes.Box, typeof(int));
                ilw.EmitCall(OpCodes.Call, typeof(Console).GetMethod("Write", new Type[] { typeof(string), typeof(object) }), null);
                ilw.Emit(OpCodes.Ret); 

                
                // Console.WriteLine("pasa por assembly.SetEntryPoint(sym.meth)");

                //LocalBuilder vbleLocalDin = il.DeclareLocal(int);

                Pila_C.Text += (".entrypoint .maxstack  1\n");
                Pila.Text += (" 0:.locals init(int32 V_0 \n"); Pila_C.Text += (".locals init ([0] int32 i)\n");

                il.Emit(OpCodes.Ldc_I4_1); Pila.Text += ("1: ldc.i4 1  \n"); Pila_C.Text += ("IL_0000:  nop\n"); Pila_C.Text += ("IL_0001:  ldc.i4.1 \n");
                il.Emit(OpCodes.Stloc_0); Pila.Text += ("2:  stloc.0  \n"); Pila_C.Text += ("IL_0002:  stloc.0 \n");
                il.Emit(OpCodes.Pop); Pila.Text += ("3: Pop  \n");
                il.Emit(OpCodes.Ldloc_0); Pila.Text += ("4: ldloc.0   \n"); Pila_C.Text += ("IL_0003:  ldloc.0 \n");
                il.Emit(OpCodes.Ldc_I4_3); Pila.Text += ("5: ldc.i4 3  \n");
                il.EmitCall(OpCodes.Call, writeInt, null); //Probar   provisorio
                il.Emit(OpCodes.Ret);

                // void write (int x, int width) 
              
                
                Pila.Text += ("6: call write#(int32,int32)  \n"); Pila_C.Text += ("IL_0004:  call       void [mscorlib]System.Console::WriteLine(int32) \n");
                Pila.Text += ("7: Ret  \n"); Pila_C.Text += ("IL_0009:  nop \n"); Pila_C.Text += ("IL_000a:  ret \n");

                ///// PARTE FINAL EN EL COMPILADOR GRANDE; METODO: ParteFinal1 de la clase Parser!!!
                
                Type ptType1 = program.CreateType();
                object ptInstance1 =
                        Activator.CreateInstance(ptType1, new object[0]);  //new object[0] si sin parms
                //ptType1.InvokeMember("Main", BindingFlags.InvokeMethod, null, ptInstance1, new object[0]);
                ptType1.InvokeMember("Main", BindingFlags.CreateInstance, null, ptInstance1, new object[0]);
                
                assembly.Save("Piripipi7" + ".exe");
                //Console.WriteLine("\nTermina ret, create type, createInst, Invoke Main");
                
                //FIN PARTE FINAL
                
                /*
                 il.Emit(OpCodes.Ldc_I4_1);
                 il.Emit(OpCodes.Ldc_I4_3);
                 Pila_C.Text += OpCodes.Add.ToString();
                 Pila_C.Text += OpCodes.Ldc_I4_1.ToString();
                 */

            }
            //GENERAMOS CODIGO PARA EL SEGUNDO EJEMPLO: "FLOAT"
            if (tabControl1.SelectedTab.Text == "Ejemplo2")
            {

                contador = 0;
                Pila_C.Text = "";
                Pila.Text = "";

                MethodBuilder meth;
                AssemblyBuilder assembly;  // metadata builder for the program assembly
                ModuleBuilder module;      // metadata builder for the program module
                TypeBuilder program;       // metadata builder for the main class P

                MethodBuilder writeFloat;
                //MethodBuilder writeStrMthd1;
                ILGenerator il,ilw;

                AssemblyName assemblyName = new AssemblyName();
                assemblyName.Name = "PProgram";

                assembly =
                    AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,   //originalm:Save
                                           AssemblyBuilderAccess.RunAndSave);
                module = assembly.DefineDynamicModule(assemblyName + "Module", assemblyName + ".exe");//".exe"
                program = module.DefineType("Main", TypeAttributes.Class | TypeAttributes.Public);  //clase din para el program

                Type objType = Type.GetType("System.Object");
                ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);
                ConstructorBuilder pointCtor =
                      program.DefineConstructor(MethodAttributes.Public,
                                        CallingConventions.Standard, new Type[0]);
                ILGenerator ctorIL = pointCtor.GetILGenerator();

                // First, you build the constructor.
                ctorIL.Emit(OpCodes.Ldarg_0);
                //ctorIL.EmitWriteLine("...dentro del constr de class P..."); //funca bien
                ctorIL.Emit(OpCodes.Call, objCtor);
                ctorIL.Emit(OpCodes.Ret);



                meth = program.DefineMethod("Main", MethodAttributes.Public, typeof(void), null);
                //METHATTR (que es lo que viene originalm) no funca
                //sym.meth = program.DefineMethod("Main", METHATTR, sym.type.sysType, null); // args);  //Provis
                //il = writeStrMthd1.GetILGenerator();

                il = meth.GetILGenerator();

                //assembly.SetEntryPoint(meth);
                // Console.WriteLine("pasa por assembly.SetEntryPoint(sym.meth)");

                //LocalBuilder vbleLocalDin = il.DeclareLocal(int);
                writeFloat = program.DefineMethod("write", MethodAttributes.Static,
                                               typeof(void),
                                               new Type[] { typeof(float), typeof(int) });
                ilw = writeFloat.GetILGenerator();

                // System.Console.Write(System.String.Format("{{0,{0}}}", width), x)
                ilw.Emit(OpCodes.Ldstr, "{{0,{0}}}");
                ilw.Emit(OpCodes.Ldarg_1);
                ilw.Emit(OpCodes.Box, typeof(float));
                ilw.EmitCall(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object) }), null);

                ilw.Emit(OpCodes.Ldarg_0);
                ilw.Emit(OpCodes.Box, typeof(int));
                ilw.EmitCall(OpCodes.Call, typeof(Console).GetMethod("Write", new Type[] { typeof(string), typeof(object) }), null);
                ilw.Emit(OpCodes.Ret); 

                
                Pila_C.Text += (".entrypoint .maxstack  1\n");
                Pila.Text += (" 0:.locals init(float32 V_0 \n"); Pila_C.Text += (" .locals init ([0] float32 x)\n");

                il.Emit(OpCodes.Ldc_I4_1); Pila.Text += ("1: Ldc_R4 (56 0E 49 40)  \n"); Pila_C.Text += ("IL_0000:  nop\n"); Pila_C.Text += ("IL_0001:  ldc.r4 (56 0E 49 40) \n");
                il.Emit(OpCodes.Stloc_0); Pila.Text += ("2:  stloc.0  \n"); Pila_C.Text += ("IL_0006:  stloc.0 \n");
                il.Emit(OpCodes.Pop); Pila.Text += ("3: Pop  \n");
                il.Emit(OpCodes.Ldloc_0); Pila.Text += ("4: ldloc.0   \n"); Pila_C.Text += ("IL_0007:  ldloc.0 \n");
                il.Emit(OpCodes.Ldc_I4_3); Pila.Text += ("5: ldc.i4 3  \n");
                il.EmitCall(OpCodes.Call, writeFloat, null); 
                il.Emit(OpCodes.Ret);

                // void write (int x, int width) 
                


                Pila.Text += ("6: call write#(float32,int32)  \n"); Pila_C.Text += ("IL_0008:  call       void [mscorlib]System.Console::WriteLine(float32) \n");
                Pila.Text += ("7: Ret  \n"); Pila_C.Text += ("IL_000d:  nop \n"); Pila_C.Text += ("IL_000e:  ret \n");

                ///// PARTE FINAL EN EL COMPILADOR GRANDE; METODO: ParteFinal1 de la clase Parser!!!
                Type ptType1 = program.CreateType();
                object ptInstance1 =
                        Activator.CreateInstance(ptType1, new object[0]);  //new object[0] si sin parms
                //ptType1.InvokeMember("Main", BindingFlags.InvokeMethod, null, ptInstance1, new object[0]);
                ptType1.InvokeMember("Main", BindingFlags.CreateInstance, null, ptInstance1, new object[0]);
                assembly.Save("Piripipi7" + ".exe");
                //FIN PARTE FINAL
                

            }
          


        }


        public int contador=0;
        private void button1_Click(object sender, EventArgs e)
        { 
            //GENERAMOS CODIGO PASO A PASO PARA EL PRIMER EJEMPLO: "INT"
            if (tabControl1.SelectedTab.Text == "Ejemplo1")
            {
            MethodBuilder meth;
            AssemblyBuilder assembly;  // metadata builder for the program assembly
            ModuleBuilder module;      // metadata builder for the program module
            TypeBuilder program;       // metadata builder for the main class P
            
            MethodBuilder    writeInt;
            //MethodBuilder writeStrMthd1;
            ILGenerator il,ilw;

            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "PProgPal1";
            
            assembly =
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,   //originalm:Save
                                       AssemblyBuilderAccess.RunAndSave);
            module = assembly.DefineDynamicModule(assemblyName + "Module", assemblyName + ".exe");//".exe"
            program = module.DefineType("Main", TypeAttributes.Class | TypeAttributes.Public);  //clase din para el program

            Type objType = Type.GetType("System.Object");
            ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);
            ConstructorBuilder pointCtor =
                  program.DefineConstructor(MethodAttributes.Public,
                                    CallingConventions.Standard, new Type[0]);
            ILGenerator ctorIL = pointCtor.GetILGenerator();

            // First, you build the constructor.
            ctorIL.Emit(OpCodes.Ldarg_0);
            //ctorIL.EmitWriteLine("...dentro del constr de class P..."); //funca bien
            ctorIL.Emit(OpCodes.Call, objCtor);
            ctorIL.Emit(OpCodes.Ret);

            

            meth = program.DefineMethod("Main", MethodAttributes.Public, typeof(void), null);
            //METHATTR (que es lo que viene originalm) no funca
            //sym.meth = program.DefineMethod("Main", METHATTR, sym.type.sysType, null); // args);  //Provis
            //il = writeStrMthd1.GetILGenerator();

            il = meth.GetILGenerator();

            assembly.SetEntryPoint(meth);
            // Console.WriteLine("pasa por assembly.SetEntryPoint(sym.meth)");

            //LocalBuilder vbleLocalDin = il.DeclareLocal(int);

            writeInt = program.DefineMethod("write", MethodAttributes.Static,
                                           typeof(void),
                                           new Type[] { typeof(int), typeof(int) });
            ilw = writeInt.GetILGenerator();

            // System.Console.Write(System.String.Format("{{0,{0}}}", width), x)
            ilw.Emit(OpCodes.Ldstr, "{{0,{0}}}");
            ilw.Emit(OpCodes.Ldarg_1);
            ilw.Emit(OpCodes.Box, typeof(int));
            ilw.EmitCall(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object) }), null);

            ilw.Emit(OpCodes.Ldarg_0);
            ilw.Emit(OpCodes.Box, typeof(int));
            ilw.EmitCall(OpCodes.Call, typeof(Console).GetMethod("Write", new Type[] { typeof(string), typeof(object) }), null);
            ilw.Emit(OpCodes.Ret);
           
            switch (contador)
            {
                case 0:
                    contador = 0;
                    Pila_C.Text = "";
                    Pila.Text = "";
                    Pila_C.Text += (".entrypoint .maxstack  1\n");
                    Pila.Text += (" 0:.locals init(int32 V_0 \n"); Pila_C.Text += (".locals init ([0] int32 i)\n");
                    contador++;
                    break;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1); Pila.Text += ("1: ldc.i4 1  \n"); Pila_C.Text += ("IL_0000:  nop\n"); Pila_C.Text += ("IL_0001:  ldc.i4.1 \n");
                    contador++;
                    break;
                case 2:
                    il.Emit(OpCodes.Stloc_0); Pila.Text += ("2:  stloc.0  \n"); Pila_C.Text += ("IL_0002:  stloc.0 \n");
                    il.Emit(OpCodes.Pop); Pila.Text += ("3: Pop  \n");
                    contador++;
                    break;
                case 3:                    
                    il.Emit(OpCodes.Ldloc_0); Pila.Text += ("4: ldloc.0   \n"); Pila_C.Text += ("IL_0003:  ldloc.0 \n");
                    il.Emit(OpCodes.Ldc_I4_3); Pila.Text += ("5: ldc.i4 3  \n");
                    contador++;
                    break;
                case 4:
                    
    
                         Pila.Text += ("6: call write#(int32,int32)  \n"); Pila_C.Text += ("IL_0004:  call       void [mscorlib]System.Console::WriteLine(int32) \n");
                         Pila.Text += ("7: Ret  \n"); Pila_C.Text += ("IL_0009:  nop \n"); Pila_C.Text += ("IL_000a:  ret \n");

                         il.EmitCall(OpCodes.Call, writeInt, null);
                         ///// PARTE FINAL EN EL COMPILADOR GRANDE; METODO: ParteFinal1 de la clase Parser!!!
                         Type ptType1 = program.CreateType();
                         object ptInstance1 =
                                 Activator.CreateInstance(ptType1, new object[0]);  //new object[0] si sin parms
                         //ptType1.InvokeMember("Main", BindingFlags.InvokeMethod, null, ptInstance1, new object[0]);
                         ptType1.InvokeMember("Main", BindingFlags.CreateInstance, null, ptInstance1, new object[0]);
                         assembly.Save("Piripipi7" + ".exe");
                         //FIN PARTE FINAL
                         
                         contador++;
                    break;
                case 5:
                    contador++;
                    break;
                case 6:
                    contador = 0;
                    Pila_C.Text = "";
                    Pila.Text = "";
                    break;
                 


            }
        }

            //GENERAMOS CODIGO PASO A PASO PARA EL SEGUNDO EJEMPLO: "FLOAT"
            if (tabControl1.SelectedTab.Text == "Ejemplo2")
            {
                MethodBuilder meth;
                AssemblyBuilder assembly;  // metadata builder for the program assembly
                ModuleBuilder module;      // metadata builder for the program module
                TypeBuilder program;       // metadata builder for the main class P

                MethodBuilder writeFloat;
                //MethodBuilder writeStrMthd1;
                ILGenerator il,ilw;

                AssemblyName assemblyName = new AssemblyName();
                assemblyName.Name = "PProgram";

                assembly =
                    AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,   //originalm:Save
                                           AssemblyBuilderAccess.RunAndSave);
                module = assembly.DefineDynamicModule(assemblyName + "Module", assemblyName + ".exe");//".exe"
                program = module.DefineType("Main", TypeAttributes.Class | TypeAttributes.Public);  //clase din para el program

                Type objType = Type.GetType("System.Object");
                ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);
                ConstructorBuilder pointCtor =
                      program.DefineConstructor(MethodAttributes.Public,
                                        CallingConventions.Standard, new Type[0]);
                ILGenerator ctorIL = pointCtor.GetILGenerator();

                // First, you build the constructor.
                ctorIL.Emit(OpCodes.Ldarg_0);
                //ctorIL.EmitWriteLine("...dentro del constr de class P..."); //funca bien
                ctorIL.Emit(OpCodes.Call, objCtor);
                ctorIL.Emit(OpCodes.Ret);



                meth = program.DefineMethod("Main", MethodAttributes.Public, typeof(void), null);
                //METHATTR (que es lo que viene originalm) no funca
                //sym.meth = program.DefineMethod("Main", METHATTR, sym.type.sysType, null); // args);  //Provis
                //il = writeStrMthd1.GetILGenerator();

                il = meth.GetILGenerator();

                assembly.SetEntryPoint(meth);
                // Console.WriteLine("pasa por assembly.SetEntryPoint(sym.meth)");
                writeFloat = program.DefineMethod("write", MethodAttributes.Static,
                                               typeof(void),
                                               new Type[] { typeof(float), typeof(int) });
                ilw = writeFloat.GetILGenerator();

                // System.Console.Write(System.String.Format("{{0,{0}}}", width), x)
                ilw.Emit(OpCodes.Ldstr, "{{0,{0}}}");
                ilw.Emit(OpCodes.Ldarg_1);
                ilw.Emit(OpCodes.Box, typeof(float));
                ilw.EmitCall(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object) }), null);

                ilw.Emit(OpCodes.Ldarg_0);
                ilw.Emit(OpCodes.Box, typeof(int));
                ilw.EmitCall(OpCodes.Call, typeof(Console).GetMethod("Write", new Type[] { typeof(string), typeof(object) }), null);
                ilw.Emit(OpCodes.Ret); 
                //LocalBuilder vbleLocalDin = il.DeclareLocal(int);

                switch (contador)
                {
                    case 0:
                        contador = 0;
                        Pila_C.Text = "";
                        Pila.Text = "";
                        Pila_C.Text += (".entrypoint .maxstack  1\n");
                        Pila.Text += (" 0:.locals init(float32 V_0 \n"); Pila_C.Text += (" .locals init ([0] float32 x)\n");
                        contador++;
                        break;
                    case 1:
                        il.Emit(OpCodes.Ldc_I4_1); Pila.Text += ("1: Ldc_R4 (56 0E 49 40)  \n"); Pila_C.Text += ("IL_0000:  nop\n"); Pila_C.Text += ("IL_0001:  ldc.r4 (56 0E 49 40) \n");
                        contador++;
                        break;
                    case 2:
                        il.Emit(OpCodes.Stloc_0); Pila.Text += ("2:  stloc.0  \n"); Pila_C.Text += ("IL_0006:  stloc.0 \n");
                        il.Emit(OpCodes.Pop); Pila.Text += ("3: Pop  \n");
                        contador++;
                        break;
                    case 3:
                        il.Emit(OpCodes.Ldloc_0); Pila.Text += ("4: ldloc.0   \n"); Pila_C.Text += ("IL_0007:  ldloc.0 \n");
                        il.Emit(OpCodes.Ldc_I4_3); Pila.Text += ("5: ldc.i4 3  \n");
                        contador++;
                        break;
                    case 4:
                        il.EmitCall(OpCodes.Call, writeFloat, null);

                        Pila.Text += ("6: call write#(float32,int32)  \n"); Pila_C.Text += ("IL_0008:  call       void [mscorlib]System.Console::WriteLine(float32) \n");
                        Pila.Text += ("7: Ret  \n"); Pila_C.Text += ("IL_000d:  nop \n"); Pila_C.Text += ("IL_000e:  ret \n");

                        ///// PARTE FINAL EN EL COMPILADOR GRANDE; METODO: ParteFinal1 de la clase Parser!!!
                        Type ptType1 = program.CreateType();
                        object ptInstance1 =
                                Activator.CreateInstance(ptType1, new object[0]);  //new object[0] si sin parms
                        //ptType1.InvokeMember("Main", BindingFlags.InvokeMethod, null, ptInstance1, new object[0]);
                        ptType1.InvokeMember("Main", BindingFlags.CreateInstance, null, ptInstance1, new object[0]);
                        assembly.Save("Piripipi7" + ".exe");
                        //FIN PARTE FINAL
                        contador++;
                        break;
                    case 5:
                        contador++;
                        break;
                    case 6:
                        contador = 0;
                        Pila_C.Text = "";
                        Pila.Text = "";
                        break;



                }
            }

           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            a.Visible = false;

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Codigo3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (band == true)
            {
                if (tabControl1.SelectedTab.Text == "Ejemplo1")
                {
                    a.Load("writeInt.png");
                    a.Visible = true;
                    a.Show();
                }
                else
                {
                    a.Load("writeFloat.png");
                    a.Visible = true;
                    a.Show();
                }
                band = false;
            }
            else
            { a.Visible = false;
            band = true;
            }
            /*
            Process pr = new Process();
            pr.StartInfo.WorkingDirectory = @"Compiladorcito\Compiladorcito\bin\Debug\";
            // Aqui se introduce el nombre del archivo
            pr.StartInfo.FileName = "Compiladorcito.exe";

            pr.Start();*/

            /*
             pr.StartInfo.FileName = "01.Overview.ppsx";
             pr.StartInfo.FileName = "02.Scanning.ppsx"; 
             pr.StartInfo.FileName = "03.Parsing.ppsx";
             pr.StartInfo.FileName = "04.SemanticProcessing.ppsx";
              pr.StartInfo.FileName = "05.SymbolTable para Imprimir.ppsx";
              pr.StartInfo.FileName = "06.CodeGeneration - Fiambala.ppsx";
               pr.StartInfo.FileName = "07.BU-Parsing.ppsx";
               pr.StartInfo.FileName = "08.CompilerGenerators.ppsx";
             */


        }
    }
}
