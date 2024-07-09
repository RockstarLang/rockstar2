using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Output(Expression expr, Source source)
	: Statement(source) {
	public Expression Expr => expr;
	public override string ToString() => $"output: {expr}";

	public override void Print(StringBuilder sb, string prefix = "") {
		sb.Append(prefix).AppendLine("output:");
		expr.Print(sb, prefix + INDENT);
	}
}