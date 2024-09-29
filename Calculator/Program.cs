namespace Calculator;

public class Program
{
	private static void Main(string[] args)
	{
		var together = string.Join(" ", args);
		
		var res = DoString(Operators.Default.Array, together);
		
		Console.WriteLine(res);
	}

	public static string DoString(IOperator[] operators, string @in)
	{
		try
		{
			var @out = Do(operators, @in);
			return @out.ToString();
		}
		catch (CalculatorException e)
		{
			return $"Error: {e.Message}";
		}
	}

	public static float Do(IOperator[] operators, string @in)
	{
		var tokens = Tokenizer.ToRpn(operators, @in);
		var @out = Worker.Calculate(tokens);
		return @out;
	}
}
