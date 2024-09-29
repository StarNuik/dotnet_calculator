namespace Calculator;

public enum Associativity
{
	Left,
	Right,
}

public interface IOperator
{
	// This full sequence of characters is required to appear in the input string to invoke the operator.
	public string RepresentedBy { get; }

	// Priority of operator execution. Higher precedence will result in earlier execution.
	// Add / Sub haev a precedence of 1
	// Mul / Div have a precedence of 2
	public int Precedence { get; }
	
	// In what direction should multi-operator expressions be executed.
	// Left Example: "1 - 2 - 3" == "(1 - 2) - 3"
	// Right Example: "1 ^ 2 ^ 3" == "1 ^ (2 ^ 3)
	public Associativity Associativity { get; }

	// Calculates the result of the operator.
	public float Execute(float lhs, float rhs);

	public bool GreaterThan(IOperator other)
	{
		return this.Precedence > other.Precedence
			|| (this.Precedence == other.Precedence
				&& other.Associativity == Associativity.Left);
	}
}