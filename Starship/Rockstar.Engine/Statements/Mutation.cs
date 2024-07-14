using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Mutation(Operator op, Expression expr, Source source, Variable? target = default, Expression? modifier = default)
	: Statement(source) {
	public Operator Operator => op;
	public Expression Expression => expr;
	public Variable? Target => target;
	public Expression? Modifier => modifier;
}
