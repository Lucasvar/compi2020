using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
// agregado por Manuel
using System.Collections;
using System.Collections.Generic;

namespace at.jku.ssw.cc {
public class Tab {
    
    
    public static int profundidad = 0;
    static Stack<TreeNode> ultimosNodos = new Stack<TreeNode>();
    static Stack<List<TreeNode>> ultimosParametros = new Stack<List<TreeNode>>();
    public static TreeNode ultimoNodo;



    public static bool muestraTabSimb = false;
    public static string tabSimbString = "";
	public static readonly Struct
		noType = new Struct(Struct.Kinds.None),
		intType = new Struct(Struct.Kinds.Int),
		charType = new Struct(Struct.Kinds.Char),stringType = new Struct(Struct.Kinds.String),
		nullType = new Struct(Struct.Kinds.Class);

    public static void mostrarTab()  
    {
        tabSimbString = "";
        //Console.WriteLine("\n======== TABLA DE SIMBOLOS ============");
        tabSimbString = tabSimbString + "==== TABLA DE SIMBOLOS =====\n";

        for (Symbol cur = Tab.topScope.locals; cur != null; cur = cur.next)
        {//mostrar un Symbol y sus descendientes
            cur.mostrarSymbol("");
        };
        //Console.WriteLine("======== FIN TABLA DE SIMBOLOS ============"); 
        tabSimbString = tabSimbString + "== FIN TABLA DE SIMBOLOS ===\n";
        // if (ZZ.parser) Console.ReadKey(); 

        if (muestraTabSimb)
        {
            //System.Windows.Forms.MessageBox.Show("Continuar");
            Parser.MessageBoxCon3Preg();
        }
    }

    /* Symbol to indicate that an error has occurred in the symbol table */
    public static readonly Symbol noSym = new Symbol(Symbol.Kinds.Const, "noSymbol", noType);
	
	public static Symbol chrSym, ordSym, lenSym;
	
	internal static Scope topScope;  // current scope

	/* Sets up the "universe" (= predefined names). */
    
	public static void Init () {
        
        //Codigo Tabla de Simbolos
        Program1.form1.arbolTS.Nodes.Clear();
        profundidad = 0;
        //Codigo Tabla de Simbolos


        // tipos estándar: int, char
        Tab.OpenScope(null);  //topScope queda apuntando al Scope p/el universe
        Program1.form1.arbolTS.Nodes.Add("Scope Universe");
        Tab.Insert(Symbol.Kinds.Type, "int", Tab.intType);
        Tab.Insert(Symbol.Kinds.Type, "char", Tab.charType);
        Tab.Insert(Symbol.Kinds.Const, "null", Tab.nullType);
        //Métodos estándares
        
        //chr(i)
        chrSym = Tab.Insert(Symbol.Kinds.Meth, "chr", Tab.charType);
        Tab.OpenScope(chrSym);
        Tab.Insert(Symbol.Kinds.Arg, "i", Tab.intType); //el arg q toma es int (Un Symbol char)
        //Program1.form1.arbolTS.Nodes[0].Nodes[3].Nodes.Add("i | ARG");
        chrSym.nArgs = topScope.nArgs; //Fran 
        chrSym.nLocs = topScope.nLocs; //Fran
        chrSym.locals = Tab.topScope.locals;
        Tab.CloseScope();//Fran cierra el Scope para el arg "i"
        Tab.mostrarTab();

        // ord(ch)
        ordSym = Tab.Insert(Symbol.Kinds.Meth, "ord", Tab.intType);//devuelve int
        //Program1.form1.arbolTS.Nodes[0].Nodes.Add("ord | METH");                                                    //char=tipo q devuelve el met (Struct)
        Tab.OpenScope(ordSym);
        Tab.Insert(Symbol.Kinds.Arg, "ch", Tab.charType);//el arg es char
        //Program1.form1.arbolTS.Nodes[0].Nodes[4].Nodes.Add("ch | ARG");
        ordSym.nArgs = topScope.nArgs; //Fran
        ordSym.nLocs = topScope.nLocs; //Fran
        ordSym.locals = Tab.topScope.locals;
        Tab.CloseScope();//Fran cierra el Scope para el arg "ch"
        Tab.mostrarTab();

        //len(arr)
        lenSym = Tab.Insert(Symbol.Kinds.Meth, "len", Tab.intType);//devuelve int
        //Program1.form1.arbolTS.Nodes[0].Nodes.Add("ord | METH");                                                    //char=tipo q devuelve el met (Struct)
        Tab.OpenScope(lenSym);
        //El arg q toma es un Symbol        con tipo Arr,   el tipo del Elem del Arr es noType            
        Struct tipoArr = new Struct(Struct.Kinds.Arr, Tab.intType);
        //debiera funcionar para arreglos de cualquier tipo (no solo de enteros)
        //segun pag 41 de Tabla de simb.ppt, debiera se Tab.noType (en vez de intType)
        //pero el Tab.noType da error,
        Tab.Insert(Symbol.Kinds.Arg, "arr", tipoArr);
        //Program1.form1.arbolTS.Nodes[0].Nodes[5].Nodes.Add("arr | ARG");
        lenSym.nArgs = topScope.nArgs; //Fran
        lenSym.nLocs = topScope.nLocs; //Fran
        lenSym.locals = Tab.topScope.locals;
        Tab.CloseScope();//Fran cierra el Scope para el arg "arr"
        Tab.mostrarTab();
    }
	public static void OpenScope (Symbol sym) {
        
        Scope s = new Scope();
        s.nArgs = 0;
        s.nLocs = 0;
        s.outer=topScope;
        topScope=s;

        //Codigo Tabla de Simbolos
        ultimosNodos.Push(ultimoNodo);
        ultimosParametros.Push(new List<TreeNode>());
        if (sym != null)
        {
            profundidad++;
            Program1.form1.arbolTS.Nodes.Add("Scope de :Type: " + sym.kind + " | Name: " + sym.name);
        }
        if (muestraTabSimb) Program1.form1.instContinuar.ShowDialog();    //MessageBox.Show("ContinuarTSimb","T de simbolo");
        //Codigo Tabla de Simbolos
	}
	
