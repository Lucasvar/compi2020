using System;
using System.IO;
using System.Text;
using System.Collections;

namespace at.jku.ssw.cc {
/// El scanner para Z# Compiler.
    public class ZZ
    {   public static bool Program = false;
        public static bool ScannerTest = false; 
        public static bool parser = false;
        public static bool SymTab = false;
        public static bool ParserTest = false;
        public static bool ParserStatem = false;
        public static bool readKey = false;
        public static bool Principal = false;

    }

public class Scanner {  
	const char EOF = '\u0080';  // retorna este valor al final del archivo
	const char CR = '\r';       // constante Carriage Return
	const char LF = '\n';       // constante Line Feed

	static TextReader input;
	static TextWriter output;
	
	public static char ch;            // caracter lookahead (= siguiente (no procesado) caracter de la entrada)
	static public int line, col;      // número de linea y columna del ch de la entrada
    
    public static Hashtable hashTableKeywords;
	/* Inicialización del scanner */
	public static void Init (TextReader r) {  /*NO se usa mas*/
		Init(r, Console.Out);
     //   GeneraHashKeywords();
    }

	public static void Init (TextReader r, TextWriter w) {
        //Glob g1 = new Glob();
        input = r;  //deja en input el programa a compilar
		output = w;
		line = 1; col = 0;
		NextCh();  // Lee el primer caracter en ch e incrementa columna en 1
        GeneraHashKeywords();
    }

	/* Lee el siguiente caracter de la entrada en ch.
	 * Guarda la posición, línea y columna en sincronización con la posición leida. */
    public static void NextCh()
    {
        try
        {
            ch = (char)input.Read(); col++;
            switch (ch)
            {
                case LF: line++; col = 0; break;
                case CR: col = 0; break;
                case '\uffff':  // read returns -1 at end of file
                    ch = EOF; break;
            }
        }
        catch (IOException) { ch = EOF; }        
    }

	/* obtiene el siguiente token
	 * a ser usado por el parser. */

