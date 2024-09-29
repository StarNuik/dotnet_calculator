namespace Calculator.Operators;

public class Div : IOperator
{
    public string RepresentedBy => "/";
    public int Precedence => 2;
    public Associativity Associativity => Associativity.Left;

	public float Execute(float lhs, float rhs)
        => lhs / rhs;
}