	public static void CloseScope () { 
        topScope = topScope.outer;
        
        //Codigo Tabla de Simbolos  
        TreeNode ultimoNodo1 = ultimosNodos.Pop();
        int c = Program1.form1.arbolTS.Nodes[Program1.form1.arbolTS.Nodes.Count - 1].Nodes.Count;
        Program1.form1.arbolTS.Nodes.RemoveAt(profundidad--);
        ultimoNodo1.Nodes.AddRange(ultimosParametros.Pop().ToArray());
        Program1.form1.arbolTS.ExpandAll();
        if (muestraTabSimb) Program1.form1.instContinuar.ShowDialog();  //MessageBox.Show("ContinuarTSimb", "T de simbolo");
        //Codigo Tabla de Simbolos

	}
	
    

	public static Symbol Insert (Symbol.Kinds kind, string name, Struct type) {
		Symbol s; // = null; /// agregado hasta ver qué pasa
        s = new Symbol(kind, name, type);
        s.next = null;

        switch (kind)
        {   //fran
			case Symbol.Kinds.Arg: s.adr = topScope.nArgs++;break;
			case Symbol.Kinds.Local:s.adr = topScope.nLocs++;break;
		};

        Symbol actual = topScope.locals, ultimo=null;
        while (actual != null)  //Fran sólo es necesario buscar en el Scope actual
        {
            if (actual.name == name)
                Parser.Errors.Error(name + " está declarado más de una vez ");
            ultimo = actual;
            actual = actual.next;
        }
        if (ultimo == null) topScope.locals = s;
        else ultimo.next = s;

        //Codigo Tabla de Simbolos
        string str;
        switch(s.kind)
        {
            case Symbol.Kinds.Meth: str = " | Locals:"; break;
            case Symbol.Kinds.Type: str =(s.name != "int" && s.name != "char")? " | Fields:":""; break;
            case Symbol.Kinds.Prog: str = " | Fields:"; break;
            default:                str = "";break;
        }
        Program1.form1.arbolTS.Nodes[profundidad].Nodes.Add("Type: "+s.kind+ " | Name: " +s.name + str);
        ultimosParametros.Peek().Add(ultimoNodo = Program1.form1.arbolTS.Nodes[profundidad].Nodes[Program1.form1.arbolTS.Nodes[profundidad].Nodes.Count - 1]);
        Program1.form1.arbolTS.ExpandAll();
        if (muestraTabSimb) Program1.form1.instContinuar.ShowDialog();   //MessageBox.Show("ContinuarTSimb", "T de simbolo");
        //Codigo Tabla de Simbolos
        
        
        return s;
	}
	
	/* Retrieves the Symbol with name from the innermost scope. */
	public static Symbol Find (string name) {  //Fran
        if (ZZ.SymTab) Console.WriteLine("Busca en la Tab " + name); //Console.ReadKey();
        Symbol retorno=noSym;
        for (Scope s = topScope; s != null ; s = s.outer)
            for (Symbol sym = s.locals; sym != null ; sym = sym.next)
                if (sym.name == name)
                {
                    //retorno = sym;
                    //return retorno;
                    if (ZZ.SymTab)
                    {
                        Console.WriteLine("Encontró " + name + " en la Tab de Símb");
                        //Console.ReadKey();
                    };
                    return sym; 
                }
            Parser.Errors.Error( name + " no está declarado");
            return noSym;
    }

