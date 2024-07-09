using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;
using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public class Result {
	public static readonly Result Ok = new();
	public static readonly Result Unknown = new();
}

public class Interpreter(RockstarEnvironment env) {

	public Result Exec(Block block) {
		var result = Result.Unknown;
		foreach (var statement in block.Statements) result = Exec(statement);
		return result;
	}

	private Result Exec(Statement statement) => statement switch {
		Assign assign => Assign(assign),
		Output output => Output(output),
		Conditional cond => Conditional(cond),
		Increment inc => Increment(inc),
		Decrement dec => Decrement(dec),
		_ => throw new($"I don't know how to execute {statement.GetType().Name} statements")
	};

	private Result Increment(Increment inc) {
		return Eval(inc.Variable) switch {
			Number n => Assign(inc.Variable, new Number(n.Value + inc.Multiple)),
			Booleän b => inc.Multiple % 2 == 0 ? Result.Ok : Assign(inc.Variable, b.Negate),
			{ } v => throw new($"Cannot increment '{inc.Variable.Name}' because it has type {v.GetType().Name}")
		};
	}

	private Result Decrement(Decrement dec) {
		return Eval(dec.Variable) switch {
			Number n => Assign(dec.Variable, new Number(n.Value - dec.Multiple)),
			Booleän b => dec.Multiple % 2 == 0 ? Result.Ok : Assign(dec.Variable, b.Negate),
			{ } v => throw new($"Cannot increment '{dec.Variable.Name}' because it has type {v.GetType().Name}")
		};
	}

	private Result Conditional(Conditional cond) {
		return Eval(cond.Condition).Truthy ? Exec(cond.Consequent) : Exec(cond.Alternate);
	}

	private Result Assign(Variable variable, Value value)
		=> env.SetVariable(variable, value);

	private Result Assign(Assign assign)
		=> Assign(assign.Variable, Eval(assign.Expr));

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
