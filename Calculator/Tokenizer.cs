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

// public struct Token
// {

// }

public static class Tokenizer
{
	public static TokenType[] Do(string @in)
	{
		var tokens = new List<TokenType>();
		var next = @in.Replace(" ", "");
		while (!string.IsNullOrEmpty(next))
		{
			(var token, next) = NextToken(next);
			tokens.Add(token);
		}
		return tokens.ToArray();
	}

	private static (TokenType token, string leftover) NextToken(string @in)
	{
		if (string.IsNullOrEmpty(@in)) {
			return (TokenType.Eof, "");
		}
		if (char.IsNumber(@in[0])) {
			return NextNumber(@in);
		}

		var token = @in[0] switch {
			'+' => TokenType.Add,
			'-' => TokenType.Sub,
			'*' => TokenType.Mul,
			'/' => TokenType.Div,
			'(' => TokenType.Open,
			')' => TokenType.Close,
			_ => throw new NotImplementedException(),
		};
		return (token, @in.Substring(1));
	}

	private static (TokenType token, string leftover) NextNumber(string @in)
	{
		var len = 0;
		while (len < @in.Length
			&& (char.IsNumber(@in[len]) || @in[len] == '.'))
		{
			len++;
		}
		return (TokenType.Number, @in.Remove(0, len));
	}
}