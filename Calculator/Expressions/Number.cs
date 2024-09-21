namespace Calculator.Expressions;

public class Number : IExpression
{
	float value;

    public Number(Token token)
    {
		value = float.Parse(token.Value);
    }

    public float Collect()
	{
		return value;
	}
}
