namespace Calculator;

public enum Associativity
{
	Left,
	Right,
}

public interface IOperator
{
	public string RepresentedBy { get; }
	public int Precedence { get; }
	public Associativity Associativity { get; }

	public bool GreaterThan(IOperator other)
	{
		return this.Precedence > other.Precedence
			|| (this.Precedence == other.Precedence
				&& other.Associativity == Associativity.Left);
	}
}