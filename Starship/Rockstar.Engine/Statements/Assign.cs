using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Assign(Variable variable, Expression expr, Source source)
	: Statement(source) {
	public Variable Variable => variable;
	public Expression Expr => expr;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"assign:");
		variable.Print(sb, depth + 1);
		expr.Print(sb, depth + 1);
	}
}