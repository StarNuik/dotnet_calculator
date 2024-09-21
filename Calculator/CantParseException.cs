namespace Calculator;

public class CantParseException : Exception
{
	public CantParseException(string message)
		: base(message)
	{}
}