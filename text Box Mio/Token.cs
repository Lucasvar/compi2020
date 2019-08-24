using System.Text;

namespace at.jku.ssw.cc {
/// A <code>Token</code> represents a terminal symbol.
/// Tokens are provided by the scanner for the parser.
/// They hold additional information about the symbol.
/// AW, woess@dotnet.jku.at
public class Token {
	public const int
	//error token
		NONE      =  0,
	//terminal classes
		IDENT     =  1, NUMBER    =  2, CHARCONST =  3,
	    //+               -               *               /               %
		PLUS      =  4, MINUS     =  5, TIMES     =  6, SLASH     =  7, REM       =  8,
	//relational operators (must be consecutive numbers, see Parser.Relop)
	//==              >=              >               <=              <               !=
		EQ        =  9, GE        = 10, GT        = 11, LE        = 12, LT        = 13, NE        = 14,
	//&&              ||
		AND       = 15, OR        = 16,
	//=               ++              --
		ASSIGN    = 17, PPLUS     = 18, MMINUS    = 19,
	//;               ,               .
		SEMICOLON = 20, COMMA     = 21, PERIOD    = 22,
	//(               )               [               ]               {               }
		LPAR      = 23, RPAR      = 24, LBRACK    = 25, RBRACK    = 26, LBRACE    = 27, RBRACE    = 28,
	//KEYWORDS
		BREAK     = 29, CLASS     = 30, CONST     = 31, ELSE      = 32, IF        = 33, NEW       = 34, 
		READ      = 35, RETURN    = 36, VOID      = 37, WHILE     = 38, WRITE     = 39,
	//end of file
		EOF       = 40, WRITELN = 41, COMILLADOBLE = 42;


	/* List of printable names for all kinds of tokens (e.g. for error messages).
	 * The token codes must index correctly into this array! */
	public static readonly string[] names = { 
		"?",
		"identifier", "number", "character constant", 
		"+", "-", "*", "/", "%", 
		"==", ">=", ">", "<=", "<", "!=", 
		"&&", "||", 
		"=", "++", "--", 
		";", ",", ".", 
		"(", ")", "[", "]", "{", "}", 
		"break", "class", "const", "else", "if", "new",
		"read", "return", "void", "while", "write", 
		"end of file" , "comilla doble", "mmm..."
	};

	
	public int kind;    // token code (NONE, IDENT, ...)
	public int line;    // token line number (for error messages)
	public int col;     // token column number (for error messages)
	public int val;     // numerical value (for numbers and character constants)
	public string str;  // string representation of token (for numbers and identifiers)
	/* We keep the string representation of numbers for error messages in case the
	 * number literal in the source code is too big to fit into an int.
	 */

	
	public Token (          int line, int col)                      : this(NONE, line, col, 0, null) {}
	public Token (int kind, int line, int col)                      : this(kind, line, col, 0, null) {}
	public Token (int kind, int line, int col, int val)             : this(kind, line, col, val, null) {}
	public Token (int kind, int line, int col,          string str) : this(kind, line, col, 0, str) {}
	public Token (int kind, int line, int col, int val, string str) {
		this.kind = kind;
		this.line = line; this.col = col;
		this.val = val; this.str = str;
	}

    public override int GetHashCode()
    {
        
        return base.GetHashCode();
    }
 

	public override bool Equals (object o) {
		if (this == o) return true;  // same object
		Token t = o as Token;
		if (t == null) return false;  // not a token
		return Equals(t);
	}

	public bool Equals (Token t) {
		if (kind != t.kind || line != t.line || col != t.col) return false;
		switch (kind) {
			case IDENT: return str == t.str;
			case NUMBER: return val == t.val && str == t.str;
			case CHARCONST: return val == t.val; 
		}
		return true;
	}
	
	/* Returns a string representation of this Token object.
	 * Always includes all attributes of the token in the string.
	 * When string or val are irrelevant, they are put in parenthesis.
	 */
	public override string ToString () {
		StringBuilder sb = new StringBuilder();
		sb.AppendFormat("line {0}, col {1}: {2}", line, col, names[kind]);
		switch (kind) {
			case IDENT:     sb.AppendFormat(" = {0}", str); break;
			case NUMBER:    sb.AppendFormat(" = {0} (\"{1}\")", val, str); break;
			case CHARCONST: sb.AppendFormat(" = '{0}'", (char) val); break;
		}
		return sb.ToString();
	}	
}

}
