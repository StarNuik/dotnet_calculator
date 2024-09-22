namespace Calculator;

public static class Tokenizer
{
	private static HashSet<string> SupportedFunc = new()
	{
		"pow", "sqrt",
	};

	public static Token[] Do(string @in)
	{
		var tokens = new List<Token>();
		var next = @in;
		while (!string.IsNullOrWhiteSpace(next))
		{
			next = next.TrimStart();
			(var token, next) = NextToken(next);
			tokens.Add(token);
		}
		tokens.Add(new Token{Key = TokenType.Eof});
		return tokens.ToArray();
	}

	private static (Token token, string leftover) NextToken(string @in)
	{
		if (string.IsNullOrEmpty(@in))
		{
			return (new Token{Key = TokenType.Eof}, "");
		}
		if (char.IsAsciiDigit(@in[0]))
		{
			return NextNumber(@in);
		}
		if (char.IsAsciiLetterLower(@in[0]))
		{
			return NextFunc(@in);
		}

		var tokenT = @in[0] switch
		{
			'+' => TokenType.Add,
			'-' => TokenType.Sub,
			'*' => TokenType.Mul,
			'/' => TokenType.Div,
			'(' => TokenType.Open,
			')' => TokenType.Close,
			',' => TokenType.Comma,
			_ => throw new CantTokenizeException(
				$"symbol not supported: '{@in[0]}'"
			),
		};
		
		return (new Token{Key = tokenT}, @in[1..]);
	}

	private static (Token token, string leftover) NextNumber(string @in)
	{
		var len = CountFirst(@in,
			ch => char.IsAsciiDigit(ch) || ch == '.'
		);

		var token = new Token
		{
			Key = TokenType.Number,
			Value = @in[..len],
		};
		return (token, @in[len..]);
	}

	private static (Token token, string leftover) NextFunc(string @in)
	{
		var len = CountFirst(@in, char.IsAsciiLetterLower);
		var value = @in[..len];
		if (!SupportedFunc.Contains(value))
		{
			throw new CantTokenizeException(
				$"function not supported: '{value}'"
			);
		}
		var token = new Token
		{
			Key = TokenType.Func,
			Value = value,
		};
		return (token, @in[len..]);
	}

	private static int CountFirst(string @in, Func<char, bool> predicate)
	{
		var len = 0;
		while (len < @in.Length && predicate(@in[len]))
		{
			len++;
		}
		return len;
	}
}