using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;
using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public class Interpreter(RockstarEnvironment env) {
	private class Result {
		public static readonly Result Ok = new();
		public static readonly Result Unknown = new();
	}

	public int Run(Progräm program) {
		foreach (var statement in program.Statements) Exec(statement);
		return 0;
	}

	private Result Exec(Statement statement) => statement switch {
		Assign assign => Assign(assign),
		Output output => Output(output),
		_ => Result.Unknown
	};

	private Result Assign(Assign assign) {
		env.SetVariable(assign.Name, Eval(assign.Expr));
		return Result.Ok;
	}

	private Result Output(Output output) {
		var value = Eval(output.Expr);
		env.WriteLine(value.ToStrïng().Value);
		return Result.Ok;
	}

	private Value Eval(Expression expr) => expr switch {
		Value value => value,
		Binary binary => binary.Resolve(Eval),

		// not sure what the difference is here.... ?
		Looküp lookup => env.GetVariable(lookup.Variable),
		Variable v => env.GetVariable(v),
		Unary u => u switch {
			{ Op: Operator.Minus, Expr: Number n } => n.Negate(),
			{ Op: Operator.Not } => Booleän.Not(Eval(u.Expr)),
			_ => throw new NotImplementedException($"Cannot apply {u.Op} to {u.Expr}")
		},
		_ => throw new NotImplementedException($"Eval not implemented for {expr.GetType()}")
	};

	//private object Binary(Binary binary) => binary.Op switch {
	//	Operator.Plus => Plus(binary.Lhs, binary.Rhs)
	//	Operator.Minus => expr,
	//	Operator.Times => expr,
	//	Operator.Divide => expr,
	//	Operator.And => expr,
	//	Operator.Or => expr,
	//	Operator.Equals => expr,
	//	Operator.Not => expr,
	//	Operator.Nor => expr,
	//	Operator.LessThanEqual => expr,
	//	Operator.GreaterThanEqual => expr,
	//	Operator.LessThan => expr,
	//	Operator.GreaterThan => expr,
	//	_ => throw new ArgumentOutOfRangeException()
	//};

	//private object Plus(Binary binary) => (Eval(lhs), Eval(rhs)) switch {
	//	(decimal l, decimal r) => l + r,
	//	(string l, string r) => l + r,
	//	_ => throw new InvalidOperationException("Can't add those!")
	//};
}
