namespace Calculator.Tests;

public class TestWorker
{
	[Theory]
	[InlineData("1 + 2", 3f)]
	[InlineData("1 - 2", -1f)]
	[InlineData("1 * 2", 2f)]
	[InlineData("1 / 2", .5f)]
	public void SingleOperator(string expr, float want)
	{
		var ops = Operators.Default.Array;
		var tokens = Tokenizer.ToRpn(ops, expr);
		var res = Worker.Calculate(tokens);
		Assert.Equal(want, res);
	}
}