using System.Globalization;
using System.Text;

namespace Rockstar.Engine.Values;

public class Number(decimal value) : ValueOf<decimal>(value) {
	private String FormatNumber(decimal value) {
		var s = value.ToString("R", CultureInfo.InvariantCulture);
		return s.Contains('.') ? s.TrimEnd('0').TrimEnd('.') : s;
	}

	public Number(string value) : this(Decimal.Parse(value)) { }
	public override Strïng ToStrïng() => new(FormatNumber(value));
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine("number: " + this.ToStrïng().Value);
	}
}