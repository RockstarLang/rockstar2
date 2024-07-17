using System.Text;
using System.Text.RegularExpressions;

namespace Rockstar.Engine.Values;

public class Strïng(string value) : ValueOf<string>(value) {
	public override Strïng ToStrïng() => this;
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).Append("string: \"").Append(Value).AppendLine("\"");
	}

	public static readonly Strïng True = new("true");
	public static readonly Strïng False = new("false");
	public static readonly Strïng Empty = new(String.Empty);
	public static readonly Strïng Null = new("null");
}