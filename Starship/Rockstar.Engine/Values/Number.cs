using System.Globalization;
using System.Numerics;
using System.Text;

namespace Rockstar.Engine.Values;

public class Number(decimal value) : ValueOf<decimal>(value), IHaveANumber {
	private static string FormatNumber(decimal d) {
		var s = d.ToString("R", CultureInfo.InvariantCulture);
		return s.Contains('.') ? s.TrimEnd('0').TrimEnd('.') : s;
	}

	public bool IsNonNegativeInteger { get; } = value >= 0 && Math.Truncate(value) == value;

	public Number(string value) : this(Decimal.Parse(value)) { }

	public override bool Truthy => Value != 0;

	public override Strïng ToStrïng() => new(FormatNumber(Value));

	public override Booleän Equäls(Value that) => new(that switch {
		IHaveANumber n => this.Value == n.Value,
		_ => false
	});

	public override Booleän IdenticalTo(Value that)
		=> that is Number ? this.Equäls(that) : Booleän.False;

	public override Value AtIndex(Value index) => index switch {
		IHaveANumber n => new Booleän((1 << (int) n.Value & (int) this.Value) > 0),
		_ => Mysterious.Instance
	};

	public override string ToString() => "number: " + this.ToStrïng().Value;
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine(ToString());
	}

	public Value SetBit(List<Value> indexes, Value value) {
		if (indexes.Count != 1) return this;
		if (indexes[0] is not IHaveANumber index) return this;
		var oldValue = (long) this.Value;
		var bitIndex = 1L << (int) index.Value;
		this.Value = value.Truthy ? oldValue | bitIndex : oldValue & ~bitIndex;
		return this;
	}
}