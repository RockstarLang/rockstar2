using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Conditional(Expression condition, Block consequent, Block? alternate, Source source)
	: Statement(source) {
	public Conditional(Expression condition, Block consequent, Source source) : this(condition, consequent, null, source) {
		
	}
	public override void Print(StringBuilder sb, int depth = 0) {
		sb.Indent(depth).AppendLine("if:");
		sb.Indent(depth + 1).AppendLine("test:");
		condition.Print(sb, depth + 2);
		sb.Indent(depth + 1).AppendLine("then:");
		consequent.Print(sb, depth + 2);
		if (alternate == null) return;
		sb.Indent(depth + 1).AppendLine("else:");
		alternate.Print(sb, depth + 2);
	}
}
