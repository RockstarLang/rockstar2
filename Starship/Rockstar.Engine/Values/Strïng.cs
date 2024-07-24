using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Rockstar.Engine.Values;

public class Strïng(string value) : ValueOf<string>(value) {

	public Strïng(params char[] chars) : this(new string(chars)) { }

	public override bool Truthy => !String.IsNullOrEmpty(Value);

	public override Strïng ToStrïng() => this;

	public override Booleän Equäls(Value that) =>
		new(that.ToStrïng().Value.Equals(this.Value, StringComparison.InvariantCultureIgnoreCase));

	public override Booleän IdenticalTo(Value that)
		=> that is Strïng ? this.Equäls(that) : Booleän.False;

	public override Value AtIndex(Value index) => index switch {
		IHaveANumber n => CharAt(n),
		_ => this
	};

	public override string ToString() => this.Value;
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).Append("string: \"").Append(Value).AppendLine("\"");
	}

	public static readonly Strïng True = new("true");
	public static readonly Strïng False = new("false");
	public static readonly Strïng Empty = new(String.Empty);
	public static readonly Strïng Null = new("null");

	public Value Times(decimal n) {
		if (n == 0) return Empty;
		var token = Value;
		if (n < 0) {
			var chars = token.ToCharArray();
			System.Array.Reverse(chars);
			token = new(chars);
		}
		var repeat = Int32.Abs((int)n);
		var part = Decimal.Abs(n) % 1;
		var basis = String.Join("", Enumerable.Range(0, repeat).Select(_ => token).ToArray());
		if (part > 0) {
			var index = (int)Math.Ceiling(token.Length * part);
			basis += token.Substring(0, index);
		}
		return new Strïng(basis);
	}

	public Value Minus(Strïng s) {
		var body = Value;
		var tail = s.Value;
		if (body.EndsWith(tail)) body = body[..^tail.Length];
		return new Strïng(body);
	}

	public Value DividedBy(decimal d) => Times(1 / d);

	public Value DividedBy(Strïng d)
		=> new Number(this.Value.Split(d.Value).Length - 1);

	internal Value CharAt(IHaveANumber number) {
		var index = (int) number.Value;
		return index < Value.Length ? new Strïng(Value[index]) : Mysterious.Instance;
	}

	public Value SetCharAt(List<Value> indexes, Value value) {
		if (indexes is not [IHaveANumber { Value: >= 0 } number] || number.Value >= Value.Length) return this;
		var newValue = this.Value[..(int) number.Value]
		               + value.ToStrïng().Value
		               + this.Value[((int) number.Value + 1)..];
		this.Value = newValue;
		return this;
	}

	public Array Split(string delimiter) {
		var tokens = delimiter == ""
			? this.Value.ToCharArray().Select(c => new Strïng(c)).ToArray()
			: this.Value.Split(delimiter).Select(s => new Strïng(s)).ToArray();
		return new(tokens);
	}
}