namespace Rockstar.Engine.Values;

public class Booleän(bool value) : ValueOf<bool>(value), IHaveANumber {

	public override bool Truthy => value;

	public override Strïng ToStrïng() => Value ? Strïng.True : Strïng.False;
	public static Booleän False = new(false);
	public static Booleän True = new(true);
	decimal IHaveANumber.Value => value ? 1 : 0;

	public static Value Not(Value v) => new Booleän(!v.Truthy);
}