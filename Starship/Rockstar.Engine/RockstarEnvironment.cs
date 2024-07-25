using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;
using Rockstar.Engine.Values;
using System;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;
using Array = Rockstar.Engine.Values.Array;

namespace Rockstar.Engine;

public class RockstarEnvironment(IRockstarIO io) {
	public RockstarEnvironment(IRockstarIO io, RockstarEnvironment parent) : this(io) {
		Parent = parent;
	}

	public RockstarEnvironment? Parent { get; init; }

	public RockstarEnvironment Extend() {
		var scope = new RockstarEnvironment(IO, this);
		foreach (var variable in variables) scope.variables[variable.Key] = variable.Value;
		return scope;
	}

	protected IRockstarIO IO = io;

	public string? ReadInput() => IO.Read();
	public void Write(string output) => IO.Write(output);

	private Variable? pronounSubject;
	internal void UpdatePronounTarget(Variable variable) => pronounSubject = variable;

	private Variable QualifyPronoun(Variable variable) =>
		variable is Pronoun pronoun
			? pronounSubject ?? throw new($"You must assign a variable before using a pronoun ('{pronoun.Name}')")
			: variable;

	private readonly Dictionary<string, Value> variables = new();

	internal Value SetLocal(Variable variable, Value value)
		=> variables[variable.Key] = value;

	//private RockstarEnvironment? FindScope(Variable variable)
	//	=> variables.ContainsKey(variable.Key) ? GetGlobalScope(variable) : null;

	//public RockstarEnvironment? GetGlobalScope(Variable variable)
	//	=> Parent?.FindScope(variable) ?? this;

	public RockstarEnvironment? GetScope(Variable variable)
		=> variables.ContainsKey(variable.Key) ? this : Parent?.GetScope(variable);

	public Result SetVariable(Variable variable, Value value, bool global = false) {
		var target = QualifyPronoun(variable);
		var scope = GetScope(target) ?? this;
		if (variable is Pronoun pronoun) {
			scope.SetLocal(target, value);
		} else if (variable.Indexes.Any()) {
			var indexes = variable.Indexes.Select(expr => Eval(expr)).ToList();
			scope.SetArray(variable, new(indexes), value);
		} else {
			pronounSubject = target;
			scope.SetLocal(target, value);
		}
		return new(value);
	}

	private Value SetArray(Variable variable, List<Value> indexes, Value value) {
		variables.TryAdd(variable.Key, new Array());
		return variables[variable.Key] switch {
			Array array => array.Set(indexes, value),
			Strïng s => s.SetCharAt(indexes, value),
			Number n => n.SetBit(indexes, value),
			_ => throw new($"{variable.Name} is not an indexed variable")
		};
	}

	public Result Execute(Program program)
		=> program.Blocks.Aggregate(Result.Unknown, (_, block) => Execute(block));

	internal Result Execute(Block block) {
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
		Enlist e => Enlist(e),
		Mutation m => Mutation(m),
		Rounding r => Rounding(r),
		Listen listen => Listen(listen),
		Increment inc => Increment(inc),
		Decrement dec => Decrement(dec),
		ExpressionStatement e => ExpressionStatement(e),
		_ => throw new($"I don't know how to execute {statement.GetType().Name} statements")
	};

	private Result Increment(Increment inc) {
		var variable = QualifyPronoun(inc.Variable);
		return Eval(variable) switch {
			Null n => Assign(variable, new Number(inc.Multiple)),
			Number n => Assign(variable, new Number(n.Value + inc.Multiple)),
			Booleän b => inc.Multiple % 2 == 0 ? new(b) : Assign(variable, b.Negate),
			{ } v => throw new($"Cannot increment '{variable.Name}' because it has type {v.GetType().Name}")
		};
	}

	private Result Decrement(Decrement dec) {
		var variable = QualifyPronoun(dec.Variable);
		return Eval(dec.Variable) switch {
			Null n => Assign(variable, new Number(-dec.Multiple)),
			Number n => Assign(variable, new Number(n.Value - dec.Multiple)),
			Booleän b => dec.Multiple % 2 == 0 ? new(b) : Assign(variable, b.Negate),
			{ } v => throw new($"Cannot increment '{variable.Name}' because it has type {v.GetType().Name}")
		};
	}


	private Result Listen(Listen l) {
		var input = ReadInput();
		Value value = input == default ? new Null() : new Strïng(input);
		if (l.Variable != default) SetVariable(l.Variable, value);
		return new(value);
	}

	private Result Mutation(Mutation m) {
		var source = Eval(m.Expression);
		var modifier = m.Modifier == null ? null : Eval(m.Modifier);
		var result = m.Operator switch {
			Operator.Join => Join(source, modifier),
			Operator.Split => Split(source, modifier),
			Operator.Cast => Cast(source, modifier),
			_ => throw new($"Unsupported mutation operator {m.Operator}")
		};
		if (m.Target == default) return new(result);
		SetLocal(QualifyPronoun(m.Target), result);
		if (m.Target is not Pronoun) this.pronounSubject = m.Target;
		return new(result);
	}

