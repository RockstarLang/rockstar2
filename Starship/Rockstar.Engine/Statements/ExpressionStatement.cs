using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class ExpressionStatement(Expression expr) : Statement {
	public Expression Expression = expr;

	public override StringBuilder Print(StringBuilder sb, string prefix)
		=> Expression.Print(sb.AppendLine("expression:"), prefix + INDENT);
}