#pragma warning disable CS8602

namespace Calculator.Tests;

public class TestExtensibility
{
    private class MockOperator : IOperator
    {
        public string RepresentedBy { get; set; } = "";
        public int Precedence { get; set; }
        public Associativity Associativity { get; set; }
		public Func<float, float, float>? ExecuteFunc { get; set; }

        public float Execute(float lhs, float rhs)
			=> ExecuteFunc(lhs, rhs);
    }

    [Theory]
	[InlineData("2 ^ 4", 16f)]
	[InlineData("2 ^ (3 + 1)", 16f)]
	[InlineData("(1 + 1) ^ 4", 16f)]
	[InlineData("2 ^ 4 ^ 2", 65536f)]
	[InlineData("(0.5 * 4) ^ (2 * 2) ^ (3 - 1)", 65536f)]
	public void IsExtensible(string expr, float want)
	{
		var before = Operators.Default.Array;
		Assert.Throws<CalculatorException>(
			() => Program.Do(before, expr)
		);

		var op = new MockOperator()
		{
			RepresentedBy = "^",
			Precedence = 4,
			Associativity = Associativity.Right,
			ExecuteFunc = (lhs, rhs) => MathF.Pow(lhs, rhs),
		};
		var after = before.Append(op).ToArray();
		var res = Program.Do(after, expr);
		Assert.Equal(want, res);
	}
}