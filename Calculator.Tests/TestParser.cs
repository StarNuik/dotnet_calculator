namespace Calculator.Tests;

public class TestParser
{
	[Theory]
	[InlineData(1f, "1")]
	[InlineData(1f, "1.0")]
	[InlineData(3.141592f, "3.141592")]
	[InlineData(1f, "1.")]
	[InlineData(0.1f, ".1")]
	public void Numbers(float want, string tokenValue)
	{
		var tokens = new Token[]{
			new Token{Key = TokenType.Number, Value = tokenValue},
			new Token{Key = TokenType.Eof},
		};
		var res = Parser.Do(tokens);
		Assert.IsType<Expressions.Number>(res);
		Assert.Equal(want, res.Collect());
	}

	[Theory]
	[InlineData("2 + 2", 4f, typeof(Expressions.Add))]
	[InlineData("8 / 2", 4f, typeof(Expressions.Divide))]
	[InlineData("2 * 2", 4f, typeof(Expressions.Multiply))]
	[InlineData("-4", -4f, typeof(Expressions.Negate))]
	[InlineData("6 - 2", 4f, typeof(Expressions.Subtract))]
	[InlineData("sqrt(16)", 4f, typeof(Expressions.Sqrt))]
	[InlineData("pow(2, 2)", 4f, typeof(Expressions.Pow))]
	public void BasicExpressions(string expr, float want, Type wantT)
	{
		var tokens = Tokenizer.Do(expr);
		var res = Parser.Do(tokens);
		Assert.IsType(wantT, res);
		Assert.Equal(want, res.Collect());
	}

	[Theory]
	[InlineData("1 + 1 + -1 + -1", 0f)]
	[InlineData("2 * 2 * 2 * 2 * 0", 0f)]
	[InlineData("-1 + (5 + 4 + 3 + 2 + 1) / 15", 0f)]
	[InlineData("2 + 3 * 4 + 5", 19f)]
	[InlineData("(2 + 3) * 4 + 5", 25f)]
	[InlineData("2 + 3 * (4 + 5)", 29f)]
	[InlineData("(2 + 3) * (4 + 5)", 45f)]
	[InlineData("2 * 3 + 4 * 5", 26f)]
	[InlineData("(2 * 3 + 4) * 5", 50f)]
	[InlineData("2 * (3 + 4 * 5)", 46f)]
	[InlineData("2 * (3 + 4) * 5", 70f)]
	[InlineData("2 * (2 + 2) * 2", 16)]
	[InlineData("2 * sqrt(2 + 2) * 2", 8)]
	[InlineData("2 + sqrt(2 * 2) + 2", 6)]
	[InlineData("2 + sqrt((7 + 3) * 5 / 2)", 7)]
	[InlineData("2 * pow((7 + 3) * 5 / (1 + 1), pow(5, 2) * 2 / 100)", 10)]
	public void ExpressionResults(string expr, float want)
	{
		var tokens = Tokenizer.Do(expr);
		var res = Parser.Do(tokens);
		Assert.Equal(want, res.Collect());
	}

	[Theory]
	[InlineData("1.2.3")]
	[InlineData("1..")]
	[InlineData(".2.")]
	[InlineData("+")]
	[InlineData("-")]
	[InlineData("*")]
	[InlineData("/")]
	[InlineData("(")]
	[InlineData(")")]
	[InlineData("1 +")]
	[InlineData("1 -")]
	[InlineData("1 *")]
	[InlineData("1 /")]
	[InlineData("1 (")]
	[InlineData("1 )")]
	[InlineData("+ 1")]
	[InlineData("* 1")]
	[InlineData("/ 1")]
	[InlineData("( 1")]
	[InlineData(") 1")]
	[InlineData("1 ( 1")]
	[InlineData("1 ) 1")]
	[InlineData("1 1 +")]
	[InlineData("1 1 -")]
	[InlineData("1 1 *")]
	[InlineData("1 1 /")]
	[InlineData("1 1 (")]
	[InlineData("1 1 )")]
	[InlineData("+ 1 1")]
	[InlineData("- 1 1")]
	[InlineData("* 1 1")]
	[InlineData("/ 1 1")]
	[InlineData("( 1 1")]
	[InlineData(") 1 1")]
	[InlineData("1 + 1 +")]
	[InlineData("1 - 1 -")]
	[InlineData("1 * 1 *")]
	[InlineData("1 / 1 /")]
	[InlineData("1 ( 1 (")]
	[InlineData("1 ) 1 )")]
	[InlineData("sqrt")]
	[InlineData("sqrt (")]
	[InlineData("sqrt )")]
	[InlineData("sqrt ( )")]
	[InlineData("sqrt ( + )")]
	[InlineData("sqrt ( - )")]
	[InlineData("sqrt ( * )")]
	[InlineData("sqrt ( / )")]
	[InlineData("sqrt ( , )")]
	[InlineData("pow")]
	[InlineData("pow (")]
	[InlineData("pow )")]
	[InlineData("pow ( )")]
	[InlineData("pow ( + )")]
	[InlineData("pow ( - )")]
	[InlineData("pow ( * )")]
	[InlineData("pow ( / )")]
	[InlineData("pow ( , )")]
	public void Throws(string expr)
	{
		var tokens = Tokenizer.Do(expr);
		var call = () => Parser.Do(tokens);
		Assert.ThrowsAny<CantParseException>(call);
	}
}