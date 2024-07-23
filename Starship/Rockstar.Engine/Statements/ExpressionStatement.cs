using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class ExpressionStatement(Expression expr) : Statement {
	public Expression Expression = expr;
}