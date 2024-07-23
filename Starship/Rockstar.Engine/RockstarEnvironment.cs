using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;
using Rockstar.Engine.Values;
using System;
using Array = Rockstar.Engine.Values.Array;

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
		} else if (variable.Indexes.Any()) {
			var indexes = variable.Indexes.Select(Eval).ToList();
			scope.SetArray(variable, new(indexes), value);
		} else {
			pronounTarget = target;
			scope.SetLocal(target, value);
		}
		return new(value);
	}

	private Value SetArray(Variable variable, List<Value> indexes, Value value) {
		variables.TryAdd(variable.Key, new Array());
		if (variables[variable.Key] is not Array array)
			throw new($"Error: {variable.Name} is not an indexed variable");
		array.Set(indexes, value);
		return variables[variable.Key];
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
		FunctionCall call => Call(call),
		Return r => Return(r),
		Continue => Result.Skip,
		Break => Result.Break,
		ExpressionStatement e => ExpressionStatement(e),
		_ => throw new($"I don't know how to execute {statement.GetType().Name} statements")
	};

	private Result ExpressionStatement(ExpressionStatement e)
		=> Result.Return(Eval(e.Expression));

	private Result Return(ExpressionStatement r) {
		var value = Eval(r.Expression);
		return Result.Return(value);
	}

	private Result Call(FunctionCall call)
		=> Call(call, []);

	private Result Call(FunctionCall call, Stack<Expression> bucket) {
		var value = Lookup(call.Function);
		if (value is not Function function) throw new($"'{call.Function.Name}' is not a function");
		var names = function.Args.ToList();
		List<Value> values = [];
		foreach (var arg in call.Args.Take(names.Count)) {
			if (arg is FunctionCall nestedCall) {
				values.Add(Call(nestedCall, bucket).Value);
			} else {
				values.Add(Eval(arg));
			}
		}
		if (call.Args.Count + bucket.Count < names.Count) {
			throw new($"Not enough arguments supplied to function {call.Function.Name} - expected {names.Count} ({String.Join(", ", names.Select(v => v.Name))}), got {call.Args.Count}");
		}
		while (values.Count < names.Count) values.Add(Eval(bucket.Pop()));
		foreach (var expression in call.Args.Skip(names.Count)) bucket.Push(expression);
		var scope = this.Extend();
		for (var i = 0; i < names.Count; i++) scope.SetLocal(names[i], values[i]);
		return scope.Execute(function.Body);
	}

	private Result Conditional(Conditional cond)
		=> Eval(cond.Condition).Truthy
			? Execute(cond.Consequent)
			: Execute(cond.Alternate ?? new Block());

	private Result Loop(Loop loop) {
		var result = Result.Unknown;
	outer: while (Eval(loop.Condition).Truthy == loop.CompareTo) {
			result = Execute(loop.Body);
			switch (result.WhatToDo) {
				case WhatToDo.Skip: continue;
				case WhatToDo.Break: return result;
				case WhatToDo.Return: return result;
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
		FunctionCall call => Call(call).Value,
		_ => throw new NotImplementedException($"Eval not implemented for {expr.GetType()}")
	};


	public Result Assign(Variable variable, Value value)
		=> SetVariable(variable, value);


	public Result Assign(Assign assign)
		=> Assign(assign.Variable, Eval(assign.Expression));

	private Value LookupValue(string key)
		=> variables.TryGetValue(key, out var value) ? value : Parent?.LookupValue(key) ?? Mysterious.Instance;

	public Value Lookup(Variable variable) {
		var key = variable is Pronoun pronoun ? QualifyPronoun(pronoun).Key : variable.Key;
		var value = LookupValue(key);
		if (value is not Array array) return value;
		var indexes = variable.Indexes.Select(Eval).ToList();
		return array.Get(indexes);
	}

}