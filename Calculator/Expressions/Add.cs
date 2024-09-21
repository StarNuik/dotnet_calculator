namespace Calculator.Expressions;

public class Add : IExpression
{
	IExpression left, right;

	public Add(IExpression left, IExpression right)
	{
		(this.left, this.right) = (left, right);
	}

	public float Collect()
	{
		return left.Collect() + right.Collect();
	}
}
