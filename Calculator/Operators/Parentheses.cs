namespace Calculator.Operators;

internal static class Parentheses
{
	internal class Open : IOperator
	{
		public string RepresentedBy => "(";

		public int Precedence => throw new NotImplementedException();
		public Associativity Associativity => throw new NotImplementedException();
		public float Execute(float lhs, float rhs) => throw new NotImplementedException();
	}

	internal class Close : IOperator
	{
		public string RepresentedBy => ")";
		
		public int Precedence => throw new NotImplementedException();
		public Associativity Associativity => throw new NotImplementedException();
		public float Execute(float lhs, float rhs) => throw new NotImplementedException();
	}

	internal static void AddTo(Dictionary<string, IOperator> ops)
	{
		var add = (IOperator op) => ops[op.RepresentedBy] = op;
		add(new Open());
		add(new Close());
	}
}