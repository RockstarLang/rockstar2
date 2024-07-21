using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Conditional(Expression condition, Block consequent, Block? alternate = null) : Statement {

	public Expression Condition => condition;
	public Block Consequent => consequent;
	public Block? Alternate => alternate;

	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine("if:");
		sb.Append(prefix + "| ").AppendLine("test:");
		condition.Print(sb, prefix + "| " + INDENT);
		sb.Append(prefix + "| ").AppendLine("then:");
		consequent.Print(sb, prefix + "| " + INDENT);
		if (alternate == default) return;
		sb.Append(prefix + INDENT).AppendLine("else:");
		consequent.Print(sb, prefix + INDENT + INDENT);
	}
}

public class ExpressionStatement(Expression expr) : Statement {
	public Expression Expression = expr;
}