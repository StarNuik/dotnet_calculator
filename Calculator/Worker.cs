namespace Calculator;

public static class Worker
{
	public static float Calculate(Token[] tokens)
	{
		var stack = new Stack<float>();
		foreach (var token in tokens)
		{
			if (token.IsNumber)
			{
				stack.Push(token.Number);
				continue;
			}

			// else
			var op = token.Operator;
			(var lhs, var rhs) = TakeFrom(stack);
			var res = op.Execute(lhs, rhs);
			stack.Push(res);
		}
		if (stack.Count != 1)
		{
			throw new NotImplementedException();
		}
		return stack.Single();
	}

	private static (float, float) TakeFrom(Stack<float> stack)
	{
		var ok = true;
		ok &= stack.TryPop(out var rhs);
		ok &= stack.TryPop(out var lhs);

		if (!ok)
		{
			throw new NotImplementedException();
		}
		return (lhs, rhs);
	}
}