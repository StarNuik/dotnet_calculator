namespace Calculator;

public class Program
{
	private static void Main(string[] args)
	{
		var together = string.Join(" ", args);
		
		float res;
		try
		{
			res = Do(together);
		}
		catch (CalculatorException e)
		{
			Console.WriteLine($"Error: {e.Message}");
			return;
		}
		
		Console.WriteLine(res.ToString());
	}

	public static float Do(string @in)
	{
		var ops = Operators.Default.Array;
		var tokens = Tokenizer.ToRpn(ops, @in);
		var @out = Worker.Calculate(tokens);
		return @out;
	}
}
