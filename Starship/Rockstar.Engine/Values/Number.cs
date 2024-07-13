using System.Globalization;
using System.Text;
namespace Rockstar.Engine.Values;

public class Number(decimal value, Source source)
	: Value(source), IHaveANumber {

	public override bool Equals(object? obj) => Equals(obj as Number);
	public override int GetHashCode() => this.Value.GetHashCode();
	public bool Equals(Number? that) => that != null && this.Value == that.Value;

	public Number(string s) : this(Decimal.Parse(s)) { }

	public Number(string s, Source source) : this(Decimal.Parse(s), source) { }

	public Number(decimal value) : this(value, Source.None) { }

	public decimal Value => value;
	public override string ToString()
		=> value.ToString(CultureInfo.InvariantCulture);

	//public override void Print(StringBuilder sb, string prefix)
	//	=> sb.Append(prefix).AppendLine($"number: {value:G29} {Location}");

	public override bool Truthy => value != 0m;

	public Value Negate() => new Number(-value);
	public decimal NumericValue => value;
}