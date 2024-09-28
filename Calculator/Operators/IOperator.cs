namespace Calculator;

public enum Associativity
{
	Left,
	Right,
}

public interface IOperator
{
	public int Precedence { get; }
	public string RepresentedBy { get; }
}