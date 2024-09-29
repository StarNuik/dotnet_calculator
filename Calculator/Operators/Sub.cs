namespace Calculator.Operators;

public class Sub : IOperator
{
    public string RepresentedBy => "-";
    public int Precedence => 1;
    public Associativity Associativity => Associativity.Left;

	public float Execute(float lhs, float rhs)
        => lhs - rhs;
}