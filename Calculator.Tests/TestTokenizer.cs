namespace Calculator.Tests;

public class TestTokenizer
{
	private IOperator[] operators = [
		new Operators.Add(),
		new Operators.Sub(),
		new Operators.Mul(),
		new Operators.Div(),
	];

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

	[Theory]
	[InlineData("1 + 1", "1 1 +")]
	[InlineData("1 - 1", "1 1 -")]
	[InlineData("1 * 1", "1 1 *")]
	[InlineData("1 / 1", "1 1 /")]
	[InlineData("1 + 2 + 3", "1 2 + 3 +")]
	[InlineData("1 - 2 - 3", "1 2 - 3 -")]
	[InlineData("1 * 2 * 3", "1 2 * 3 *")]
	[InlineData("1 / 2 / 3", "1 2 / 3 /")]
	public void SingleOperator(string expr, string want)
	{
		var tokens = Tokenizer.ToRpn(operators, expr);
		var rpn = ToString(tokens);
		Assert.Equal(want, rpn);
	}

	[Theory]
	// [InlineData("1 ** 1")]
	[InlineData(".")]
	[InlineData("..")]
	[InlineData("1..")]
	[InlineData("..1")]
	[InlineData(".1.")]
	[InlineData("1 ^ 1")]
	[InlineData("pow(1, 1)")]
	[InlineData("tan(1)")]
	[InlineData("abc")]
	public void Exceptions(string expr)
	{
		Assert.Throws<TokenizerException>(
			() => Tokenizer.ToRpn(operators, expr)
		);
	}

	private string ToString(Token[] tokens)
	{
		var strings = tokens.Select(
			tok => tok.IsNumber
				? tok.Number.ToString()
				: tok.Operator.RepresentedBy
		);
		return string.Join(' ', strings);
	}
}