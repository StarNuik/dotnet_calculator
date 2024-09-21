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

public static class Tokenizer
{
	public static Token[] Do(string @in)
	{
		var tokens = new List<Token>();
		var next = @in.Replace(" ", "");
		while (!string.IsNullOrEmpty(next))
		{
			(var token, next) = NextToken(next);
			tokens.Add(token);
		}
		tokens.Add(new Token{Key = TokenType.Eof});
		return tokens.ToArray();
	}

	private static (Token token, string leftover) NextToken(string @in)
	{
		if (string.IsNullOrEmpty(@in)) {
			return (new Token{Key = TokenType.Eof}, "");
		}
		if (char.IsNumber(@in[0])) {
			return NextNumber(@in);
		}

		var tokenT = @in[0] switch {
			'+' => TokenType.Add,
			'-' => TokenType.Sub,
			'*' => TokenType.Mul,
			'/' => TokenType.Div,
			'(' => TokenType.Open,
			')' => TokenType.Close,
			_ => throw new NotImplementedException(),
		};
		
		return (new Token{Key = tokenT}, @in.Substring(1));
	}

	private static (Token token, string leftover) NextNumber(string @in)
	{
		var len = 0;
		while (len < @in.Length
			&& (char.IsNumber(@in[len]) || @in[len] == '.'))
		{
			len++;
		}
		var token = new Token{
			Key = TokenType.Number,
			Value = @in.Substring(0, len),
		};
		return (token, @in.Remove(0, len));
	}
}