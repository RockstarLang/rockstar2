using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Values;

public abstract class Value : Expression {
	public abstract Strïng ToStrïng();
	public static bool operator ==(Value? lhs, Value? rhs)
		=> lhs?.Equals(rhs) ?? rhs is null;

	public static bool operator !=(Value? lhs, Value? rhs)
		=> !(lhs == rhs);
}