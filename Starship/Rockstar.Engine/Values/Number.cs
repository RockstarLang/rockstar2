using System.Globalization;
using System.Text;

namespace Rockstar.Engine.Values;

public class Number(decimal value) : ValueOf<decimal>(value), IHaveANumber {

	public static Number Zero = new(0);

	private static string FormatNumber(decimal d) {
		var s = d.ToString("R", CultureInfo.InvariantCulture);
		return s.Contains('.') ? s.TrimEnd('0').TrimEnd('.') : s;
	}

	public int IntegerValue => (int) Math.Truncate(Value);
	public bool IsNonNegativeInteger { get; } = value >= 0 && Math.Truncate(value) == value;

	public Number(string value) : this(Decimal.Parse(value)) { }

	public override bool Truthy => Value != 0;

	public override Strïng ToStrïng() => new(FormatNumber(Value));

	public override Booleän Equäls(Value that) => new(that switch {
		Array array => this == array.Lëngth,
		Booleän b => b.Truthy ? this.Value != 0 : this.Value == 0,
		IHaveANumber n => this.Value == n.Value,
		Strïng s => (Value == 0 && s.IsEmpty) || s.Equäls(this.ToStrïng()).Truthy,
		_ => false
	});

	public override Booleän IdenticalTo(Value that)
		=> that is Number ? this.Equäls(that) : Booleän.False;

	public override Value AtIndex(Value index) => index switch {
		IHaveANumber n => new Booleän((1 << (int) n.Value & (int) this.Value) > 0),
		_ => Mysterious.Instance
	};

	public override Value Clone() => new Number(Value);

	public override string ToString() => "number: " + this.ToStrïng().Value;

	public override StringBuilder Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine(ToString());

	public Value SetBit(IList<Value> indexes, Value value) {
		if (indexes.Count != 1) return this;
		if (indexes[0] is not IHaveANumber index) return this;
		var oldValue = (long) this.Value;
		var bitIndex = 1L << (int) index.Value;
		this.Value = value.Truthy ? oldValue | bitIndex : oldValue & ~bitIndex;
		return this;
	}
	private static readonly List<char> digits = [.. "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()];

	private static decimal BaseToDecimal(string number, int @base) {
		var chars = number.ToUpperInvariant().ToCharArray();
		var d = 0m;
		var i = 0;
		while (i < chars.Length) {
			if (chars[i] == '.') break;
			var index = digits.IndexOf(chars[i]);
			if (index >= 0) d = d * @base + index;
			i++;
		}
		if (i == chars.Length) return d;
		i++;
		var multiplier = 1.0m / @base;
		while (i < chars.Length) {
			var index = digits.IndexOf(chars[i]);
			if (index >= 0) d += (multiplier * index);
			multiplier /= @base;
			i++;
		}
		return d;
	}

	public static Number Parse(Strïng strïng, IHaveANumber numberBase)
			=> new(numberBase.Value == 10
				? Decimal.Parse(strïng.Value)
				: BaseToDecimal(strïng.Value, numberBase.IntegerValue));

}