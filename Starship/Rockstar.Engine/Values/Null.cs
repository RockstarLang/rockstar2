namespace Rockstar.Engine.Values;

public class Null : Value {
	public override Strïng ToStrïng() => Strïng.Null;
	public static readonly Null Instance = new();
}
