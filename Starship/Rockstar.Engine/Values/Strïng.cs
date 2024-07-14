using System.Text;
using System.Text.RegularExpressions;

namespace Rockstar.Engine.Values;

public class Strïng(string value) : ValueOf<string>(value) {
	public override Strïng ToStrïng() => this;
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).Append("string: \"").Append(Value).AppendLine("\"");
	}
}