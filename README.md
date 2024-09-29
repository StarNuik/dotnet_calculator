# dotnet_calculator
Basic Cli calculator

## Installation
```bash
git clone git@github.com:StarNuik/dotnet_calculator.git
cd dotnet_calculator
dotnet build Calculator -o ./build
alias calc="./build/Calculator"

calc "1 + 1"
```

## Supported Operations
```bash
calc "5 + 10"       # Addition
calc "5 - 10"       # Subtraction
calc "5 * 10"       # Multiplication
calc "5 / 10"       # Division (floating point)
calc "5 / (5 + 5)"  # Parentheses

calc "1 * (2 - 3 * 4 / (5 + 6) - 7) * 8 * 9"
# -438.54544
```

## Testing
```bash
dotnet test
```

## Extensibility / Custom Operators
* Implement the IOperator interface
* Append your custom operators to the default operators array
* Pass these operators to one of the Program's methods.

### Implementing IOperator
```c#
private class Exp : IOperator
{
	// This full sequence of characters will be parsed into this operator.
	public string RepresentedBy => "^";

	// Priority of operator execution. Higher precedence will result in earlier execution.
	// Add / Sub have a precedence of 1
	// Mul / Div have a precedence of 2
	public int Precedence => 4;

	// In what direction should multi-operator expressions execute.
	// Left Example: "1 - 2 - 3" == "(1 - 2) - 3"
	// Right Example: "1 ^ 2 ^ 3" == "1 ^ (2 ^ 3)
	public Associativity Associativity => Associativity.Right;

	// Calculate the result of the operator.
	public float Execute(float lhs, float rhs)
		=> MathF.Pow(lhs, rhs);
}
```

### Using custom operators
```c#
var expr = "2 ^ 4";
var op = new Exp();
var extended = Operators.Default.Array.Append(op);
var res = Program.DoString(extended.ToArray(), expr);
Console.WriteLine(res);
```
