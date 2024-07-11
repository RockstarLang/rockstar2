using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;
using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public interface RockstarIO {
	public string? Read();
	public void Write(string? s);
}
public class RockstarEnvironment(RockstarIO io) {

	public RockstarEnvironment Extend() {
		var extended = new RockstarEnvironment(IO);
		extended.CopyVariablesFrom(this);
		return extended;
	}

	private void CopyVariablesFrom(RockstarEnvironment that) {
		foreach (var variable in that.variables) {
			this.variables[variable.Key] = variable.Value;
		}
	}

	protected RockstarIO IO = io;
	
	public string? ReadInput() => IO.Read();
	public void WriteLine(string? output) => IO.Write((output ?? String.Empty)+ Environment.NewLine);
	public void Write(string output) => IO.Write(output);
	private Variable? pronounTarget;

	private readonly Dictionary<string, Value> variables = new();

	private Variable AssertTarget(Pronoun pronoun)
		=> pronounTarget ?? throw new($"You must assign a variable before using a pronoun ('{pronoun.Name}')");

	public Result SetVariable(Variable variable, Value value) {
		if (variable is Pronoun pronoun) {
			variables[AssertTarget(pronoun).Key] = value;
		} else {
			pronounTarget = variable;
			variables[variable.Key] = value;
		}

		return Result.Ok;
	}

	public Value GetVariable(Variable variable) {
		var key = (variable is Pronoun pronoun ? AssertTarget(pronoun).Key : variable.Key);
		return variables.TryGetValue(key, out var value) ? value : throw new($"Unknown variable '{variable.Name}'");
	}

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
		Loop loop => Loop(loop),
		FunctionCall call => Call(call),
		_ => throw new($"I don't know how to execute {statement.GetType().Name} statements")
	};

	private Result Call(FunctionCall call) {
		var value = GetVariable(call.Function);
		if (value is not Function function) throw new($"'{call.Function.Name}' is not a function");
		var names = function.Args.ToList();
		var values = call.Args.Select(Eval).ToList();
		if (names.Count != values.Count) throw new($"Wrong number of arguments supplied to function {call.Function.Name} - expected {names.Count} ({String.Join(", ", names.Select(v => v.Name))}), got {values.Count}");
		var scope = this.Extend();
		for (var i = 0; i < names.Count; i++) scope.SetVariable(names[i], values[i]);
		return scope.Exec(function.Body);
	}

	private Result Loop(Loop loop) {
		var result = Result.Unknown;
		var condition = Eval(loop.Condition);
		while (condition.Truthy == loop.CompareTo) {
			result = Exec(loop.Body);
			condition = Eval(loop.Condition);
		}
		return result;
	}

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
		=> SetVariable(variable, value);

	private Result Assign(Assign assign)
		=> Assign(assign.Variable, Eval(assign.Expr));

	private Result Output(Output output) {
		var value = Eval(output.Expr);
		WriteLine(value.ToStrïng().Value);
		return Result.Ok;
	}

	private Value Eval(Expression expr) => expr switch {
		Value value => value,
		Binary binary => binary.Resolve(Eval),

		// not sure what the difference is here.... ?
		Looküp lookup => GetVariable(lookup.Variable),
		Variable v => GetVariable(v),
		Unary u => u switch {
			{ Op: Operator.Minus, Expr: Number n } => n.Negate(),
			{ Op: Operator.Not } => Booleän.Not(Eval(u.Expr)),
			_ => throw new NotImplementedException($"Cannot apply {u.Op} to {u.Expr}")
		},
		_ => throw new NotImplementedException($"Eval not implemented for {expr.GetType()}")
	};
}