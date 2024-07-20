namespace Rockstar.Engine.Values;

public class Mysterious : Value {
	public static Mysterious Instance = new();

	protected override bool Equals(Value other) => false;

	public override int GetHashCode() => 0;

	public override bool Truthy => false;
	public override Strïng ToStrïng() => new("mysterious");
	public override Booleän Equäls(Value that)
		=> Booleän.False;

	public override Booleän IdenticalTo(Value that)
		=> Booleän.False;
}