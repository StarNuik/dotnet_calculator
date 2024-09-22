namespace Calculator.Tests;

public class TestTokenizer
{

	[Theory]
	[InlineData("+-*/(),", new TokenType[]{TokenType.Add, TokenType.Sub, TokenType.Mul, TokenType.Div, TokenType.Open, TokenType.Close, TokenType.Comma})]
	[InlineData("2 + 2 * (9 / -3)", new TokenType[]{TokenType.Number, TokenType.Add, TokenType.Number, TokenType.Mul, TokenType.Open, TokenType.Number, TokenType.Div, TokenType.Sub, TokenType.Number, TokenType.Close})]
	public void TokenKeys(string expr, TokenType[] want)
		=> AssertAllKeys(expr, want);
	
	[Theory]
	[InlineData("1")]
	[InlineData("123456")]
	[InlineData("1.0")]
	[InlineData("3.141592")]
	public void NumberValues(string want)
	{
		var res = Tokenizer.Do(want);
		Assert.Equal(2, res.Length);
		Assert.Equal(TokenType.Number, res[0].Key);
		Assert.Equal(want, res[0].Value);
	}

	[Theory]
	[InlineData("sqrt()", new TokenType[]{TokenType.Func, TokenType.Open, TokenType.Close})]
	[InlineData("pow(,)", new TokenType[]{TokenType.Func, TokenType.Open, TokenType.Comma, TokenType.Close})]
	public void FuncKeys(string expr, TokenType[] want)
		=> AssertAllKeys(expr, want);

	[Fact]
	public void Throws()
	{
		var chars = "!@#$%^&_=abcdefgwxyzABCXYZ[]{}";
		foreach (var ch in chars)
		{
			var expr = "123 * " + ch;
			var call = () => Tokenizer.Do(expr);
			Assert.Throws<CantTokenizeException>(call);
		}
	}

	private void AssertAllKeys(string expr, TokenType[] want)
	{
		want = want.Append(TokenType.Eof).ToArray();
		var res = Tokenizer.Do(expr);
		var mapped = res.Select(tok => tok.Key);
		Assert.Equal(want, mapped);
	}
}