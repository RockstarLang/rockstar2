using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Conditional(Expression condition, Block consequent, Block? alternate = null) : Statement {

	public Expression Condition => condition;
	public Block Consequent => consequent;
	public Block? Alternate => alternate;

	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine("if:");
		sb.Append(prefix + NESTER).AppendLine("test:");
		condition.Print(sb, prefix + NESTER + INDENT);
		sb.Append(prefix + NESTER).AppendLine("then:");
		consequent.Print(sb, prefix + NESTER + INDENT);
		if (alternate == default) return;
		sb.Append(prefix + NESTER).AppendLine("else:");
		alternate.Print(sb, prefix + NESTER+ INDENT);
	}
}

public class ExpressionStatement(Expression expr) : Statement {
	public Expression Expression = expr;
}