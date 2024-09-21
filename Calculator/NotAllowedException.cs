namespace Calculator;

public class CantTokenizeException : Exception
{
	public CantTokenizeException(string message)
		: base(message)
	{}
}