using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Output(Expression expr) : ExpressionStatement(expr) {
	public override void Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		Expression.Print(sb, prefix + INDENT);
	}
}