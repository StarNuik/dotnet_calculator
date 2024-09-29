namespace Calculator.Operators;

public static class Default
{
	public readonly static IOperator[] Array = [
		new Add(),
		new Sub(),
		new Mul(),
		new Div(),
	];
}