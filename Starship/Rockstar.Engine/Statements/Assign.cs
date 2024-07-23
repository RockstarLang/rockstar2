using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Assign(Variable variable, Expression expression) : ExpressionStatement(expression) {
	public Variable Variable => variable;
	public override void Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		variable.Print(sb, prefix + INDENT);
		expression.Print(sb, prefix + INDENT);
	}
}