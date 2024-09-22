namespace Calculator;

/*
expression ::= multiply {( "+" | "-") multiply}
multiply ::= unary {( "*" | "/") unary}
unary ::= ["-"] primary
primary ::= (number | "(" expression ")" | function )
function ::= "sqrt" "(" expression ")"
	| "pow" "(" expression "," expression ")"
*/

/*

*/

public interface IExpression
{
	public float Collect();
}

public static class Parser
{
	public static IExpression Do(Token[] tokens)
	{
		(var expr, tokens) = Expression(tokens);
		if (tokens[0].Key != TokenType.Eof)
		{
			throw new CantParseException("generic parsing error");
		}
		return expr;
	}

	static (IExpression, Token[]) Expression(Token[] tokens)
	{
		(var expr, tokens) = Multiply(tokens);
		var op = tokens[0].Key;
		while (op == TokenType.Add || op == TokenType.Sub)
		{
			(var other, tokens) = Multiply(tokens[1..]);
			expr = op switch
			{
				TokenType.Add => new Expressions.Add(expr, other),
				TokenType.Sub => new Expressions.Subtract(expr, other),
				_ => throw new CantParseException("impossible error"),
			};
			op = tokens[0].Key;
		}
		return (expr, tokens);
	}

	static (IExpression, Token[]) Multiply(Token[] tokens)
	{
		(var expr, tokens) = Unary(tokens);
		var op = tokens[0].Key;
		while (op == TokenType.Mul || op == TokenType.Div)
		{
			(var other, tokens) = Unary(tokens[1..]);
			expr = op switch
			{
				TokenType.Mul => new Expressions.Multiply(expr, other),
				TokenType.Div => new Expressions.Divide(expr, other),
				_ => throw new CantParseException("impossible error"),
			};
			op = tokens[0].Key;
		}
		return (expr, tokens);
	}

	static (IExpression, Token[]) Unary(Token[] tokens)
	{
		if (tokens[0].Key == TokenType.Sub)
		{
			(var expr, tokens) = Primary(tokens[1..]);
			expr = new Expressions.Negate(expr);
			return (expr, tokens);
		}
		
		return Primary(tokens);
	}

	static (IExpression, Token[]) Primary(Token[] tokens)
	{
		return tokens[0].Key switch
		{
			TokenType.Func => Function(tokens),
			TokenType.Open => Block(tokens),
			TokenType.Number => (new Expressions.Number(tokens[0]), tokens[1..]),
			_ => throw new CantParseException("trailing operator (probably)"),
		};
	}


	static (IExpression, Token[]) Function(Token[] tokens)
	{
		if (tokens[0].Value == "sqrt")
		{
			(var expr, tokens) = Block(tokens[1..]);
			return (new Expressions.Sqrt(expr), tokens);
		}
		if (tokens[0].Value == "pow")
		{
			(var lhs, var rhs, tokens) = CommaBlock(tokens[1..]);
			return (new Expressions.Pow(lhs, rhs), tokens);
		}
		throw new CantParseException("impossible error");
	}

	static (IExpression, Token[]) Block(Token[] tokens)
	{
		if (tokens[0].Key != TokenType.Open)
		{
			throw new CantParseException("no open bracket");
		}

		(var expr, tokens) = Expression(tokens[1..]);
		
		if (tokens[0].Key != TokenType.Close)
		{
			throw new CantParseException("no closed bracket");
		}
		return (expr, tokens[1..]);
	}

	static (IExpression, IExpression, Token[]) CommaBlock(Token[] tokens)
	{
		if (tokens[0].Key != TokenType.Open)
		{
			throw new CantParseException("no open bracket");
		}

		(var lhs, tokens) = Expression(tokens[1..]);
		if (tokens[0].Key != TokenType.Comma)
		{
			throw new CantParseException("no comma");
		}

		(var rhs, tokens) = Expression(tokens[1..]);
		if (tokens[0].Key != TokenType.Close)
		{
			throw new CantParseException("no closing bracket");
		}
		return (lhs, rhs, tokens[1..]);
	}
}