        public static Symbol FindSymbol(string name, Symbol puntSymbol)
    {  //Fran
        for (Symbol sym = puntSymbol; sym != null; sym = sym.next)
                if (sym.name == name)
                {
                    if (ZZ.SymTab)
                    {
                        Console.WriteLine("Encontró " + name + " con FindSymbol");
                    };
                    return sym;
                }
        Parser.Errors.Error("ERROR: " + name + " no está delcarado");
        return noSym;
    }

      
     /*	Retrieves the field name from the fields of type. */
    public static Symbol FindField(string name, Struct type)
    {
		/*---------------------------------*/
		/*----- insert your code here -----*/
		/*---------------------------------*/
        Symbol s = null;/// agregado hasta ver qué pasa
        return s;
	}
}

public class Scope {
	public Scope outer;    // reference to enclosing scope
	public Symbol locals;  // symbol table of this scope
	public int nArgs;      // # of arguments in this scope (for address asignment)
	public int nLocs;      // # of local variables in this scope (for address asignment)
}

/* Z# Symbol Table Nodes:
 * Every named object in a program is stored in an Symbol node.
 * Every scope has a list of Symbols declared within it. */
public class Symbol {
	public enum Kinds { Const, Global, Field, Arg, Local, Type, Meth, Prog }
	
	public Kinds kind;
	public string name;
	public Struct type;
	public Symbol next;                // next Symbol node in this scope
	public int val;                    // Const: value;
	public int adr;                    // Field, Arg, Local: order of declaration in scope
	public int nArgs;                  // Meth: # of arguments
	public int nLocs;                  // Meth: # of local variables
	public Symbol locals;              // Meth: arguments, then local variables; Prog: symbol table of program
	/* these fields are necessary for the building the assembly with System.Reflection.Emit */
	internal MethodBuilder meth;       // Method: builder for metadata and CIL
	internal FieldBuilder fld;         // Field: builder for metadata
	internal ConstructorBuilder ctor;  // Type: builder for metadata and CIL
				
	public Symbol (Kinds kind, string name, Struct type) {
		this.kind = kind; this.name = name; this.type = type;

	}
	
    public void mostrarSymbol(String blancos)
    {
        //Console.WriteLine(blancos + "SIMBOLO kind:" + this.kind + "  nombre:" + this.name);
        Tab.tabSimbString = Tab.tabSimbString + blancos + "SIMBOLO kind:" 
                            + this.kind + "  nombre:" + this.name + "\n";

        if (this.type != null) this.type.mostrarStruct(blancos + "  ");
        //else Console.WriteLine("type es null");
        else Tab.tabSimbString = Tab.tabSimbString + "type es null" + "\n";

        switch (this.kind)
        {
            case Symbol.Kinds.Const:
                //Console.WriteLine(blancos + "Value=" + this.val); break;
                Tab.tabSimbString = Tab.tabSimbString + blancos + "Value=" + this.val + "\n"; break;
            case Symbol.Kinds.Meth: case Symbol.Kinds.Prog:
                //muestra la lista de args y de vbles locales
                int cantArgs, cantLocs;
                cantArgs = cantLocs = 0;
                for (Symbol cur = this.locals; cur != null; cur = cur.next)
                {
                    cur.mostrarSymbol(blancos + "  ");
                    if (cur.kind == Symbol.Kinds.Arg) cantArgs++;
                    if (cur.kind == Symbol.Kinds.Local) cantLocs++; ;
                };
                if (this.nArgs != cantArgs || this.nLocs != cantLocs)
                    //Console.WriteLine("Error 4324242"); break;
                    Tab.tabSimbString = Tab.tabSimbString + "Error 4324242" + "\n"; break;
        }
    }

	public override bool Equals (object o) {
		if (this == o) return true;  // same object
		Symbol sym = o as Symbol;
		if (sym == null) return false;
		return Equals(sym);
	}
	
	public bool Equals (Symbol sym) {
		if (kind != sym.kind || name != sym.name || !type.Equals(sym.type)) 
			return false;
		switch (kind) {
			case Kinds.Const: return val == sym.val;
			case Kinds.Arg: case Kinds.Local: return adr == sym.adr;
			case Kinds.Meth:
				return nArgs == sym.nArgs && nLocs == sym.nLocs && 
				       EqualsCompleteList(locals, sym.locals);
		}
		return true;
	}
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

