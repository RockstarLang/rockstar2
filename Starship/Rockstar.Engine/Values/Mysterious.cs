namespace Rockstar.Engine.Values;

public class Mysterious : Value {
	public static Mysterious Instance = new();
	public override Strïng ToStrïng() => new("mysterious");
}