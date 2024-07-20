using System.Globalization;
using System.Text;

namespace Rockstar.Engine.Values;

public class Number(decimal value) : ValueOf<decimal>(value), IHaveANumber {
	private static string FormatNumber(decimal d) {
		var s = d.ToString("R", CultureInfo.InvariantCulture);
		return s.Contains('.') ? s.TrimEnd('0').TrimEnd('.') : s;
	}

	public Number(string value) : this(Decimal.Parse(value)) { }
	public override bool Truthy => Value != 0;
	public override Strïng ToStrïng() => new(FormatNumber(Value));
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine("number: " + this.ToStrïng().Value);
	}
}