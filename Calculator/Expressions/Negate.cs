namespace Calculator.Expressions;

public class Negate : IExpression
{
	IExpression expr;

	public Negate(IExpression expr)
	{
		this.expr = expr;
	}

	public float Collect()
	{
		return -expr.Collect();
	}
}
