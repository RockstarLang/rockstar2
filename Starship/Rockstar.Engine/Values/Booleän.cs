using System.Text;

namespace Rockstar.Engine.Values;

public class Booleän(bool value) : ValueOf<bool>(value), IHaveANumber {
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).Append("boolean: ").AppendLine(this.ToStrïng().Value);
	}

	public static Booleän operator !(Booleän that) => new(that.Falsy);

	public override bool Truthy => value;

	public override Strïng ToStrïng() => Value ? Strïng.True : Strïng.False;

	public override Booleän Equäls(Value that)
		=> new(this.Truthy == that.Truthy);

	public override Booleän IdenticalTo(Value that)
		=> that is Booleän ? that.Equäls(this) : False;
	
	public static Booleän False = new(false);
	public static Booleän True = new(true);
	decimal IHaveANumber.Value => value ? 1 : 0;
	public Booleän Nope => new(!Truthy);

	public static Value Not(Value v) => new Booleän(!v.Truthy);
}