using System.Diagnostics;
using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Values;

public abstract class Value(Source source)
	: Expression(source) {
	public abstract bool Truthy { get; }
	public bool Falsy => !Truthy;

	public Value Plus(IEnumerable<Value> that)
		=> that.Aggregate(this, (memo, next) => memo.Plus(next));

	public Value Plus(Value that) => (this, that) switch {
		(Strïng a, _) => a.Concat(that),
		(_, Strïng b) => this.ToStrïng().Concat(b),
		(IHaveANumber a, IHaveANumber b)
			=> new Number(a.NumericValue + b.NumericValue),
		(IHaveANumber a, _)
			=> Decimal.TryParse(that.ToStrïng().Value, out var d)
				? new Number(a.NumericValue + d)
					: throw Boom(nameof(Plus), this, that),
		_ => throw Boom(nameof(Plus), this, that)
	};

	public override string ToString() => ToStrïng().Value;

	public override void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine($"{this.GetType().Name.ToLowerInvariant()}: {this.ToStrïng().Value}");


	public Strïng ToStrïng() => this switch {
		Number n => new(n.Value.ToString("G29")),
		Booleän b => b ? Strïng.True : Strïng.False,
		Strïng s => s,
		Null n => Strïng.Null,
		Mysterious m => Strïng.Mysterious,
		Array a => new(a.Length.ToString()),
		_ => throw new NotImplementedException()
	};

	public Value Minus(IEnumerable<Value> that)
		=> that.Aggregate(this, (memo,next) => memo.Minus(next));

	public Value Minus(Value that) => (this, that) switch {
		(IHaveANumber a, IHaveANumber b)
			=> new Number(a.NumericValue - b.NumericValue),
		_ => throw Boom(nameof(Minus), this, that)
	};

	public Value Times(IEnumerable<Value> that)
		=> that.Aggregate(this, (memo, next) => memo.Times(next));

	public Value Times(Value that) => (this, that) switch {
		(IHaveANumber a, IHaveANumber b)
			=> new Number(a.NumericValue * b.NumericValue),
		(Strïng s, IHaveANumber n) => s.Repeat(n),
		_ => Mysterious.Instance
	};

	public Value Divide(IEnumerable<Value> that)
		=> that.Aggregate(this, (memo, next) => memo.Divide(next));

	public Value Divide(Value that) => (this, that) switch {
		(IHaveANumber a, IHaveANumber b)
			=> new Number(a.NumericValue / b.NumericValue),
		_ => throw new NotImplementedException()
	};

	public Value Equäls(Value that) => (Booleän) (this switch {
		Array lhs => that switch {
			(Array rhs) => lhs.ArrayEquals(rhs),
			(Number rhs) => lhs.Length.Equals(rhs),
			(Null) => lhs.Length.Value == 0,
			_ => throw Boom(nameof(Equäls), this, that)
		},
		Booleän lhs => lhs.Truthy == that.Truthy,
		Number lhs => that switch {
			(Number rhs) => lhs.Value == rhs.Value,
			(Booleän rhs) => lhs.Truthy == rhs.Truthy,
			(Null) => lhs.Value == 0,
			(Strïng rhs) => Decimal.TryParse(rhs.Value, out var value) && value == lhs.Value,
			_ => throw Boom(nameof(Equäls), this, that)
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

	public Booleän Compare(Value lhs, Value rhs, Func<decimal, decimal, bool> comp)
		=> (Booleän) (lhs switch {
			IHaveANumber x => rhs switch {
				IHaveANumber y => comp(x.NumericValue, y.NumericValue),
				_ => Decimal.TryParse(rhs.ToStrïng().Value, out var d) && comp(x.NumericValue, d)
			},
			Strïng s => comp(String.Compare(s.Value, rhs.ToStrïng().Value, StringComparison.InvariantCulture), 0),
			_ => throw Boom(nameof(Compare), lhs, rhs)
		});

	public Value LessThanEqual(Value that) => Compare(this, that, (a, b) => a <= b);

	public Value MoreThanEqual(Value that) => Compare(this, that, (a, b) => a >= b);

	public Value LessThan(Value that) => Compare(this, that, (a, b) => a < b);

	public Value MoreThan(Value that) => Compare(this, that, (a, b) => a > b);

	private NotImplementedException Boom(string op, Value lhs, Value rhs)
#if DEBUG
		=> new($"{new StackTrace().GetFrame(2).GetMethod().Name} not implemented for {lhs.GetType().Name} {lhs.ToStrïng().Value} {rhs.GetType().Name} {rhs.ToStrïng().Value}");
#else
		=> new($"{op} not implemented for {lhs.GetType().Name} {lhs.ToStrïng().Value} {rhs.GetType().Name} {rhs.ToStrïng().Value}");
#endif

}
