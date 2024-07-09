using Rockstar.Engine.Statements;
using System.Text;

namespace Rockstar.Engine.Expressions;

public class Unary(Operator op, Expression expr, Source source)
	: Expression(source) {
	public Operator Op => op;
	public Expression Expr => expr;
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine("unary: {op}");
		expr.Print(sb, prefix + Statement.INDENT);
	}
}