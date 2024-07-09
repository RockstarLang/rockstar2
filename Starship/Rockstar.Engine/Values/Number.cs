using System.Globalization;
using System.Text;
namespace Rockstar.Engine.Values;

public interface IHaveANumber {
	decimal NumericValue { get; }
}

public class Number(decimal value, Source source)
	: Value(source), IHaveANumber {

	public Number(string s) : this(Decimal.Parse(s)) { }

	public Number(string s, Source source) : this(Decimal.Parse(s), source) { }

	public Number(decimal value) : this(value, Source.None) { }

	public decimal Value => value;
	public override string ToString()
		=> value.ToString(CultureInfo.InvariantCulture);

	//public override void Print(StringBuilder sb, int depth)
	//	=> sb.Indent(depth).AppendLine($"number: {value:G29} {Location}");

	public override bool Truthy => value != 0m;

	public Value Negate() => new Number(-value);
	public decimal NumericValue => value;
}