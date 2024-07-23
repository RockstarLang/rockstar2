using System.Globalization;
using System.Text;

namespace Rockstar.Engine.Values;

public class Number(decimal value) : ValueOf<decimal>(value), IHaveANumber {
	private static string FormatNumber(decimal d) {
		var s = d.ToString("R", CultureInfo.InvariantCulture);
		return s.Contains('.') ? s.TrimEnd('0').TrimEnd('.') : s;
	}

	public bool IsNonNegativeInteger { get; } = value >= 0 && value == (int) value;

	public Number(string value) : this(Decimal.Parse(value)) { }

	public override bool Truthy => Value != 0;
	public override Strïng ToStrïng() => new(FormatNumber(Value));

	public override Booleän Equäls(Value that) => new(that switch {
		IHaveANumber n => this.Value == n.Value,
		_ => false
	});

	public override Booleän IdenticalTo(Value that)
		=> that is Number ? this.Equäls(that) : Booleän.False;

	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine("number: " + this.ToStrïng().Value);
	}
}