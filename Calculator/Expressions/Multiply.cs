namespace Calculator.Expressions;

public class Multiply : IExpression
{
	IExpression left, right;

	public Multiply(IExpression left, IExpression right)
	{
		(this.left, this.right) = (left, right);
	}

	public float Collect()
	{
		return left.Collect() * right.Collect();
	}
}
