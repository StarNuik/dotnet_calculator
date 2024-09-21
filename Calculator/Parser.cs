namespace Calculator;

/*
(2 * -(3 + -5)) / -(-(35 - 6) / -3)
*/

/* kind side
root ::= (block | addsub)
block ::= "(" addsub ")"
addsub ::= muldiv {( "+" | "-") muldiv}
muldiv ::= unary {( "*" | "/") unary}
unary ::= ["-"] (number | block)
*/

/* evil side
expression ::= ( addsub | "(" addsub ")")
addsub ::= muldiv [( "+" | "-") muldiv]
muldiv ::= unary [( "*" | "/") unary]
unary ::= ["-"] (number | expression)
*/

public interface IExpression
{
	// public IExpression Execute();
	public float Collect();
}





public static class Parser
{
	public static void Do(Token[] @in)
	{}
}