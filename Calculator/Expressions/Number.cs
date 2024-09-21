namespace Calculator.Expressions;

public class Number : IExpression
{
	float value;

	public Number(float val)
	{
		this.value = val;
	}

	public float Collect()
	{
		return value;
	}
}
