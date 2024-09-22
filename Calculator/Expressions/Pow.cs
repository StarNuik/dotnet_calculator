namespace Calculator.Expressions;

public class Pow : IExpression
{
	IExpression left, right;

	public Pow(IExpression left, IExpression right)
	{
		(this.left, this.right) = (left, right);
	}

	public float Collect()
	{
		return MathF.Pow(left.Collect(), right.Collect());
	}
}
