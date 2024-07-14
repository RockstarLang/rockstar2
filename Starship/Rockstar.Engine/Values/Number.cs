using System.Text;

namespace Rockstar.Engine.Values;

public class Number(double value) : ValueOf<double>(value) {
	public override Strïng ToStrïng() => new(Value.ToString("G29"));
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine("number: " + this.ToStrïng().Value);
	}
}