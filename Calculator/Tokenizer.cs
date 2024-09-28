namespace Calculator;

public class TokenizerException : Exception
{
	public TokenizerException(string message) : base(message)
	{}
}

public static class Tokenizer
{
	private class Open : IOperator
	{
		public string RepresentedBy => "(";
		public int Precedence => throw new NotImplementedException();
		public Associativity Associativity => throw new NotImplementedException();
	}

	private class Close : IOperator
	{
		public string RepresentedBy => ")";
		public int Precedence => throw new NotImplementedException();
		public Associativity Associativity => throw new NotImplementedException();
	}

	// TODO: clean up this majestic blanket of code
	public static Token[] ToRpn(IOperator[] operators, string @in)
	{
		var ops = operators.ToDictionary(
			op => op.RepresentedBy,
			op => op
		);
		AddParentheses(ops);

		var @out = new List<Token>();
		var opStack = new Stack<Token>();

		while (!string.IsNullOrWhiteSpace(@in))
		{
			@in = @in.TrimStart();
			Token token;
			var next = @in[0];

			if (IsNumberChar(next))
			{
				(@in, token) = SplitNumber(@in);
				@out.Add(token);
				continue;
			}

			// else
			(@in, token, var op) = SplitOperator(ops, @in);
			if (op is Open)
			{
				opStack.Push(token);
				continue;
			}
			if (op is Close)
			{
				MoveUntilOpen(opStack, @out);
				opStack.Pop();
				continue;
			}

			// else
			MoveLowerPrecedence(opStack, @out, op);
			opStack.Push(token);
		}

		MoveFinal(opStack, @out);

		return @out.ToArray();
	}

	private static void MoveLowerPrecedence(Stack<Token> from, List<Token> to, IOperator op)
	{
		var @out = new List<Token>();
		while (from.Count > 0
			&& from.Peek().Operator is not Open
			&& from.Peek().Operator.GreaterThan(op))
		{
			to.Add(from.Pop());
		}
	}

	private static void MoveUntilOpen(Stack<Token> from, List<Token> to)
	{
		while (from.Count > 0
			&& from.Peek().Operator is not Open)
		{
			to.Add(from.Pop());
		}
		if (from.Count == 0)
		{
			throw new TokenizerException("invalid parentheses");
		}
	}

	private static void MoveFinal(Stack<Token> opStack, List<Token> @out)
	{
		if (opStack.Any(tok => tok.Operator is Open))
		{
			throw new TokenizerException("invalid parentheses");
		}
		@out.AddRange(opStack);
		opStack.Clear();
	}

	private static void AddParentheses(Dictionary<string, IOperator> ops)
	{
		var add = (IOperator op) => ops[op.RepresentedBy] = op;
		add(new Open());
		add(new Close());
	}

	private static (string, Token) SplitNumber(string @in)
	{
		var len = CountFirst(@in, IsNumberChar);

		float val;
		try
		{
			val = float.Parse(@in[..len]);
		}
		catch (Exception e)
		{
			throw new TokenizerException($"couldn't parse number: {e.Message}");
		}

		var token = new Token(val);
		return (@in[len..], token);
	}

	private static (string, Token, IOperator) SplitOperator(Dictionary<string, IOperator> ops, string @in)
	{
		// Did not use `.Single(predicate)`
		// so that I can throw a custom exception 
		var keys = ops.Keys.Where(@in.StartsWith);
		if (keys.Count() != 1)
		{
			throw new TokenizerException("unknown operator");
		}
		var key = keys.Single();

		var op = ops[key];
		var token = new Token(op);
		return (@in[key.Length..], token, op);
	}

	private static bool IsNumberChar(char ch)
	{
		return char.IsAsciiDigit(ch) || ch == '.';
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