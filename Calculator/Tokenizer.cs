namespace Calculator;

public static class Tokenizer
{
    private class OpenBracket : IOperator
    {
        public string RepresentedBy => "(";
        public int Precedence => throw new NotImplementedException();
    }

    private class ClosedBracket : IOperator
    {
        public string RepresentedBy => ")";
        public int Precedence => throw new NotImplementedException();
    }

	// TODO: clean up this majestic blanket of code
    public static Token[] ToRpn(IOperator[] operators, string @in)
	{
		// var ops = operators.ToDictionary(
		// 	op => op.RepresentedBy,
		// 	op => op
		// );
		// AddBrackets(ops);

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
			// (@in, token) = SplitOperator(ops, @in);
			// var op = token.Operator;
			// if (op is OpenBracket)
			// {
			// 	opStack.Push(token);
			// 	continue;
			// }
			// if (op is ClosedBracket)
			// {
			// 	while (opStack.Peek().Operator is not OpenBracket)
			// 	{
			// 		@out.Add(opStack.Pop());
			// 	}
			// 	opStack.Pop();
			// 	continue;
			// }

			// while (opStack.Peek().Operator.Precedence >= op.Precedence)
			// {
			// 	@out.Add(opStack.Pop());
			// }
			// opStack.Push(token);
		}

		// while (opStack.Count > 0)
		// {
		// 	@out.Add(opStack.Pop());
		// }
		return @out.ToArray();
	}

	private static void AddBrackets(Dictionary<string, IOperator> ops)
	{
		var add = (IOperator op) => ops[op.RepresentedBy] = op;
		add(new OpenBracket());
		add(new ClosedBracket());
	}

	private static (string, Token) SplitNumber(string @in)
	{
		var len = CountFirst(@in, IsNumberChar);

		var token = new Token(float.Parse(@in[..len]));
		return (@in[len..], token);
	}

	// private static (string, Token) SplitOperator(Dictionary<string, IOperator> ops, string @in)
	// {
	// 	// Did not use `.Single(predicate)`
	// 	// so that I can throw a custom exception 
	// 	var keys = ops.Keys.Where(@in.StartsWith);
	// 	if (keys.Count() != 1)
	// 	{
	// 		throw new NotImplementedException();
	// 	}
	// 	var key = keys.Single();

	// 	var token = new Token(ops[key]);
	// 	return (@in[key.Length..], token);
	// }

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