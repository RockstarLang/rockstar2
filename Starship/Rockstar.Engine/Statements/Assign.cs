using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Assign(Variable name, Expression expr, Source source)
	: Statement(source) {
	public string Name => name.Name;
	public Expression Expr => expr;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"assign:");
		name.Print(sb, depth + 1);
		expr.Print(sb, depth + 1);
	}
}