	public static Token Next () {  //zzz
        // intenta formar un token de la entrada
        //linea=1, col=13 y apunta al blanco 
        while ((ch == ' ') || (ch == LF) || (ch == CR)) // bloque que saltea los blancos 
        {
            NextCh();
        };
        if (ch == EOF) return new Token(Token.EOF, line, col);
        
        Token t = new Token(line, col);
        //if ('A' <= ch && 'z' >= ch) System.Console.WriteLine(ch + " es letra ");
        //if ('0' <= ch && '9' >= ch) System.Console.WriteLine(ch + " es nro ");
        if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch == '_')) //es Letra
         ReadName(t);
        else 
         if ('0' <= ch && '9' >= ch)
           ReadNumber(t);
          else
           switch (ch){
            // -----------------------
            //| tokens simples        |
            // -----------------------

            case ';':
                   t.kind = Token.SEMICOLON; t.str = ch.ToString(); NextCh();
                break;
            case ',': 
                t.str = ","; t.str = ch.ToString();
                t.kind = Token.COMMA; NextCh();
                break;
            case '.':
                t.kind = Token.PERIOD; t.str = ch.ToString(); NextCh();
                break;
            case '*':
                t.kind = Token.TIMES; t.str = ch.ToString(); NextCh();
                break;
            case '%':
                t.kind = Token.REM; t.str = ch.ToString(); NextCh();
                break;
            case '(':
                t.kind = Token.LPAR; t.str = ch.ToString(); NextCh();
                break;
            case ')':
                t.kind = Token.RPAR; t.str = ch.ToString(); NextCh();
                break;
            case '[':
                t.kind = Token.LBRACK; t.str = ch.ToString(); NextCh();
                break;
            case ']':
                t.kind = Token.RBRACK; t.str = ch.ToString(); NextCh();
                break;
            case '{':
                t.kind = Token.LBRACE; t.str = ch.ToString(); NextCh();
                break;
            case '}':
                t.kind = Token.RBRACE; t.str = ch.ToString(); NextCh();
                break;
            case EOF: t.kind = Token.EOF;
                break;
            // -----------------------
            //| tokens compuestos     |
            // -----------------------
            case '=':NextCh();
                if (ch == '=')
                {
                    NextCh();
                    t.kind = Token.EQ; t.str = "==";
                }
                else
                { t.kind = Token.ASSIGN; t.str = "="; }
                break;
            case '!': NextCh();
                if (ch == '=')
                {
                    NextCh();
                    t.kind = Token.NE; t.str = "!=";
                }
                else
                {
                    t.kind = Token.NONE;  //debe reportar error
                    System.Console.WriteLine("Error:"+ch + " por el default despues del NextCh");
                };
                break;
            case '+': NextCh();
                if (ch == '+'){
                    NextCh();
                    t.kind = Token.PPLUS; //detecta ++
                    t.str = "++";
                }
                else
                    t.kind = Token.PLUS; t.str = ch.ToString();
                break;
            case '-': NextCh();
                if (ch == '-')
                {
                    NextCh();
                    t.kind = Token.MMINUS; //detecta --
                    t.str = "--";
                }
                else
                { t.kind = Token.MINUS; t.str = ch.ToString(); }
                break;
            case '&': NextCh();
                if (ch == '&')
                {
                    NextCh();
                    t.kind = Token.AND; t.str = "&&";
                }
                else
                    t.kind = Token.NONE;
                break;
            case '|': NextCh();
                if (ch == '|')
                {
                    NextCh();
                    t.kind = Token.OR; t.str = "||";
                }
                else
                    t.kind = Token.NONE;
                break;
            case '>': NextCh();
                if (ch == '=')
                {
                    NextCh();
                    t.kind = Token.GE; t.str = ">=";
                }
                else
                {
                    t.kind = Token.GT; t.str = ">";
                }
                break;
            case '<': NextCh();
                if (ch == '=')
                {
                    NextCh();
                    t.kind = Token.LE; t.str = "<=";

                }
                else
                { NextCh(); t.kind = Token.LT; t.str = "<"; }

                break;
            case '\"':
                NextCh(); t.kind = Token.COMILLADOBLE; t.str = "comilla doble";
                break;
            //  --------------------
            // | CONSTANTE CARACTER
            //  --------------------
            case '\'': NextCh();  // ' (comilla)
                if (ch == '\\')  //=>  '\  (se prepara para '\n','\r','\\') 
                {
                    t.val=0;
                    NextCh();
                    switch (ch)
                    {
                        case 'n': t.val = '\n'; // idem a t.val = 10;  //
                            NextCh();
                            break;
                        case 'r': t.val = '\r';
                            NextCh();
                            break;
                        case '\'': t.val = '\\';
                            break;
                        default: t.val = 0; // caracter escape no válido
                            break;          // solo 3 cosas pueden venir despues de '\
                    }
                }
                else   //(comilla sola) (y char sig en ch)
                {
                    Console.WriteLine("HA leido una comilla y el ch sig. es "+ch);  //Console.ReadKey();
                    int inicio = 32;
                    int fin = 126;  //por ej 'c             //seria ''
                    if (((ch >= inicio) && (ch <= fin)) || (ch == '\''))
                    { t.val = ch;//si ch es una c => t.val = 99 
                      t.str = ch.ToString(); 
                        System.Console.WriteLine("caracter:"+ch);
                    }
                    else
                        t.val = 0; // caracter no válido
                    NextCh();  //pasa al 3º char del token.str => ch debiera ser ' (o sea 'c')
                }
                if (ch != '\'') //si el 3º char no es comilla => error
                    t.val = 0; // falta el caracter de fin de constante char
                else
                    {NextCh();  //deja ch en el proximo char (por ej en blanco)
                     //System.Console.WriteLine("reconoció el CHARCONST");
                    }
                t.kind = Token.CHARCONST;  //era por ej un 'a'
                break;

            // -----------------------
            //| comentarios           |
            // -----------------------   
            case '/': NextCh();
                if (ch == '*')
                {
                    Form1.iniCommLine = line;
                    Form1.iniCommCol = col;

                    Pila aux = new Pila(50);
                    aux.push("comentario");
                    //Boolean bandera = false;
                    //while ((ch != EOF) && (!bandera))
                    while ((ch != EOF) && (!aux.estaVacia()))
                    {
                        NextCh();
                        if (ch == '*')
                        {
                            NextCh();
                            if ((ch == '/') && (!aux.estaVacia()))
                            {
                                Form1.finCommLin = line;
                                Form1.finCommCol = col;

                                aux.pop();
                                if (aux.estaVacia())
                                {
                                    //bandera = true;
                                    NextCh();
                                    t = Next();
                                }
                            }
                        }
                        else
                        {
                            if (ch == '/')
                            {
                                NextCh();
                                if (ch == '*')
                                    aux.push("comentario");
                            }
                        }
                    }
                }
                else
                {
                    t.kind = Token.SLASH;
                }
                break;
            
               default:
                {
                    t.str = ch.ToString();
                    //t.line = line; t.col = col;
                    throw new ErrorMio(line, col, 1, "caracter " + ch.ToString() + " invalido");
                }
        }
        return t;
	}
	
	/* Error handling for the scanner.
	 * Prints error message to output. */
	static void Error (string msg) {
		// leave this here for scanner testing
		if (output != null) output.WriteLine("-- line {0}, col {1}: {2}", line, col, msg);
//		else Parser.Errors.Error(msg);
	}
    public static void GeneraHashKeywords(){
       string[] keywords ={"break", "class", "const", "else",
                           "if", "new", "read","return",
                           "void","while","write","writeln"
                          };
       hashTableKeywords = new Hashtable();
        int i=0;
        for(i=0;i<keywords.Length;i++)
            hashTableKeywords.Add(keywords[i].GetHashCode(),keywords[i]);
    }
    //public static int esPalabraClave(string cadena)
    public static bool esPalabraClave(string cadena)
    {
        //int retorno = 0;
        //if (hashTableKeywords.ContainsValue(cadena))
        //    retorno=0;
        //else
        //    retorno=-1;
        //return retorno;
        return hashTableKeywords.ContainsValue(cadena);
    }
    static void ReadName(Token t)
    {
        while ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || ch == '_')
        {
            t.str = t.str+ch;
            NextCh();
        }
        if (esPalabraClave(t.str))
            switch (t.str)
            {
                case "break":
                    t.kind = Token.BREAK;
                    break;
                case "class":
                    t.kind = Token.CLASS;
                    break;
                case "const":
                    t.kind = Token.CONST;
                    break;
                case "else":
                    t.kind = Token.ELSE;
                    break;
                case "if":
                    t.kind = Token.IF;
                    break;
                case "new":
                    t.kind = Token.NEW;
                    break;
                case "read":
                    t.kind = Token.READ;
                    break;
                case "return":
                    t.kind = Token.RETURN;
                    break;
                case "void":
                    t.kind = Token.VOID;
                    break;
                case "while":
                    t.kind = Token.WHILE;
                    break;
                case "write":
                    t.kind = Token.WRITE;
                    break;
                case "writeln":
                    t.kind = Token.WRITELN;
                    break;

            }
        else
        {
            t.kind = Token.IDENT;
        }
        
    }
    static void ReadNumber(Token t)
    {
        while (ch>='0' && ch<='9' && ch!=EOF){
            t.str = t.str + ch;
            NextCh();
        }

        if (t.str != null)
            try
            {
                t.val = Int16.Parse(t.str);
                t.kind = Token.NUMBER;
            }
            catch (Exception)
            {
                Error(ErrorStrings.BIG_NUM);
                
                //Console.WriteLine(e.ToString());
                t.kind = Token.NONE;
            }
    }
}
}
