namespace Calculator.Operators;

public class Sub : IOperator
{
    public string RepresentedBy => "-";
    public int Precedence => 1;
    public Associativity Associativity => Associativity.Left;
}