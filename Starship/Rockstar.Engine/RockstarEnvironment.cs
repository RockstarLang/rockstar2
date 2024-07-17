using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;
using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public class RockstarEnvironment(IRockstarIO io) {
	public RockstarEnvironment(IRockstarIO io, RockstarEnvironment parent) : this(io) {
		Parent = parent;
	}

	public RockstarEnvironment? Parent { get; init; }
	public RockstarEnvironment Extend() => new(IO, this);
	protected IRockstarIO IO = io;

	public string? ReadInput() => IO.Read();
	public void WriteLine(string? output) => IO.Write((output ?? String.Empty) + Environment.NewLine);
	public void Write(string output) => IO.Write(output);

	private Variable? pronounTarget;

	private Variable QualifyPronoun(Variable variable) =>
		variable is Pronoun pronoun
			? pronounTarget ?? throw new($"You must assign a variable before using a pronoun ('{pronoun.Name}')")
			: variable;

	private readonly Dictionary<string, Value> variables = new();

	private Value SetLocal(Variable variable, Value value)
		=> variables[variable.Key] = value;

	public RockstarEnvironment? GetScope(Variable variable)
		=> variables.ContainsKey(variable.Key) ? this : Parent?.GetScope(variable);

	public Result SetVariable(Variable variable, Value value) {
		var target = QualifyPronoun(variable);
		var scope = GetScope(target) ?? this;
		if (variable is Pronoun pronoun) {
			scope.SetLocal(target, value);
		//} else if (variable.Index != default) {
		//	var index = Eval(variable.Index);
		//	scope.SetArray(variable, index, value);
		} else {
			pronounTarget = target;
			scope.SetLocal(target, value);
		}
		return new(value);
	}

	public Result Execute(Program program)
		=> program.Blocks.Aggregate(Result.Unknown, (_, block) => Execute(block));

	private Result Execute(Block block)
		=> block.Statements.Aggregate(Result.Unknown, (_, statement) => Execute(statement));

	private Result Execute(Statement statement) => statement switch {
		Output output => Output(output),
		Assign assign => Assign(assign),
		_ => throw new($"I don't know how to execute {statement.GetType().Name} statements")
	};

	private Result Output(Output output) {
		var value = Eval(output.Expression);
		WriteLine(value.ToStrïng().Value);
		return new(value);
	}

	private Value Eval(Expression expr) => expr switch {
		Value value => value,
		Lookup lookup => Lookup(lookup.Variable),
		_ => throw new NotImplementedException($"Eval not implemented for {expr.GetType()}")
	};

	public Result Assign(Variable variable, Value value)
		=> SetVariable(variable, value);


	public Result Assign(Assign assign)
		=> Assign(assign.Variable, Eval(assign.Expression));

	private Value? LookupValue(string key)
		=> variables.TryGetValue(key, out var value) ? value : Parent?.LookupValue(key);

	public Value Lookup(Variable variable) {
		var key = variable is Pronoun pronoun ? QualifyPronoun(pronoun).Key : variable.Key;
		var value = LookupValue(key);
		if (value != null) return value;
		throw new($"Unknown variable '{variable.Name}'");
	}
}

public class Result(Value value) {
	public Value Value => value;
	public static Result Unknown = new(Mysterious.Instance);
}