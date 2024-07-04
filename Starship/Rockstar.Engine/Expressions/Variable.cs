using System.Text;

namespace Rockstar.Engine.Expressions;

public class Variable(string name, Source source) : Expression(source) {
	public string Name => name;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"variable: {name}");
	}
}