	public override string ToString () {
		StringBuilder sb = new StringBuilder();
		
		sb.AppendFormat("{0}: {1}, {2}", name, kind, type);
		switch (kind) {
			case Kinds.Const: sb.AppendFormat(" ={0}", val); break;
			case Kinds.Arg: case Kinds.Local: sb.AppendFormat(", {0}", adr); break;
			case Kinds.Meth:
				sb.AppendFormat(", {0}, {1}", nArgs, nLocs);
				Symbol sym; int i;
				for (sym = locals, i = 0; sym != null; sym = sym.next, i++)
					sb.AppendFormat("{0}-----{3}[{1}]={2}", Environment.NewLine, i, sym,
							(sym.kind == Symbol.Kinds.Arg) ? "arg" : "loc");
				if (nArgs + nLocs > 0) sb.AppendFormat("{0}", Environment.NewLine);
				break;
		}

		return sb.ToString();
	}
	
	/* Compare complete Symbol node lists. */
	public static bool EqualsCompleteList (Symbol sym1, Symbol sym2) {
		if (sym1 == sym2) return true;  // same object
		
		while (sym1 != null && sym1.Equals(sym2)) {
			sym1 = sym1.next; sym2 = sym2.next;
		}
		
		if (sym1 != null || sym2 != null) return false;
		
		return true;
	}
}

/* Z# Type Structures:
 * A type structure stores the type attributes of a declared type. */
public class Struct {
	public enum Kinds { None, Int, Char,String, Arr, Class }
	
	public Kinds kind;               // None, Int, Char, Arr, Class
	public Struct elemType;          // Arr: type of array elements
	public Symbol fields;            // Class: reference to list of local variables
	/* this field is necessary for the building the assembly with System.Reflection.Emit */ 
	internal Type sysType;           // CLR runtime type object
    
    
    public Struct (Kinds kind) : this(kind, null) {}
	public Struct (Kinds kind, Struct elemType) {
		this.kind = kind;
		// set CLR type of Struct instance
		switch (kind) {
            case Struct.Kinds.String: sysType=typeof(string);break;
			case Struct.Kinds.None: sysType = typeof(void); break;
			case Struct.Kinds.Int: sysType = typeof(int); break;
			case Struct.Kinds.Char: sysType = typeof(char); break;
			case Struct.Kinds.Arr:              //int            //long (del array)
                sysType = Array.CreateInstance(elemType.sysType, 0).GetType();//Metadata para int[]
                this.elemType = elemType; //(struct para int[]).elemType = (struct para el int)
				break;
			case Struct.Kinds.Class: 
				// do nothing here, type must first be defined
				// sysType is set in Code.CreateMetadata before first use
				break;
		}

        this.elemType = elemType;  //si   int[], hace la asig 2 veces
	}

    public void mostrarStruct(String blancos)
    {
        //Console.WriteLine(blancos + "STRUCT  kind:"+ this.kind); 
        Tab.tabSimbString = Tab.tabSimbString + blancos + "STRUCT  kind:" + this.kind + "\n";
        switch (this.kind)
        {
                case Struct.Kinds.Class:
                  //recurro los fields
                  for (Symbol campo = this.fields; campo != null; campo = campo.next )
                      campo.mostrarSymbol(blancos + "  "); break;
                case Struct.Kinds.Arr:
                  //muestro elem
                  if (this.elemType != null) this.elemType.mostrarStruct(blancos + "  "); break;

            }
    }
	
	public override bool Equals (object o) {
		if (this == o) return true;  // same object
		Struct s = o as Struct;
		if (s == null) return false;
		return Equals(s);
	}
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
	public bool Equals (Struct other) {
		if (kind == Kinds.Arr)
			return other.kind == Kinds.Arr && elemType.Equals(other.elemType);
		
		return this == other;  // must be same type node
	}

	public bool IsRefType () { return kind == Kinds.Class || kind == Kinds.Arr; }
	
	public bool CompatibleWith (Struct other) {
		return this.Equals(other) ||
		       this  == Tab.nullType && other.IsRefType() ||
		       other == Tab.nullType &&  this.IsRefType();
	}

	public bool AssignableTo (Struct dest) {
		return this.Equals(dest) || 
		       this == Tab.nullType && dest.IsRefType() ||
		       // for predefined function len(Array of noType)
		       kind == Kinds.Arr && dest.kind == Kinds.Arr && dest.elemType == Tab.noType;
	}	

	public override string ToString () {
		StringBuilder sb = new StringBuilder();
		
		switch (kind) {
			case Struct.Kinds.Int: sb.Append("int"); break;
			case Struct.Kinds.Char: sb.Append("char"); break;
			case Struct.Kinds.Class: 
				sb.AppendFormat("#{0}", GetHashCode());
				Symbol field; int i;
				for (field = fields, i = 0; field != null; field = field.next, i++)
					sb.AppendFormat("{0}-----field[{1}]={2}", Environment.NewLine, i, field);
				sb.AppendFormat("{0}", Environment.NewLine);
				break;
			case Struct.Kinds.Arr: sb.AppendFormat("{0}[]", elemType); break;
		}
		
		return sb.ToString();
	}
}

}
