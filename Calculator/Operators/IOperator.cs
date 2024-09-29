namespace Calculator;

public enum Associativity
{
	Left,
	Right,
}

public interface IOperator
{
	/// <summary>
	/// This full sequence of characters will be parsed into this operator.
	/// </summary>
	public string RepresentedBy { get; }

	/// <summary>
	/// Priority of operator execution. Higher precedence will result in earlier execution.
	/// Add / Sub have a precedence of 1
	/// Mul / Div have a precedence of 2
	/// </summary>
	public int Precedence { get; }
	
	/// <summary>
	/// In what direction should multi-operator expressions execute.
	/// Left Example: "1 - 2 - 3" == "(1 - 2) - 3"
	/// Right Example: "1 ^ 2 ^ 3" == "1 ^ (2 ^ 3)
	/// </summary>
	public Associativity Associativity { get; }

	/// <summary>
	/// Calculate the result of the operator.
	/// </summary>
	public float Execute(float lhs, float rhs);

	public bool GreaterThan(IOperator other)
	{
		return this.Precedence > other.Precedence
			|| (this.Precedence == other.Precedence
				&& other.Associativity == Associativity.Left);
	}
}