namespace Calculator.Tests;

public class TestE2E
{
	[Theory]
	[InlineData("1", "1")]
	[InlineData("0", "0")]
	public void Constants(string expr, string want) => Roundtrip(expr, want);
	
	[Theory]
	[InlineData("1 + 1", "2")]
	[InlineData("0 + 1", "1")]
	[InlineData("5 + 7", "12")]
	public void Addition(string expr, string want) => Roundtrip(expr, want);

	[Theory]
	[InlineData("2 - 1", "1")]
	[InlineData("1 - 1", "0")]
	[InlineData("1 - 2", "-1")]
	public void Subtraction(string expr, string want) => Roundtrip(expr, want);

	// [Theory]
	// [InlineData("-1", "-1")]
	// [InlineData("-1 + 1", "0")]
	// [InlineData("1 + -1", "0")]
	// [InlineData("-1 + -1", "-2")]
	// public void Inversion(string expr, string want) => Roundtrip(expr, want);

	[Theory]
	[InlineData("1 * 1", "1")]
	[InlineData("2 * 2", "4")]
	// [InlineData("-2 * 2", "-4")]
	// [InlineData("-2 * -2", "4")]
	public void Multiplication(string expr, string want) => Roundtrip(expr, want);

	[Theory]
	[InlineData("1 / 1", "1")]
	[InlineData("1 / 2", "0.5")]
	[InlineData("2 / 1", "2")]
	[InlineData("2 / 2", "1")]
	[InlineData("4 / 2", "2")]
	[InlineData("5 / 2", "2.5")]
	[InlineData("6 / 2", "3")]
	public void Division(string expr, string want) => Roundtrip(expr, want);

	[Theory]
	[InlineData("3 + 3 + 3", "9")]
	[InlineData("3 * 3 + 3", "12")]
	[InlineData("3 + 3 * 3", "12")]
	public void Priority(string expr, string want) => Roundtrip(expr, want);

	[Theory]
	[InlineData("(3 + 3) * 3", "18")]
	[InlineData("3 * (3 + 3)", "18")]
	public void Brackets(string expr, string want) => Roundtrip(expr, want);

	private void Roundtrip(string expr, string want)
	{
		var res = Program.DoString(Operators.Default.Array, expr);
		Assert.Equal(want, res);
	}

	[Theory]
	[InlineData("1 + 2 +")]
	[InlineData("%&$()")]
	[InlineData("1 - ")]
	[InlineData("((((()))))")]
	[InlineData("(1 + ())")]
	[InlineData("Hello, world!")]
	public void Errors(string expr)
	{
		var res = Program.DoString(Operators.Default.Array, expr);
		Assert.StartsWith("Error:", res);
	}
}