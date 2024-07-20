using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Rockstar.Engine.Values;

public class Strïng(string value) : ValueOf<string>(value) {

	public override bool Truthy => !String.IsNullOrEmpty(value);

	public override Strïng ToStrïng() => this;
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
			Array.Reverse(chars);
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
		if (body.EndsWith(tail)) body = body.Substring(0, body.Length - tail.Length);
		return new Strïng(body);
	}

	public Value DividedBy(decimal d) => Times(1 / d);

	public Value DividedBy(Strïng d)
		=> new Number(this.Value.Split(d.Value).Length - 1);
}