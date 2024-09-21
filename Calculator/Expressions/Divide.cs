namespace Calculator.Expressions;

public class Divide : IExpression
{
	IExpression left, right;

	public Divide(IExpression left, IExpression right)
	{
		(this.left, this.right) = (left, right);
	}

	public float Collect()
	{
		return left.Collect() / right.Collect();
	}
}
