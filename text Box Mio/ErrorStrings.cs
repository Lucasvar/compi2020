namespace at.jku.ssw.cc {
/// This class provides strings constants for the error messages.
public class ErrorStrings {
	public static readonly string
		//----- messages for syntax errors
			//----- for Scanner
		BIG_NUM           = "número demasiado grande apra una constante entera",
		EMPTY_CHARCONST   = "Caracter constante vacío",
		INVALID_CHAR      = "Caracter no válido",
		INVALID_CHARCONST = "Constante Caracter no válida",
		MISS_SQUOTE       = "Falta caracter ' al final de la constante Caracter",
		UNDEF_ESC         = "Secuencia de caracter ESC no definida",
	
			//----- for Parser
		ADD_OP            = "se esperaba operador suma (" + Token.names[Token.PLUS] +  // cannot occur
			                  "," + Token.names[Token.MINUS] + ")",
		CONST_DECL        = Token.names[Token.NUMBER] + " o " + Token.names[Token.CHARCONST] + " se esperaba",
		DESIGN_FOLLOW     = Token.names[Token.ASSIGN] + ", " + Token.names[Token.LPAR] + ", " + 
		                    Token.names[Token.PPLUS] + " o " + Token.names[Token.MMINUS] + " se esperaba",
		INVALID_STAT      = "invalid start of statement: " + Token.names[Token.IDENT] + ", " + 
		                    Token.names[Token.IF]     + ", " + Token.names[Token.WHILE]    + ", " +
		                    Token.names[Token.BREAK] + ", " + Token.names[Token.RETURN] + ", " + 
		                    Token.names[Token.READ] + ", " + Token.names[Token.WRITE]  + ", " + 
		                    Token.names[Token.LBRACE] + " o " + Token.names[Token.SEMICOLON] + " se esperaba",
		INVALID_FACT      = "invalid start of factor: " + Token.names[Token.IDENT] + ", " +
		                    Token.names[Token.NUMBER] + ", " + Token.names[Token.CHARCONST] + ", " +
		                    Token.names[Token.NEW] + " o " + Token.names[Token.LPAR] + " se esperaba",
		METH_DECL         = Token.names[Token.IDENT] + " o " + Token.names[Token.VOID] + " se esperaba",
		MUL_OP            = "se esperaba operador multiplicación (" + Token.names[Token.TIMES] + 
		                    "," + Token.names[Token.SLASH] + "," + Token.names[Token.REM] + ")",
		REL_OP            = "se esperaba operador relacional (" + Token.names[Token.EQ] + "," + Token.names[Token.NE] + 
		                    "," + Token.names[Token.GT] + "," + Token.names[Token.GE] + "," + Token.names[Token.LT] + 
		                    "," + Token.names[Token.LE] + ")",
		
		//----- messages for semantic errors 
			//----- for Parser
		ARRAY_INDEX       = "array index must be an integer",
		ARRAY_SIZE        = "array size must be an integer",
		CONST_TYPE        = "value does not match constant type",
		EQ_CHECK          = "only (un)equality checks are allowed for reference types",
		INCOMP_TYPES      = "incompatible types",
		INVALID_CALL      = "procedure called as a function",
		INVALID_DECL      = "invalid global declaration",
		LESS_PARAMS       = "less actual than formal parameters",
		MORE_PARAMS       = "more actual than formal parameters",
		NO_ARRAY          = " is not an array",
		NO_CLASS          = " is not an object",
		NO_CLASS_TYPE     = "class type expected",
		NO_INT            = "variable must be an integer",
		NO_INT_OP         = "operand(s) must be of type int",
		NO_LOOP           = "break statement outside of loop",
		NO_MAIN           = "Main must be a method",
		NO_METH           = "not a method",
		NO_PARAMS         = "method Main must not have parameters",
		NO_RETURN         = "no return from function",
		NO_RETURN_EXPR    = "return value expected",
		NO_TYPE           = "type expected",
		PARAM_TYPE        = "parameter type mismatch",
		READ_VALUE        = "can only read int or char values",
		RETURN_TYPE       = "type of return value must be assignable to method type",
		RETURN_VOID       = "void method must not return a value",
		VOID_MAIN         = "method Main must be void",
		WRITE_VALUE       = "can only write int or char values",
		//----- for Tab
		NARGS             = "too many method arguments",
		DECL_NAME         = " declared twice",
		NLOCALS           = "too many local variables",  // case covered by SymTabTest.testInsertMaxLocals, w/o error report
		NO_FIELD          = "no such field",
		NOT_FOUND         = "name not found",
		NOT_IN_CLASS      = "no class defined for this field",
			//----- for Item
		CREATE_ITEM       = "cannot create code item for this kind of symbol table object",
			//----- for Code
		CODE_FULL         = "program too large",
		CODE_LOAD         = "compiler error in Code.load",
		INVALID_ELEM      = "invalid array element type",
		NO_VAR            = "left-hand side of assignment must be a variable",
		NO_VAR_INC        = "incremented object must be a variable",
		OBJ_FILE          = "cannot write object file",
		EXE_FILE          = "cannot write PE file",
			//----- for Label
		LAB_DEF           = "label already defined",
		DEST_UNDEF        = "destination undefined",

		PANIC_MODE        = "PANIC MODE: exit at first error";
}

}
