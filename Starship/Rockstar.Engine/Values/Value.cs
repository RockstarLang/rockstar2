using System.Net.Http.Headers;
using Rockstar.Engine.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace Rockstar.Engine.Values;

public abstract class Value : Expression {
	public abstract Strïng ToStrïng();
	public static bool operator ==(Value? lhs, Value? rhs)
		=> lhs?.Equals(rhs) ?? rhs is null;

	public static bool operator !=(Value? lhs, Value? rhs)
		=> !(lhs == rhs);

	public static Value operator +(Value lhs, IEnumerable<Value> rhs)
		=> rhs.Aggregate(lhs, (memo, next) => memo + next);
	public static Value operator -(Value lhs, IEnumerable<Value> rhs)
		=> rhs.Aggregate(lhs, (memo, next) => memo - next);
	public static Value operator *(Value lhs, IEnumerable<Value> rhs)
		=> rhs.Aggregate(lhs, (memo, next) => memo * next);
	public static Value operator /(Value lhs, IEnumerable<Value> rhs)
		=> rhs.Aggregate(lhs, (memo, next) => memo / next);

	public static Value operator +(Value lhs, Value rhs) => (lhs, rhs) switch {
		(IHaveANumber a, IHaveANumber b) => new Number(a.Value + b.Value),
		(_, _) => new Strïng(lhs.ToStrïng().Value + rhs.ToStrïng().Value),
	};

	public static Value operator -(Value lhs, Value rhs) => (lhs, rhs) switch {
		(IHaveANumber a, IHaveANumber b) => new Number(a.Value - b.Value),
		(_, _) => throw new NotImplementedException($"I don't know how to subtract {lhs.GetType().Name} and {rhs.GetType().Name}")
	};

	public static Value operator *(Value lhs, Value rhs) => (lhs, rhs) switch {
		(IHaveANumber a, IHaveANumber b) => new Number(a.Value * b.Value),
		(_, _) => throw new NotImplementedException($"I don't know how to subtract {lhs.GetType().Name} and {rhs.GetType().Name}")
	};

	public static Value operator /(Value lhs, Value rhs) => (lhs, rhs) switch {
		(IHaveANumber a, IHaveANumber b) => new Number(a.Value / b.Value),
		(_, _) => throw new NotImplementedException($"I don't know how to subtract {lhs.GetType().Name} and {rhs.GetType().Name}")
	};
}