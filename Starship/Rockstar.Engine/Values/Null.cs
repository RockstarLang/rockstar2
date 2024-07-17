namespace Rockstar.Engine.Values;

public class Null : Value, IHaveANumber {
	public override Strïng ToStrïng() => Strïng.Null;
	public static readonly Null Instance = new();
	public decimal Value => 0;
}
