namespace Calculator.Tests;

public class TestParser
{
	[Theory]
	[InlineData(1f, "1")]
	[InlineData(1f, "1.0")]
	[InlineData(3.141592f, "3.141592")]
	public void Numbers(float want, string tokenValue)
	{
		var tokens = new Token[]{
			new Token{Key = TokenType.Number, Value = tokenValue},
		};
		var res = Parser.Do(tokens).Collect();
		Assert.Equal(want, res);
	}
}