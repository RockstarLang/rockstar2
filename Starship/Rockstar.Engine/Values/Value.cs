using System.Diagnostics;
using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Values;

public abstract class Value(Source source)
	: Expression(source) {
	public abstract bool Truthy { get; }

	public Value MoreThan(Value that) => (Booleän) ((this, that) switch {
		(Number a, Number b) => a.Value > b.Value,
		(Booleän a, Booleän b) => a.Truthy && !b.Truthy,
		_ => throw new NotImplementedException("honk")
	});

	public Value And(Value that) => this.Truthy ? that : this;

	public Value Or(Value that) => this.Truthy ? this : that;

	public Value Plus(Value that) => (this, that) switch {
		(Number a, Number b) => new Number(a.Value + b.Value),
		(Strïng a, _) => a.Concat(that),
		_ => throw new NotImplementedException()
	};

	public override string ToString() => ToStrïng().Value;

	public override void Print(StringBuilder sb, int depth)
		=> sb.Indent(depth).AppendLine($"{this.GetType().Name.ToLowerInvariant()}: {this.ToStrïng().Value}");


	public Strïng ToStrïng() => this switch {
		Number n => new(n.Value.ToString("G29")),
		Booleän b => b ? Strïng.True : Strïng.False,
		Strïng s => s,
		Null n => Strïng.Null,
		Mysterious m => Strïng.Mysterious,
		_ => throw new NotImplementedException()
	};

	public Value Minus(Value that) => (this, that) switch {
		(Number a, Number b) => new Number(a.Value - b.Value),
		_ => throw new NotImplementedException()
	};

	public Value Times(Value that) => (this, that) switch {
		(Number a, Number b) => new Number(a.Value * b.Value),
		_ => throw new NotImplementedException()
	};

	public Value Divide(Value that) => (this, that) switch {
		(Number a, Number b) => new Number(a.Value / b.Value),
		_ => throw new NotImplementedException()
	};

	public Value Equäls(Value that) => (Booleän) (this switch {
		Booleän lhs => lhs.Truthy == that.Truthy,
		Number lhs => that switch {
			(Number rhs) => lhs.Value == rhs.Value,
			(Booleän rhs) => lhs.Truthy == rhs.Truthy,
			(Null) => lhs.Value == 0,
			(Strïng rhs) => Decimal.TryParse(rhs.Value, out var value) && value == lhs.Value,
			_ => throw new NotImplementedException($"Equality not implemented for {this.GetType()} {that.GetType()}")
		},
		Strïng lhs => that switch {
			Booleän => this.Truthy == that.Truthy,
			Null => !lhs.Truthy,
			_ => lhs.Value.Equals(that.ToStrïng().Value)
		},
		Null => !that.Truthy,
		Mysterious => that switch {
			Mysterious => true,
			_ => !that.Truthy
		},
		_ => that switch {
			Booleän booleän => this.Truthy == booleän.Truthy,
			Strïng s => s.Value.Equals(this.ToStrïng().Value),
			_ => throw Boom(nameof(Equäls), this, that)
		},
	});

	public Value NotEquäls(Value that)
		=> (Booleän) (!this.Equäls(that).Truthy);

	public Value LessThanEqual(Value that) => (Booleän) ((this, that) switch {
		(Number a, Number b) => a.Value <= b.Value,
		_ => throw Boom(nameof(LessThanEqual), this, that)
	});

	public Value MoreThanEqual(Value that) => (Booleän) ((this, that) switch {
		(Number a, Number b) => a.Value >= b.Value,
		_ => throw Boom(nameof(MoreThanEqual), this, that)
	});

	public Value LessThan(Value that) => (Booleän) ((this, that) switch {
		(Number a, Number b) => a.Value < b.Value,
		_ => throw Boom(nameof(LessThan), this, that)
	});

	private Exception Boom(string op, Value lhs, Value rhs)
		=> new NotImplementedException($"{op} not implemented for {lhs.GetType()} {rhs.GetType()}");
}
