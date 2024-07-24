using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Assign(Variable variable, Expression expression) : ExpressionStatement(expression) {
	public Variable Variable => variable;
	public override StringBuilder Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		variable.Print(sb, prefix + INDENT);
		return expression.Print(sb, prefix + INDENT);
	}
}