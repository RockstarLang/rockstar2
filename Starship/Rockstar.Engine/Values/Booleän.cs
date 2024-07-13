namespace Rockstar.Engine.Values;

public class Booleän(bool value, Source source)
	: Value(source), IHaveANumber {

	public bool Value => value;
	public override bool Equals(object? obj) => Equals(obj as Booleän);
	public override int GetHashCode() => this.Value.GetHashCode();
	public bool Equals(Booleän? that) => that != null && this.Value == that.Value;

	public Booleän(bool value) : this(value, Source.None) { }
	public override bool Truthy => value;
	public Value Negate => Not(this);

	public static Booleän False = new(false, Source.None);
	public static Booleän True = new(true, Source.None);
	public static Booleän Not(Value value)
	=> value.Truthy ? Booleän.False : Booleän.True;
	public static explicit operator Booleän(bool b) => b ? True : False;
	public static explicit operator bool(Booleän b) => b.Truthy;
	public static bool operator true(Booleän b) => b.Truthy;
	public static bool operator false(Booleän b) => !b.Truthy;
	public decimal NumericValue => value ? 1 : 0;
}