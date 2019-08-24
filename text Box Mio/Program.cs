using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using at.jku.ssw.cc;
//using at.jku.ssw.cc.tests;
using System.Reflection.Emit;

namespace at.jku.ssw.cc //Compilador
{
    class Program
    {
        //static int i, j;
        public static void Main1(string progr) //string[] args)
        {
            Parser.inicializaCil();

            /* Quitar los comentarios para que funcione el Scanner */
            if (ZZ.Program) Console.WriteLine("Main Compilador 2" ); 
            //char cc1 = (char) 47; char cc2 = (char)50; //char cc1 = 'A'; char cc2 = 'z';
            //for (int i= (int)cc1; i<= (int)cc2; i++) Console.Write("char "+i+" " + (char)i + "   "); 
            //for (int i= 30; i<= 130; i++) 
            //   { Console.Write("char "+i+" " + (char)i + "   ");
            //   if (Math.Floor((double)i / 6) == (double)i / 6) Console.WriteLine();
            //   };
            //Console.ReadKey();
            //Console.WriteLine("ñ:"+(int)'ñ'+" char " + '\u0080'+"FIN----"); Console.ReadKey();
            //Console.WriteLine("1uno" +  "\u000A...este es un LF"+ "\n2dos\r3tres"); Console.ReadKey();
            //Console.WriteLine("1uno" + "\n2dos" + "\u000A...este es un LF...(el 2dos se ve)"); Console.ReadKey();
            //Console.WriteLine("1uno" + "\n2dos" + (char)10+ "...este es un LF...(el 2dos se ve)"); Console.ReadKey();
            //Console.WriteLine("aaa\'bbb"); Console.ReadKey();

           // ScannerTest SCTest = new ScannerTest();
            if (ZZ.Principal)
            {
                Console.WriteLine("ha pasado new ScannerTest()"); // if (ZZ.Program) Console.ReadKey();
                //SCTest.AllTokens();                          // ok
                //Console.WriteLine("ha pasado SCTest.AllTokens()");  Console.ReadKey();

                //SCTest.CRLFLineSeparators();                 // ok
                Console.WriteLine("ha pasado SCTest.CRLFLineSeparators()"); //Console.ReadKey();

                //SCTest.LFLineSeparators();                   // ok
                Console.WriteLine("ha pasado SCTest.LFLineSeparators"); //Console.ReadKey();
                //SCTest.SimpleSingleLineComment();            // ok
                //SCTest.SimpleMultiLineComment();             // ok
            }; 
             //SCTest.NestedSingleLineComment();            // ok
             //SCTest.NestedMultiLineComment();             // ok
             //SCTest.CharConstants();                      // ok
             //SCTest.ESCsequences();                       // ok
             //Console.WriteLine("hasta aqui ok"); //Console.ReadKey();
             //SCTest.InvalidSymbols();                     // ok

             //ZZ.Program = ZZ.w = true;
             if (ZZ.Program)
             {
                 Console.WriteLine("pasó InvalidSymbols()\n\nTERMINÓ TODO EL SCANNER");
                 //Console.ReadKey();
             }
            
        //    ParserTest Ptest = new ParserTest();
            //Ptest.ShortestProgram();
            //Ptest.DoNothingProgram();
            //Ptest.RefTypeComparison();

            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            //Ptest.ScriptExample();   //Ptest (PROGAMA TEST) ES POR DONDE HAY QUE COMENZAR
           // Ptest.ScriptExampleDesdeFile(progr);
            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////

            //TabTest tabTest = new TabTest(); 
            //    tabTest.InitSymTab(); 
            //tabTest.Universe();
            //tabTest.InsertIntoUniverse();
            //tabTest.InsertFields();
            //tabTest.InsertLocals();
            //tabTest.InsertMaxLocals();
            //tabTest.AssignableTo();
            //ZZ.Program = true;
            if (ZZ.Program) Console.WriteLine("Tab.mostrarTab().....al final"); //Console.ReadKey();

            if (ZZ.Program) Tab.mostrarTab();

            //Tab.mostrarTab();
            //Program1.form1.richTextBox10.Text = Tab.tabSimbString;

            
            ZZ.Program = false;
            if (ZZ.Program) Console.WriteLine("TERMINA TODO");
            if (ZZ.Program) { Console.ReadKey(); }

            //Console.WriteLine("{{2,{1}}}", 454, 4);
            
            //Code.il.Emit(Code.LDC4);
            //Code.il.EmitCall(Code.CALL, Code.writeInt, null); //Probar

            //MethodBuilder  writeInt = program.DefineMethod("write", MethodAttributes.Static,
            //                    typeof(void),
            //                    new Type[] { typeof(int), typeof(int) });
            //il = writeInt.GetILGenerator();

            //Code.il.Emit(OpCodes.Ldstr, "{{0,{0}}}");
            //Code.il.Emit(OpCodes.Ldc_I4_4);//
            //Code.il.Emit(OpCodes.Box, typeof(int));
            //Code.il.EmitCall(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object) }), null);

            //Code.il.Emit(OpCodes.Ldc_I4_3);//
            //Code.il.Emit(OpCodes.Box, typeof(int));
            //Code.il.EmitCall(OpCodes.Call, typeof(Console).GetMethod("Write", new Type[] { typeof(string), typeof(object) }), null);

            //Code.il.EmitCall(Code.CALL, Code.writeInt, null);
        }
    }
}