	private static Value Cast(Value source, Value modifier) {
		return source switch {
			Strïng s => Number.Parse(s, modifier),
			IHaveANumber n => new Strïng(Char.ConvertFromUtf32((int) n.Value)),
			_ => throw new($"Can't cast expression of type {source.GetType().Name}")
		};
	}

	private static Array Split(Value source, Value? modifier) {
		if (source is not Strïng s) throw new("Only strings can be split.");
		var splitter = modifier?.ToString() ?? "";
		return s.Split(splitter);
	}

	private static Value Join(Value source, Value? modifier) {
		if (source is not Array array) throw new("Can't join something which is not an array.");
		var joiner = modifier?.ToString() ?? "";
		return array.Join(joiner);
	}

	private Result Rounding(Rounding r) {
		var value = Lookup(r.Variable);
		if (value is not Number n) throw new($"Can't apply rounding to variable {r.Variable.Name} of type {value.GetType().Name}");
		var rounded = new Number(r.Round switch {
			Round.Down => Math.Floor(n.Value),
			Round.Up => Math.Ceiling(n.Value),
			Round.Nearest => Math.Round(n.Value),
			_ => throw new ArgumentOutOfRangeException()
		});
		SetVariable(r.Variable, rounded);
		return new(rounded);
	}

	private Result Delist(Delist delist) {
		var variable = QualifyPronoun(delist.Variable);
		var value = LookupValue(variable.Key);
		if (value is Array array) return new(array.Pop());
		return new(new Null());
	}

	private Result Enlist(Enlist e) {
		var variable = QualifyPronoun(e.Variable);
		var value = LookupValue(variable.Key);
		if (value is not Array array) {
			array = value == Mysterious.Instance ? new Array() : new(value);
			SetLocal(variable, array);
		}
		foreach (var expr in e.Expressions) array.Push(Eval(expr));
		return new(array);
	}

	private Result ExpressionStatement(ExpressionStatement e)
		=> new Result(Eval(e.Expression));

	private Result Return(ExpressionStatement r) {
		var value = Eval(r.Expression);
		return Result.Return(value);
	}

	private Result Call(FunctionCall call)
		=> Call(call, []);

	private Result Call(FunctionCall call, Queue<Expression> bucket) {
		var value = Lookup(call.Function);
		if (value is not Closure closure) throw new($"'{call.Function.Name}' is not a function");
		var names = closure.Function.Args.ToList();

		List<Value> values = [];

		foreach (var arg in call.Args.Take(names.Count)) {
			value = arg is FunctionCall nestedCall ? Call(nestedCall, bucket).Value : Eval(arg);
			values.Add(value);
		}
		if (call.Args.Count + bucket.Count < names.Count) {
			throw new($"Not enough arguments supplied to function {call.Function.Name} - expected {names.Count} ({String.Join(", ", names.Select(v => v.Name))}), got {call.Args.Count}");
		}
		while (values.Count < names.Count) values.Add(Eval(bucket.Dequeue()));
		foreach (var expression in call.Args.Skip(names.Count)) bucket.Enqueue(expression);
		Dictionary<Variable, Value> args = new();
		for (var i = 0; i < names.Count; i++) args[names[i]] = values[i].Clone();
		return closure.Apply(args);
	}

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
				case WhatToDo.Break: return result;
				case WhatToDo.Return: return result;
			}
		}
		return result;
	}


	private Result Output(Output output) {
		var value = Eval(output.Expression) switch {
			Array array => array.Lëngth,
			{ } v => v
		};
		Write(value.ToStrïng().Value);
		Write(output.Suffix);
		return new(value);
	}

	private Value Eval(Expression expression) => expression switch {
		Value value => value,
		Binary binary => binary.Resolve(Eval),
		Lookup lookup => Lookup(lookup.Variable),
		Variable v => Lookup(v),
		Unary unary => unary.Resolve(Eval),
		FunctionCall call => Call(call).Value,
		Delist delist => Delist(delist).Value,
		_ => throw new NotImplementedException($"Eval not implemented for {expression.GetType()}")
	};

	public Result Assign(Variable variable, Value value) => value switch {
		Function function => SetVariable(variable, MakeLambda(function)),
		_ => SetVariable(variable, value)
	};

	private Value MakeLambda(Function function) => new Closure(function, this);

	public Result Assign(Assign assign)
		=> Assign(assign.Variable, Eval(assign.Expression));

	private Value LookupValue(string key) {
		if (variables.TryGetValue(key, out var value)) return value;
		return Parent != default ? Parent.LookupValue(key) : Mysterious.Instance;
	}

	public Value Lookup(Variable variable) {
		var key = variable is Pronoun pronoun ? QualifyPronoun(pronoun).Key : variable.Key;
		var value = LookupValue(key);
		return value.AtIndex(variable.Indexes.Select(Eval));
	}
}