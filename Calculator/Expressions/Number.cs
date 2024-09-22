namespace Calculator.Expressions;

public class Number : IExpression
{
	float value;

    public Number(Token token)
    {
		try
		{
			value = float.Parse(token.Value);
		}
		catch
		{
			throw new CantParseException(
				$"number is incorrect: '{token.Value}'"
			);
		}
    }

    public float Collect()
	{
		return value;
	}
}
