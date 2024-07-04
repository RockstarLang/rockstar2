namespace Rockstar.Engine.Values;

public class Booleän(bool value, Source source)
	: Value(source) {
	public override bool Truthy => value;
	public static Booleän False = new(false, Source.None);
	public static Booleän True = new(true, Source.None);
	public static Booleän Not(Value value)
	=> value.Truthy ? Booleän.False : Booleän.True;
	public static explicit operator Booleän(bool b) => b ? True : False;
	public static explicit operator bool(Booleän b) => b.Truthy;
	public static bool operator true(Booleän b) => b.Truthy;
	public static bool operator false(Booleän b) => !b.Truthy;
}