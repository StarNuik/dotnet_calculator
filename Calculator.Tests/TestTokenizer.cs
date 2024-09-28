namespace Calculator.Tests;

public class TestTokenizer
{
	[Theory]
	[InlineData("1", 1f)]
	[InlineData("5.3", 5.3f)]
	[InlineData(".1", 0.1f)]
	public void Numbers(string expr, float want)
	{
		var tokens = Tokenizer.ToRpn(null, expr);
		Assert.Single(tokens);
		Assert.True(tokens[0].IsNumber);
		Assert.Equal(want, tokens[0].Number);
	}
}