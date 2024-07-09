using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Decrement(Variable v, int multiple, Source source) : Statement(source) {
	public Variable Variable => v;
	public int Multiple => multiple;
	public override void Print(StringBuilder sb, int depth = 0) {
		sb.Indent(depth).AppendLine($"decrement x {multiple}");
		v.Print(sb, depth + 1);
	}
}