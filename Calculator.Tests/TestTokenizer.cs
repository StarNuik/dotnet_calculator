namespace Calculator.Tests;

public class TestTokenizer
{
	[Theory]
	[InlineData("1", new TokenType[]{TokenType.Number})]
	[InlineData("123456", new TokenType[]{TokenType.Number})]
	[InlineData("1.0", new TokenType[]{TokenType.Number})]
	[InlineData("3.141592", new TokenType[]{TokenType.Number})]
	public void Number(string expr, TokenType[] want) => Roundtrip(expr, want);

	[Theory]
	[InlineData("+-*/()", new TokenType[]{TokenType.Add, TokenType.Sub, TokenType.Mul, TokenType.Div, TokenType.Open, TokenType.Close})]
	[InlineData("2 + 2 * (9 / 3)", new TokenType[]{TokenType.Number, TokenType.Add, TokenType.Number, TokenType.Mul, TokenType.Open, TokenType.Number, TokenType.Div, TokenType.Number, TokenType.Close})]
	public void Intermediate(string expr, TokenType[] want) => Roundtrip(expr, want);

	private void Roundtrip(string expr, TokenType[] want)
	{
		var res = Tokenizer.Do(expr);
		Assert.Equal(want, res);
	}

	[Fact]
	public void Throws()
	{
		var chars = "!@#$%^&_=abcdefgwxyzABCXYZ[]{}";
		foreach (var ch in chars)
		{
			var expr = "123 * " + ch;
			var call = () => Tokenizer.Do(expr);
			Assert.Throws<NotImplementedException>(call);
		}
	}
}