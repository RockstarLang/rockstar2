namespace Rockstar.Engine.Values;

public class Null : Value, IHaveANumber {
	protected override bool Equals(Value other) => (other is Null);
	public override int GetHashCode() => 0;
	public override bool Truthy => false;
	public override Strïng ToStrïng() => Strïng.Null;
	public static readonly Null Instance = new();
	public decimal Value => 0;
}
