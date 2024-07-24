using System.Text;
using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;

namespace Rockstar.Engine.Values;

public class Return(Expression expr) : ExpressionStatement(expr) {
	public override StringBuilder Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		return Expression.Print(sb, prefix + INDENT);
	}
}