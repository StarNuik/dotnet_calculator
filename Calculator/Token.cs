namespace Calculator;

public readonly struct Token
{
	private readonly float? number;
	private readonly IOperator? op;

	public readonly bool IsNumber => number.HasValue;
	public readonly bool IsOperator => op != null;
	
	public Token(float value)
	{
		number = value;
	}

	public Token(IOperator @operator)
	{
		op = @operator;
	}

	public float Number
	{
		get
		{
			if (!IsNumber)
			{
				throw new CalculatorException("taking this value is not allowed");
			}
			return number.Value;
		}
	}

	public IOperator Operator
	{
		get
		{
			if (!IsOperator)
			{
				throw new CalculatorException("taking this value is not allowed");
			}
			return op;
		}
	}
}