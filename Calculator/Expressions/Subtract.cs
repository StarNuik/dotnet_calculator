namespace Calculator.Expressions;

public class Subtract : IExpression
{
	IExpression left, right;

	public Subtract(IExpression left, IExpression right)
	{
		(this.left, this.right) = (left, right);
	}

	public float Collect()
	{
		return left.Collect() - right.Collect();
	}
}
