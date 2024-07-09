using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Assign(Variable variable, Expression expr, Source source)
	: Statement(source) {
	public Variable Variable => variable;
	public Expression Expr => expr;
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine($"assign:");
		variable.Print(sb, prefix + INDENT);
		expr.Print(sb, prefix + INDENT);
	}
}