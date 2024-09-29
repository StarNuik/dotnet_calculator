namespace Calculator.Tests;

public class TestWorker
{
	[Theory]
	[InlineData("1 + 2", 3f)]
	[InlineData("1 - 2", -1f)]
	[InlineData("1 * 2", 2f)]
	[InlineData("1 / 2", .5f)]
	public void SingleOperator(string expr, float want)
		=> AssertExpr(expr, want);
	
	[Theory]
	[InlineData("0.5", .5f)]
	[InlineData("1 / 2", .5f)]
	[InlineData("1 + 2 + 3", 6f)]
	[InlineData("1 - 2 + 3", 2f)]
	[InlineData("1 + 2 - 3", 0f)]
	[InlineData("1 - 2 - 3", -4f)]
	[InlineData("1 + 2 * 3", 7f)]
	[InlineData("1 * 2 + 3", 5f)]
	[InlineData("(1 + 2) * 3", 9f)]
	[InlineData("1 * (2 + 3)", 5f)]
	[InlineData("1 * 2 - 3 * 4 / 5 + 6 - 7 * 8 * 9", -498.4f)]
	[InlineData("(1 * 2 - 3 * 4) / (5 + 6 - 7 * 8 * 9)", 0.0202839756592f)]
	[InlineData("1 * (2 - 3 * 4 / (5 + 6) - 7) * 8 * 9", -438.545454545f)]
	public void Advanced(string expr, float want)
		=> AssertExpr(expr, want);
	
	[Theory]
	[InlineData("1 ** 2")]
	[InlineData("1 2 3")]
	[InlineData("")]
	public void Exceptions(string expr)
	{
		var ops = Operators.Default.Array;
		var tokens = Tokenizer.ToRpn(ops, expr);
		Assert.Throws<CalculatorException>(() => Worker.Calculate(tokens));
	}

	private void AssertExpr(string expr, float want)
	{
		var ops = Operators.Default.Array;
		var tokens = Tokenizer.ToRpn(ops, expr);
		var res = Worker.Calculate(tokens);
		Assert.Equal(want, res);
	}
}