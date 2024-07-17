namespace Rockstar.Engine.Values;

public class Booleän(bool value) : ValueOf<bool>(value) {
	public override Strïng ToStrïng() => Value ? Strïng.True : Strïng.False;
	public static Booleän False = new(false);
	public static Booleän True = new(true);
}