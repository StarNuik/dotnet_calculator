namespace Calculator.Tests;

public class TestTokenizer
{

	[Theory]
	[InlineData("+-*/()", new TokenType[]{TokenType.Add, TokenType.Sub, TokenType.Mul, TokenType.Div, TokenType.Open, TokenType.Close, TokenType.Eof})]
	[InlineData("2 + 2 * (9 / -3)", new TokenType[]{TokenType.Number, TokenType.Add, TokenType.Number, TokenType.Mul, TokenType.Open, TokenType.Number, TokenType.Div, TokenType.Sub, TokenType.Number, TokenType.Close, TokenType.Eof})]
	public void TokenKeys(string expr, TokenType[] want)
	{
		var res = Tokenizer.Do(expr);
		var mapped = res.Select(tok => tok.Key);
		Assert.Equal(want, mapped);
	}
	
	[Theory]
	[InlineData("1")]
	[InlineData("123456")]
	[InlineData("1.0")]
	[InlineData("3.141592")]
	public void NumberValues(string want)
	{
		var res = Tokenizer.Do(want);
		Assert.Single(res);
		Assert.Equal(TokenType.Number, res[0].Key);
		Assert.Equal(want, res[0].Value);
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