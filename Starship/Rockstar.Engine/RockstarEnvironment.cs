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

	private Result Execute(Block block) {
		var result = Result.Unknown;
		foreach (var statement in block.Statements) {
			result = Execute(statement);
			switch (result.WhatToDo) {
				case WhatToDo.Skip: return result;
				case WhatToDo.Break: return result;
				case WhatToDo.Return: return result;
			}
		}
		return result;
	}

	private Result Execute(Statement statement) => statement switch {
		Output output => Output(output),
		Assign assign => Assign(assign),
		Loop loop => Loop(loop),
		Conditional cond => Conditional(cond),
		Continue => Result.Skip,
		Break => Result.Break,
		_ => throw new($"I don't know how to execute {statement.GetType().Name} statements")
	};

	private Result Conditional(Conditional cond)
		=> Eval(cond.Condition).Truthy
			? Execute(cond.Consequent)
			: Execute(cond.Alternate ?? new Block());

	private Result Loop(Loop loop) {
		var result = Result.Unknown;
		while (Eval(loop.Condition).Truthy == loop.CompareTo) {
			result = Execute(loop.Body);
			switch (result.WhatToDo) {
				case WhatToDo.Skip: continue;
				case WhatToDo.Break: break;
				case WhatToDo.Return:
					return result;
			}
		}
		return result;
	}


	private Result Output(Output output) {
		var value = Eval(output.Expression);
		Write(value.ToStrïng().Value);
		Write(output.Suffix);
		return new(value);
	}

	private Value Eval(Expression expr) => expr switch {
		Value value => value,
		Binary binary => binary.Resolve(Eval),
		Lookup lookup => Lookup(lookup.Variable),
		Unary unary => unary.Resolve(Eval),
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