namespace Calculator.Expressions;

public class Sqrt : IExpression
{
	IExpression expr;

	public Sqrt(IExpression expr)
	{
		this.expr = expr;
	}

	public float Collect()
	{
		return MathF.Sqrt(expr.Collect());
	}
}
