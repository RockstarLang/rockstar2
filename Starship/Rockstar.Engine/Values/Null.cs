namespace Rockstar.Engine.Values;

public class Null : Value, IHaveANumber {
	protected override bool Equals(Value other) => (other is Null);
	public override int GetHashCode() => 0;
	public override bool Truthy => false;
	public override Strïng ToStrïng() => Strïng.Null;

	public override Booleän Equäls(Value that)
		=> new(that is Null);

	public override Booleän IdenticalTo(Value that)
		=> new(that is Null);

	public static readonly Null Instance = new();
	public decimal Value => 0;
}
