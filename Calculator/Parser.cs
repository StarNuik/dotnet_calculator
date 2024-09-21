namespace Calculator;

/*
expression ::= ( addsub | "(" addsub ")")
addsub ::= muldiv [( "+" | "-") muldiv]
muldiv ::= unary [( "*" | "/") unary]
unary ::= ["-"] (number | expression)
*/

public interface IExpression
{
	// public IExpression[] Children();
	public float Collect();
}

public static class Parser
{
	public static IExpression Do(Token[] @in)
	{
		if (@in.Length == 0)
		{
			throw new NotImplementedException();
		}
		if (@in[0].Key == TokenType.Number)
		{
			return Number(@in[0]);
		}
		throw new NotImplementedException();
	}

	public static IExpression Number(Token @in)
	{
		var val = float.Parse(@in.Value);
		return new Expressions.Number(val);
	}

	// public static IExpression Expression(Token[] @in)
	// {

	// }

	// public static IExpression Addsub(Token[] @in)
	// {}

	// public static IExpression Muldiv(Token[] @in)
	// {}

	// public static IExpression Unary(Token[] @in)
	// {

	// }
}