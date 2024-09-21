﻿namespace Calculator;

public class Program
{
	static void Main(string[] args)
	{
		var together = string.Join(" ", args);
		var res = Do(together);
		Console.WriteLine(res);
	}

	public static string Do(string @in)
	{
		try
		{
			var tokens = Tokenizer.Do(@in);
			var expr = Parser.Do(tokens);
			var res = expr.Collect();
			return res.ToString();
		}
		catch (CantTokenizeException e)
		{
			return $"Error: symbol not supported: '{e.Message}'";
		}
		catch (CantParseException e)
		{
			return $"Error: incorrect input: {e.Message}";
		}
		catch
		{
			return "Error: unknown error";
		}
	}
}
