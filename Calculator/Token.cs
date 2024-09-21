namespace Calculator;

public enum TokenType
{
	Number,
	
	Add,
	Sub,
	Mul,
	Div,

	Open,
	Close,

	Eof,
}

public struct Token
{
	public TokenType Key;
	public string Value;
}
