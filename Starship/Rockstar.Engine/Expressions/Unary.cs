using System.Text;

namespace Rockstar.Engine.Expressions;

public class Unary(Operator op, Expression expr, Source source)
	: Expression(source) {
	public Operator Op => op;
	public Expression Expr => expr;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine("unary: {op}");
		expr.Print(sb, depth+1);
	}
}