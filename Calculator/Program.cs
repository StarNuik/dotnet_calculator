namespace Calculator;

public class Program
{
	static void Main(string[] args)
	{
		var together = string.Join(" ", args);
		var res = Do(together);
		Console.WriteLine(res.ToString());
	}

	public static float Do(string @in)
	{
		// var kernel = new Kernel();
		//
		throw new NotImplementedException();
	}
}
