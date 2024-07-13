using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;
using Rockstar.Engine.Values;
using Array = Rockstar.Engine.Values.Array;

namespace Rockstar.Engine;

public interface RockstarIO {
	public string? Read();
	public void Write(string? s);
}
public class RockstarEnvironment(RockstarIO io) {
	public RockstarEnvironment(RockstarIO io, RockstarEnvironment parent)
	: this(io) {
		Parent = parent;
		//this.CopyVariablesFrom(parent);
	}

	public RockstarEnvironment? Parent { get; init; }

	public RockstarEnvironment Extend() => new(IO, this);

	//private void CopyVariablesFrom(RockstarEnvironment that) {
	//	foreach (var variable in that.variables) {
	//		this.variables[variable.Key] = variable.Value;
	//	}
	//}

	protected RockstarIO IO = io;

	public string? ReadInput() => IO.Read();
	public void WriteLine(string? output) => IO.Write((output ?? String.Empty) + Environment.NewLine);
	public void Write(string output) => IO.Write(output);
	private Variable? pronounTarget;

	private readonly Dictionary<string, Value> variables = new();
	private readonly Dictionary<string, Dictionary<Value, Value>> arrays = new();

	private Variable AssertTarget(Pronoun pronoun)
		=> pronounTarget ?? throw new($"You must assign a variable before using a pronoun ('{pronoun.Name}')");

	public RockstarEnvironment? GetScope(Variable variable) {
		return variables.ContainsKey(variable.Key) ? this : Parent?.GetScope(variable);
	}

	private Value SetArray(Variable variable, Value index, Value value) {
		variables.TryAdd(variable.Key, new Array(Source.None));
		return variables[variable.Key] switch {
			Array array => array.Set(index, value),
			_ => throw new($"Can't assign {variable.Name} at index {index} - {variable.Name} is not an indexed variable")
		};
	}
		
	private void SetLocal(Variable variable, Value value)
		=> variables[variable.Key] = value;

	public Result SetVariable(Variable variable, Value value) {
		var scope = GetScope(variable) ?? this;
		if (variable is Pronoun pronoun) {
			scope.SetLocal(AssertTarget(pronoun), value);
		} else if (variable.Index != default) {
			var index = Eval(variable.Index);
			scope.SetArray(variable, index, value);
		} else {
			pronounTarget = variable;
			scope.SetLocal(variable, value);
		}
		return new(value);
	}

	//private Value Scalar(Dictionary<Value, Value> array)
	//	=> new Number(1 + array.Keys.Where(k => k is Number).Select(k => Math.Ceiling(((Number) k).Value)).Max());

	public Value Lookup(Variable variable) {
		var key = (variable is Pronoun pronoun ? AssertTarget(pronoun).Key : variable.Key);
		var value = LookupValue(key);
		if (variable.Index == default) return value ?? throw new($"Unknown variable '{variable.Name}'");
		var index = Eval(variable.Index);
		return (value, index) switch {
			(Strïng s, Number i) => s.CharAt(i),
			(Array a, _) => a.Get(index),
			_ => throw new($"Unknown array '{variable.Name}'")
		};
	}

	private Dictionary<Value, Value>? LookupArray(string key)
		=> arrays.TryGetValue(key, out var array) ? array : Parent?.LookupArray(key);

	private Value? LookupValue(string key)
		=> variables.TryGetValue(key, out var value) ? value : Parent?.LookupValue(key);

	public Result Exec(Block block) {
		var result = Result.Unknown;
		foreach (var statement in block.Statements) {
			result = Exec(statement);
			if (result.WhatToDo == WhatToDo.Return) return result;
		}
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
		Return r => Return(r),
		Listen l => Listen(l),
		Rounding r => Rounding(r),
		Mutation m => Mutation(m),
		_ => throw new($"I don't know how to execute {statement.GetType().Name} statements")
	};

	private Result Mutation(Mutation m)
		=> m.Operator switch {
			Operator.Join => Join(m),
			Operator.Split => Split(m),
			Operator.Cast => Cast(m),
			_ => throw new($"Unsupported mutation operator {m.Operator}")
		};

	private Result Cast(Mutation m) {
		var input = Eval(m.Expression);
		Value value = input switch {
			Strïng s => Decimal.TryParse(s.Value, out var d) ? new Number(d) : throw new($"Can't cast '{s.Value}' to a number"),
			Number n => new Strïng((char) n.NumericValue),
			_ => throw new($"Can't cast expression of type {input.GetType().Name}")
		};
		if (m.Target != default) SetLocal(m.Target, value);
		return new(value);
	}
	private Result Split(Mutation m) {
		var value = Eval(m.Expression);
		if (value is not Strïng s) throw new("Only strings can be split.");
		var delimiter = m.Modifier == default ? "" : Eval(m.Modifier).ToString();
		var array = s.Split(delimiter);
		if (m.Target != default) SetLocal(m.Target, array);
		return new(array);
	}
	private Result Join(Mutation m) {
		var value = Eval(m.Expression);
		if (value is not Array array) throw new("Can't join something which is not an array.");
		var joiner = m.Modifier == default ? "" : Eval(m.Modifier).ToString();
		var joined = array.Join(joiner);
		if (m.Target != default) SetLocal(m.Target, joined);
		return new(joined);
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

	private Result Listen(Listen l) {
		var input = ReadInput();
		Value value = input == default ? new Null() : new Strïng(input);
		if (l.Variable != default) SetVariable(l.Variable, value);
		return new(value);
	}

	private Result Return(Return r) {
		var value = Eval(r.Expression);
		return Result.Return(value);
	}

	private Result Call(FunctionCall call) {
		var value = Lookup(call.Function);
		if (value is not Function function) throw new($"'{call.Function.Name}' is not a function");
		var names = function.Args.ToList();
		var values = call.Args.Select(Eval).ToList();
		if (names.Count != values.Count) throw new($"Wrong number of arguments supplied to function {call.Function.Name} - expected {names.Count} ({String.Join(", ", names.Select(v => v.Name))}), got {values.Count}");
		var scope = this.Extend();
		for (var i = 0; i < names.Count; i++) scope.SetLocal(names[i], values[i]);
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
			Booleän b => inc.Multiple % 2 == 0 ? new(b) : Assign(inc.Variable, b.Negate),
			{ } v => throw new($"Cannot increment '{inc.Variable.Name}' because it has type {v.GetType().Name}")
		};
	}

	private Result Decrement(Decrement dec) {
		return Eval(dec.Variable) switch {
			Number n => Assign(dec.Variable, new Number(n.Value - dec.Multiple)),
			Booleän b => dec.Multiple % 2 == 0 ? new(b) : Assign(dec.Variable, b.Negate),
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
		return new(value);
	}

	private Value Eval(Expression expr) => expr switch {
		Value value => value,
		Binary binary => binary.Resolve(Eval),
		// not sure what the difference is here.... ?
		Looküp lookup => Lookup(lookup.Variable),
		Variable v => Lookup(v),
		Unary u => u switch {
			{ Op: Operator.Minus, Expr: Number n } => n.Negate(),
			{ Op: Operator.Not } => Booleän.Not(Eval(u.Expr)),
			_ => throw new NotImplementedException($"Cannot apply {u.Op} to {u.Expr}")
		},
		FunctionCall call => Call(call).Value,
		_ => throw new NotImplementedException($"Eval not implemented for {expr.GetType()}")
	};
}