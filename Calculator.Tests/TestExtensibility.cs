#pragma warning disable CS8602

namespace Calculator.Tests;

public class TestExtensibility
{
	private class Exp : IOperator
	{
		public string RepresentedBy => "^";
		public int Precedence => 4;
		public Associativity Associativity => Associativity.Right;

		public float Execute(float lhs, float rhs)
			=> MathF.Pow(lhs, rhs);
	}

	[Theory]
	[InlineData("2 ^ 4", "16")]
	[InlineData("2 ^ (3 + 1)", "16")]
	[InlineData("(1 + 1) ^ 4", "16")]
	[InlineData("2 ^ 4 ^ 2", "65536")]
	[InlineData("(0.5 * 4) ^ (2 * 2) ^ (3 - 1)", "65536")]
	public void IsExtensible(string expr, string want)
	{
		var before = Operators.Default.Array;
		var err = Program.DoString(before, expr);
		Assert.StartsWith("Error:", err);

		var op = new Exp();
		var extended = Operators.Default.Array.Append(op);
		var res = Program.DoString(extended.ToArray(), expr);
		Assert.Equal(want, res);
	}
}