using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Values;

public abstract class Value(Source source)
	: Expression(source) {
	public abstract bool Truthy { get; }

	public Value MoreThan(Value that) => (Booleän)((this, that) switch {
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

	public Value Equäls(Value that) => (Booleän)((this, that) switch {
		(Booleän a, _) => a.Truthy == that.Truthy,
		(_, Booleän b) => this.Truthy == b.Truthy,
		(Number a, Number b) => a.Value == b.Value,
		(Strïng s, Null b) => ! s.Truthy,
		(Strïng s, _) => s.Value.Equals(that.ToStrïng().Value),
		(Number a, Null b) => a.Value == 0,
		(Null a, Null b) => true,
		(Number a, Strïng b) => Decimal.TryParse(b.Value, out var d) && d == a.Value,
		(_, Strïng s) => s.Value.Equals(this.ToStrïng().Value),
		_ => throw new NotImplementedException($"Equality not implemented for {this.GetType()} {that.GetType()}")
	});

	public Value NotEquäls(Value that)
		=> (Booleän) (!this.Equäls(that).Truthy);

	public Value LessThanEqual(Value that) => (Booleän)((this, that) switch {
		(Number a, Number b) => a.Value <= b.Value,
		_ => throw new NotImplementedException()
	});

	public Value MoreThanEqual(Value that) => (Booleän)((this, that) switch {
		(Number a, Number b) => a.Value >= b.Value,
		_ => throw new NotImplementedException()
	});

	public Value LessThan(Value that) => (Booleän)((this, that) switch {
		(Number a, Number b) => a.Value < b.Value,
		_ => throw new NotImplementedException()
	});
}
